namespace WebBasics.ServerFramework.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Attributes;
    using HttpServer;

    public static class FrameworkExtension
    {
        public static void Register(this List<Route> table, IServerFramework serverFramework, IServiceCollection serviceCollection)
        {
            var controllerTypes = serverFramework.GetType().Assembly.GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(Controller)));

            foreach (var controllerType in controllerTypes)
            {
                var methods = controllerType.GetMethods()
                    .Where(x => x.IsPublic && !x.IsStatic && x.DeclaringType == controllerType
                                && !x.IsAbstract && !x.IsConstructor && !x.IsSpecialName);

                foreach (var method in methods)
                {
                    var url = "/" + controllerType.Name.Replace("Controller", string.Empty)
                                  + "/" + method.Name;
                    var attribute = method.GetCustomAttributes(false)
                        .FirstOrDefault(x => x.GetType().IsSubclassOf(typeof(BaseHttpAttribute))) as BaseHttpAttribute;
                    var httpMethod = HttpMethod.Get;

                    if (attribute != null)
                    {
                        httpMethod = attribute.Method;
                    }

                    if (!string.IsNullOrEmpty(attribute?.Url))
                    {
                        url = attribute.Url;
                    }

                    table.Add(new Route(url, httpMethod, request => request.Execute(controllerType, method, serviceCollection)));
                }
            }
        }

        private static HttpResponse Execute(this HttpRequest request, Type controllerType, MethodInfo action,
            IServiceCollection serviceCollection)
        {
            var instance = serviceCollection.CreateInstance(controllerType) as Controller;
            instance.Request = request;
            var arguments = new List<object>();
            var parameters = action.GetParameters();
            foreach (var parameter in parameters)
            {
                var paramerValue = request.GetParameter(parameter.Name);
                var parameterValue = Convert.ChangeType(paramerValue, parameter.ParameterType);

                if (parameterValue == null &&
                    parameter.ParameterType != typeof(string)
                    && parameter.ParameterType != typeof(int?))
                {
                    parameterValue = Activator.CreateInstance(parameter.ParameterType);
                    var properties = parameter.ParameterType.GetProperties();

                    foreach (var property in properties)
                    {
                        var propertyValue = request.GetParameter(property.Name);
                        var propertyParameterValue = Convert.ChangeType(propertyValue, property.PropertyType);
                        property.SetValue(parameterValue, propertyParameterValue);
                    }
                }

                arguments.Add(parameterValue);
            }

            var response = action.Invoke(instance, arguments.ToArray()) as HttpResponse;
            return response;
        }

        private static string GetParameter(this HttpRequest request, string parameter)
        {
            var param = parameter.ToLower();

            if (request.FormData.Any(x => x.Key.ToLower() == param))
            {
                return request.FormData
                    .FirstOrDefault(x => x.Key.ToLower() == param).Value;
            }

            if (request.QueryData.Any(x => x.Key.ToLower() == param))
            {
                return request.QueryData
                    .FirstOrDefault(x => x.Key.ToLower() == param).Value;
            }

            return null;
        }
    }
}