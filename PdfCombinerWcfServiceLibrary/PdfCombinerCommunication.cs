namespace PdfCombinerWcf
{
    using System;
    using System.IO;
    using System.Reflection;

    using AspectInjector.Broker;

    using PdfCombinerLibrary;

    /// <summary>
    /// The pdf combiner communication.
    /// </summary>
    [Inject(typeof(LoggingAspect))]
    [DebugLogFileName(@"C:\\DebugLogForClass.txt"), DebugLogMode(true)]
    public class PdfCombinerCommunication : IPdfCombinerCommunication
    {
        /// <summary>
        /// The get data.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetData()
        {
            return "XXXXX";
        }

        /// <summary>
        /// The get data.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [DebugLogFileName(@"C:\\DebugLogInt.txt"), DebugLogMode(true)]
        public string GetData(int value)
        {
            return new DebugRoutine().GetData(value); 
        }

        /// <summary>
        /// The get data.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="additional">
        /// The additional.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [DebugLogFileName(@"C:\\DebugLogString.txt"), DebugLogMode(true)]
        public string GetData(int value, string additional)
        {
            return new DebugRoutine().GetData(value);
        }

        public string GetDataLongTime(int value)
        {
            return new DebugRoutine().GetDataLongTime(value); 
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            var result = new DebugRoutine().GetDataUsingDataContract((DebugRoutine.CompositeType)composite);
            return (CompositeType)result;
        }
    }
}
