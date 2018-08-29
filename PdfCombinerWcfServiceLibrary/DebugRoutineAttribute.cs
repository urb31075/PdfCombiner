// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DebugRoutineAttribute.cs" company="urb31075">
//  All Right Reserved 
// </copyright>
// <summary>
//   Defines the DubugRoutineAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PdfCombinerWcfServiceLibrary
{
    using System;

    /// <summary>
    /// The dubug routine attribute.
    /// </summary>
    public class DebugRoutineAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DebugRoutineAttribute"/> class.
        /// </summary>
        /// <param name="debugLogFileName">
        /// The dubug log file name.
        /// </param>
        public DebugRoutineAttribute(string debugLogFileName)
        {
            this.DebugLogFileName = debugLogFileName;
        }

        /// <summary>
        /// Gets or sets the dubug log file name.
        /// </summary>
        public string DebugLogFileName { get; set; }
    }
}
