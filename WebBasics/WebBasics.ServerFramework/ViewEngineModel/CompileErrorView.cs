namespace WebBasics.ServerFramework.ViewEngineModel
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CompileErrorView : IView
    {
        private readonly IEnumerable<string> _errors;
        private readonly string _codeForTemplate;

        public CompileErrorView(IEnumerable<string> errors, string codeForTemplate)
        {
            _errors = errors;
            _codeForTemplate = codeForTemplate;
        }

        public string Execute(object viewModel, string user)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"<h1>View compile {_errors.Count()} errors:</h1><ul>");
            foreach (var error in _errors)
            {
                sb.AppendLine($"<li>{error}</li>");
            }

            sb.AppendLine($"</ul><pre>{_codeForTemplate}</pre>");
            return sb.ToString();
        }
    }
}