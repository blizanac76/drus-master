﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="Cupid", ConfigurationName="ServiceReference1.ICupid", CallbackContract=typeof(Client.ServiceReference1.ICupidCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface ICupid {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="Cupid/ICupid/GetData")]
        void GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="Cupid/ICupid/GetData")]
        System.Threading.Tasks.Task GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="Cupid/ICupid/InitSinglePerson")]
        void InitSinglePerson(WCFLib.User user);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="Cupid/ICupid/InitSinglePerson")]
        System.Threading.Tasks.Task InitSinglePersonAsync(WCFLib.User user);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="Cupid/ICupid/LetterResponse")]
        void LetterResponse(int value, string responseTo);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="Cupid/ICupid/LetterResponse")]
        System.Threading.Tasks.Task LetterResponseAsync(int value, string responseTo);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICupidCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="Cupid/ICupid/PrintShit")]
        void PrintShit(string value);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="Cupid/ICupid/InitSinglePersonR")]
        void InitSinglePersonR(bool success);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="Cupid/ICupid/Announce")]
        void Announce(string name);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="Cupid/ICupid/GetLetter")]
        void GetLetter(WCFLib.User u);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICupidChannel : Client.ServiceReference1.ICupid, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CupidClient : System.ServiceModel.DuplexClientBase<Client.ServiceReference1.ICupid>, Client.ServiceReference1.ICupid {
        
        public CupidClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public CupidClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public CupidClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public CupidClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public CupidClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void GetData(int value) {
            base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public void InitSinglePerson(WCFLib.User user) {
            base.Channel.InitSinglePerson(user);
        }
        
        public System.Threading.Tasks.Task InitSinglePersonAsync(WCFLib.User user) {
            return base.Channel.InitSinglePersonAsync(user);
        }
        
        public void LetterResponse(int value, string responseTo) {
            base.Channel.LetterResponse(value, responseTo);
        }
        
        public System.Threading.Tasks.Task LetterResponseAsync(int value, string responseTo) {
            return base.Channel.LetterResponseAsync(value, responseTo);
        }
    }
}
