namespace PdfCombinerWcf
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
        public object HandleMethod(
            [Advice.Argument(Advice.Argument.Source.Type)] Type type,
            [Advice.Argument(Advice.Argument.Source.Name)] string name,
            [Advice.Argument(Advice.Argument.Source.ReturnType)] Type returnType,
            [Advice.Argument(Advice.Argument.Source.Method)] Type method,
            [Advice.Argument(Advice.Argument.Source.Arguments)] object[] arguments,
            [Advice.Argument(Advice.Argument.Source.Target)] Func<object[], object> target)
        {
            var sw = Stopwatch.StartNew();
            var result = target(arguments);
            sw.Stop();

            var methodFullName = method.ToString();
            var logFileName = string.Empty;
            var logMode = false;
            var exportable = false;

            if (method.DeclaringType != null)
            {
                // Проход по атрибутам класса
                foreach (var customAttributes in method.DeclaringType.CustomAttributes)
                {
                    if (customAttributes.AttributeType.FullName == typeof(DebugLogFileNameAttribute).FullName)
                    {
                        logFileName = customAttributes.ConstructorArguments[0].Value.ToString();
                    }

                    if (customAttributes.AttributeType.FullName == typeof(DebugLogModeAttribute).FullName)
                    {
                        logMode = Convert.ToBoolean(customAttributes.ConstructorArguments[0].Value);
                    }
                }

                
                var declaringType = (TypeInfo)method.DeclaringType;
                if (declaringType != null)
                {
                    var implementedInterfaces = declaringType.ImplementedInterfaces;
                    foreach (var type1 in implementedInterfaces)
                    {
                        var implementedInterface = (TypeInfo)type1;
                        //Проход по атрибутам интерфейса
                        foreach (var customAttribute in implementedInterface.CustomAttributes)
                        {
                            if (customAttribute.AttributeType.FullName == typeof(DebugExportableAttribute).FullName)
                            {
                                exportable = true;
                            }
                        }
                        
                        //Проход по атрибутам методов интерфейса
                        foreach (var declaredMethod in implementedInterface.DeclaredMethods.Where(m => m.ToString() == methodFullName))
                        {
                            foreach (var customAttribute in declaredMethod.CustomAttributes)
                            {
                                if (customAttribute.AttributeType.FullName == typeof(DebugExportableAttribute).FullName)
                                {
                                    exportable = true;
                                }
                            }
                        }
                    }
                }
            }

            // Проход по методам класса
            foreach (var customAttributes in method.CustomAttributes)
                {
                    if (customAttributes.AttributeType.FullName == typeof(DebugLogFileNameAttribute).FullName)
                    {
                        logFileName = customAttributes.ConstructorArguments[0].Value.ToString();
                    }

                    if (customAttributes.AttributeType.FullName == typeof(DebugLogModeAttribute).FullName)
                    {
                        logMode = Convert.ToBoolean(customAttributes.ConstructorArguments[0].Value);
                    }
                }

            if (logMode)
            {
                var argumentStr = arguments.Aggregate($"Execute method: {name}(", (current, arg) => current + (arg + ", ")).Trim().Trim(',') + ")";
                if (!string.IsNullOrEmpty(logFileName))
                {
                    File.AppendAllText(logFileName, $"{DateTime.Now} {argumentStr} in {sw.ElapsedMilliseconds} ms\n"); // используем значение атрибута для формирования результата
                }
            }

            if (exportable)
            {
                File.AppendAllText(logFileName, $"{DateTime.Now} Method {methodFullName} is Exportable!\n");
            }
            return result;
        }
    }
}
