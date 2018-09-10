// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DebugLogFileNameAttribute.cs" company="urb31075">
//   All Right Reserved
// </copyright>
// <summary>
//   Defines the DubugRoutineAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PdfCombinerWcf
{
    using System;

    /// <summary>
    /// The dubug routine attribute.
    /// </summary>
    public class DebugLogModeAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DebugLogModeAttribute"/> class. 
        /// </summary>
        /// <param name="debugLogMode">
        /// The dubug log file name.
        /// </param>
        public DebugLogModeAttribute(bool debugLogMode)
        {
            this.DebugLogMode = debugLogMode;
        }

        /// <summary>
        /// Gets or sets a value indicating whether debug log mode.
        /// </summary>
        public bool DebugLogMode { get; set; }
    }
}