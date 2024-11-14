﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Application.BookFlightServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BookFlightServiceReference.IBookFlight")]
    public interface IBookFlight {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBookFlight/BookFlightFunction", ReplyAction="http://tempuri.org/IBookFlight/BookFlightFunctionResponse")]
        string BookFlightFunction(int time, string depart, string arrival);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBookFlight/BookFlightFunction", ReplyAction="http://tempuri.org/IBookFlight/BookFlightFunctionResponse")]
        System.Threading.Tasks.Task<string> BookFlightFunctionAsync(int time, string depart, string arrival);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBookFlightChannel : Application.BookFlightServiceReference.IBookFlight, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BookFlightClient : System.ServiceModel.ClientBase<Application.BookFlightServiceReference.IBookFlight>, Application.BookFlightServiceReference.IBookFlight {
        
        public BookFlightClient() {
        }
        
        public BookFlightClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BookFlightClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BookFlightClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BookFlightClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string BookFlightFunction(int time, string depart, string arrival) {
            return base.Channel.BookFlightFunction(time, depart, arrival);
        }
        
        public System.Threading.Tasks.Task<string> BookFlightFunctionAsync(int time, string depart, string arrival) {
            return base.Channel.BookFlightFunctionAsync(time, depart, arrival);
        }
    }
}
