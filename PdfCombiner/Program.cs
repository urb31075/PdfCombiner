// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="urb31075">
// All Right Reserved  
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PdfCombiner
{
    using System;
    using System.Runtime.CompilerServices;

    using MyPdfServiceReference;

    using PdfCombinerLibrary;

    using PdfCombinerWcf;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            var pdfCombinerCommunication = new PdfCombinerCommunication();

            var result1 = pdfCombinerCommunication.GetData();
            var result2 = pdfCombinerCommunication.GetData(15);
            var result3 = pdfCombinerCommunication.GetData(25, "xxx");
            Console.WriteLine(result1);
            Console.WriteLine(result2);
            Console.WriteLine(result3);
            Console.ReadLine();
        }
    }
}
