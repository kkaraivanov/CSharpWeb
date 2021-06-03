namespace WebBasics.ServerFramework.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Emit;
    using ViewEngineModel;

    public static class ViewEngineExtension
    {
        public static string GenerateTemplate(this string template, object viewModel)
        {
            string modelType = "object";

            if (viewModel != null)
            {
                if (viewModel.GetType().IsGenericType)
                {
                    var modelName = viewModel.GetType().FullName;
                    var arguments = viewModel.GetType().GenericTypeArguments;
                    modelType = modelName.Substring(0, modelName.IndexOf('`'))
                                  + "<" + string.Join(",", arguments.Select(x => x.FullName)) + ">";
                }
                else
                {
                    modelType = viewModel.GetType().FullName;
                }
            }

            string modelObject = @"
namespace ViewEngineNamespace
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Collections.Generic;
    WebBasics.ServerFramework.ViewEngineModel

    public class NewClassView : IView
    {
        public string Execute(object viewModel, string user)
        {
            var User = user;
            var Model = viewModel as " + modelType + @";
            var sb = new StringBuilder();

            " + template.GetBody() + @"

            return sb.ToString();
        }
    }
}
";
            return modelObject;
        }

        public static IView ExecuteView(this string codeForTemplate, object viewModel)
        {
            var references = CSharpCompilation.Create("ViewAssembly")
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(typeof(IView).Assembly.Location));

            if (viewModel != null)
            {
                if (viewModel.GetType().IsGenericType)
                {
                    var arguments = viewModel.GetType().GenericTypeArguments;
                    foreach (var argument in arguments)
                    {
                        references = references
                            .AddReferences(MetadataReference.CreateFromFile(argument.Assembly.Location));
                    }
                }

                references = references
                    .AddReferences(MetadataReference.CreateFromFile(viewModel.GetType().Assembly.Location));
            }

            var libraries = Assembly
                .Load(new AssemblyName("netstandard"))
                .GetReferencedAssemblies();

            foreach (var library in libraries)
            {
                references = references
                    .AddReferences(MetadataReference.CreateFromFile(Assembly.Load(library).Location));
            }

            references = references.AddSyntaxTrees(SyntaxFactory.ParseSyntaxTree(codeForTemplate));

            using MemoryStream memoryStream = new MemoryStream();
            EmitResult result = references.Emit(memoryStream);

            if (!result.Success)
            {
                return new CompileErrorView(result.Diagnostics
                    .Where(x => x.Severity == DiagnosticSeverity.Error)
                    .Select(x => x.GetMessage()), codeForTemplate);
            }

            try
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                var bytes = memoryStream.ToArray();
                var assembly = Assembly.Load(bytes);
                var viewType = assembly.GetType("ViewEngineNamespace.NewClassView");
                var instance = Activator.CreateInstance(viewType);

                return (instance as IView) ?? new CompileErrorView(new List<string> { "Instance is null!" }, codeForTemplate);
            }
            catch (Exception e)
            {
                return new CompileErrorView(new List<string> { e.ToString() }, codeForTemplate);
            }
        }

        private static string GetBody(this string template)
        {
            Regex modelParser = new Regex(@"[^\""\s&\'\<]+");
            var operators = new List<string> { "for", "while", "if", "else", "foreach" };
            StringBuilder sb = new StringBuilder();
            StringReader sr = new StringReader(template);
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if (operators.Any(x => line.TrimStart().StartsWith("@" + x)))
                {
                    var atSignLocation = line.IndexOf("@", StringComparison.Ordinal);
                    line = line.Remove(atSignLocation, 1);
                    sb.AppendLine(line);
                }
                else if (line.TrimStart().StartsWith("{") ||
                         line.TrimStart().StartsWith("}"))
                {
                    sb.AppendLine(line);
                }
                else
                {
                    sb.Append($"html.AppendLine(@\"");

                    while (line.Contains("@"))
                    {
                        var location = line.IndexOf("@", StringComparison.Ordinal);
                        var before = line.Substring(0, location);
                        sb.Append(before.Replace("\"", "\"\"") + "\" + ");
                        var after = line.Substring(location + 1);
                        var code = modelParser.Match(after).Value;
                        sb.Append(code + " + @\"");
                        line = after.Substring(code.Length);
                    }

                    sb.AppendLine(line.Replace("\"", "\"\"") + "\");");
                }
            }

            return sb.ToString();
        }
    }
}