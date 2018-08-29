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
    using MyPdfServiceReference;

    using PdfCombinerWcfServiceLibrary;

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
            var pdfCombineService = new PdfCombineService();
            var result = pdfCombineService.GetDataLongTime(25);
            Console.WriteLine("Reciwe data");
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
