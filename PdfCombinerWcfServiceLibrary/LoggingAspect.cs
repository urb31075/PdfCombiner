namespace PdfCombinerWcfServiceLibrary
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using AspectInjector.Broker;

    [Aspect(Aspect.Scope.Global)]
    class LoggingAspect
    {
        [Advice(Advice.Type.Around, Advice.Target.Method)]
        public object HandleMethod([Advice.Argument(Advice.Argument.Source.Name)] string name,
            [Advice.Argument(Advice.Argument.Source.Arguments)] object[] arguments,
            [Advice.Argument(Advice.Argument.Source.Target)] Func<object[], object> method)
        {
            var argumentStr = arguments.Aggregate($"Executing method: {name}(", (current, arg) => current + (arg + ", ")).Trim().Trim(',') + ")"; ;
            var sw = Stopwatch.StartNew();
            var result = method(arguments);
            sw.Stop();
            File.AppendAllText(@"C:\AspectLog.txt", $"{DateTime.Now} {argumentStr} in {sw.ElapsedMilliseconds} ms\n"); // используем значение атрибута для формирования результата
            return result;
        }
    }
}
