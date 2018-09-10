using System;

namespace PdfCombinerLibrary
{
    using System.Threading;

    public class DebugRoutine
    {
        public int Sqrt(int x)
        {
            return x;
        }
        public string GetData(int value)
        {
            Thread.Sleep(100);
            return $"Input value is : {value}";
        }

        public string GetDataLongTime(int value)
        {
            Thread.Sleep(10000);
            return $"Input value is : {value}";
        }

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

        public class CompositeType
        {
            public bool BoolValue { get; set; }

            public string StringValue { get; set; }
        }
    }
}
