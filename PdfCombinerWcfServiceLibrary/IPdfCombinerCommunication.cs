﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPdfCombinerWcf.cs" company="urb31075">
// All Right Reserved  
// </copyright>
// <summary>
//   Defines the IPdfCombineService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PdfCombinerWcf
{
    using System;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using PdfCombinerLibrary;

    /// <summary>
    /// The PdfCombineService interface.
    /// </summary>
    [ServiceContract, DebugExportable]
    public interface IPdfCombinerCommunication
    {
        /// <summary>
        /// The get data.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        /// <summary>
        /// The get data long time.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetDataLongTime(int value);

        [OperationContract]
        /// <summary>
        /// The get data using data contract.
        /// </summary>
        /// <param name="composite">
        /// The composite.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeType"/>.
        /// </returns>
        CompositeType GetDataUsingDataContract(CompositeType composite);
    }

    /// <summary>
    /// The composite type.
    /// </summary>
    [DataContract]
    public class CompositeType
    {
        /// <summary>
        /// Gets or sets a value indicating whether bool value.
        /// </summary>
        [DataMember]
        public bool BoolValue { get; set; }

        /// <summary>
        /// Gets or sets the string value.
        /// </summary>
        [DataMember]
        public string StringValue { get; set; }

        /// <summary>
        /// The op_ explicit.
        /// </summary>
        /// <param name="v">
        /// The v.
        /// </param>
        /// <returns>
        /// </returns>
        public static explicit operator CompositeType(DebugRoutine.CompositeType v)
        {
            return new CompositeType { BoolValue = v.BoolValue, StringValue = v.StringValue }; 
        }

        /// <summary>
        /// The op_ explicit.
        /// </summary>
        /// <param name="v">
        /// The v.
        /// </param>
        /// <returns>
        /// </returns>
        public static explicit operator DebugRoutine.CompositeType(CompositeType v)
        {
            return new DebugRoutine.CompositeType { BoolValue = v.BoolValue, StringValue = v.StringValue };
        }
    }
}
