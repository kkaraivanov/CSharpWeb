namespace WebBasics.ServerFramework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IServiceCollection
    {
        void Add<TSource, TDestination>();

        object CreateInstance(Type type);
    }

    class ServiceCollection : IServiceCollection
    {
        private readonly Dictionary<Type, Type> _dependencyContainer = new Dictionary<Type, Type>();

        public void Add<TSource, TDestination>()
        {
            this._dependencyContainer[typeof(TSource)] = typeof(TDestination);
        }

        public object CreateInstance(Type type)
        {
            if (this._dependencyContainer.ContainsKey(type))
            {
                type = this._dependencyContainer[type];
            }

            var constructor = type.GetConstructors()
                .OrderBy(x => x.GetParameters().Count()).FirstOrDefault();

            var parameters = constructor.GetParameters();
            var parameterValues = new List<object>();
            foreach (var parameter in parameters)
            {
                var parameterValue = CreateInstance(parameter.ParameterType);
                parameterValues.Add(parameterValue);
            }

            var obj = constructor.Invoke(parameterValues.ToArray());
            return obj;
        }
    }
}