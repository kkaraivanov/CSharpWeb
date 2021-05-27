namespace ChronometerApp
{
    using System;
    using System.Linq;
    using System.Reflection;
    using CommandModels;

    public static class HelperExtensions
    {
        public static void SetCommand(this IChronometer chronometer, string command)
        {
            var curr = command.ToLower();
            var model = command.Replace(curr[0], char.ToUpper(curr[0])) + "Command";
            var type = Assembly.GetEntryAssembly().GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(ICommandModel)))
                .FirstOrDefault(t => t.Name == model);
            var method = type?.GetMethod("Execute");

            if (type != null && method != null)
            {
                var obj = Activator.CreateInstance(type);
                method.Invoke(obj, new object?[] { chronometer });
            }
        }
    }
}