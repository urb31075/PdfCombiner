﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PdfCombinerDebug.MyPdfCombineService {
    using PdfCombinerWcf;

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MyPdfCombineService.IPdfCombineService")]
    public interface IPdfCombineService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPdfCombineService/GetData", ReplyAction="http://tempuri.org/IPdfCombineService/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPdfCombineService/GetData", ReplyAction="http://tempuri.org/IPdfCombineService/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPdfCombineService/GetDataLongTime", ReplyAction="http://tempuri.org/IPdfCombineService/GetDataLongTimeResponse")]
        string GetDataLongTime(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPdfCombineService/GetDataLongTime", ReplyAction="http://tempuri.org/IPdfCombineService/GetDataLongTimeResponse")]
        System.Threading.Tasks.Task<string> GetDataLongTimeAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPdfCombineService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IPdfCombineService/GetDataUsingDataContractResponse")]
        CompositeType GetDataUsingDataContract(CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPdfCombineService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IPdfCombineService/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<CompositeType> GetDataUsingDataContractAsync(CompositeType composite);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPdfCombineServiceChannel : PdfCombinerDebug.MyPdfCombineService.IPdfCombineService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PdfCombineServiceClient : System.ServiceModel.ClientBase<PdfCombinerDebug.MyPdfCombineService.IPdfCombineService>, PdfCombinerDebug.MyPdfCombineService.IPdfCombineService {
        
        public PdfCombineServiceClient() {
        }
        
        public PdfCombineServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PdfCombineServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PdfCombineServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PdfCombineServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public string GetDataLongTime(int value) {
            return base.Channel.GetDataLongTime(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataLongTimeAsync(int value) {
            return base.Channel.GetDataLongTimeAsync(value);
        }
        
        public CompositeType GetDataUsingDataContract(CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<CompositeType> GetDataUsingDataContractAsync(CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
    }
}
