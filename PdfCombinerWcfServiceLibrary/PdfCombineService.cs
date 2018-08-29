using System;

namespace PdfCombinerWcfServiceLibrary
{
    using System.IO;
    using System.Reflection;
    using System.Threading;

    using AspectInjector.Broker;

    [Inject(typeof(LoggingAspect)), DebugRoutine(@"C:\\DebugLog.txt")]
    public class PdfCombineService : IPdfCombineService
    {
        protected void SetTimeStamp()
        {
            var type = this.GetType(); // получение описания типа
            if (!Attribute.IsDefined(type, typeof(DebugRoutineAttribute)))
            {
                return;
            }

            var attributeValue = Attribute.GetCustomAttribute(type, typeof(DebugRoutineAttribute)) as DebugRoutineAttribute; // получаем значение атрибута
            if (attributeValue != null)
            {
                File.AppendAllText(attributeValue.DebugLogFileName, $"{DateTime.Now} {MethodBase.GetCurrentMethod().Name}\n"); // используем значение атрибута для формирования результата
            }
        }

        [Obsolete("Тестовая процедура")]
        public string GetData(int value)
        {
            this.SetTimeStamp();
            Thread.Sleep(1000);
            return $"Input value is : {value}";
        }

        [Obsolete("Тестовая процедура")]
        public string GetDataLongTime(int value)
        {
            Thread.Sleep(10000);
            return $"Input value is : {value}";
        }

        [Obsolete("Тестовая процедура")]
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException(nameof(composite));
            }
            if (composite.BoolValue)
            {
                composite.BoolValue = !composite.BoolValue;
                composite.StringValue += " Suffix";
            }

            return composite;
        }
    }
}
