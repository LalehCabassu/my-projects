﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProtocolTester.wfssWebAPI {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfString", Namespace="http://tempuri.org/", ItemName="string")]
    [System.SerializableAttribute()]
    public class ArrayOfString : System.Collections.Generic.List<string> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfInt", Namespace="http://tempuri.org/", ItemName="int")]
    [System.SerializableAttribute()]
    public class ArrayOfInt : System.Collections.Generic.List<int> {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="wfssWebAPI.WFStatsSoap")]
    public interface WFStatsSoap {
        
        // CODEGEN: Generating message contract since element name fightManagerId from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Register", ReplyAction="*")]
        ProtocolTester.wfssWebAPI.RegisterResponse Register(ProtocolTester.wfssWebAPI.RegisterRequest request);
        
        // CODEGEN: Generating message contract since element name fightManagerId from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/LogNewGame", ReplyAction="*")]
        ProtocolTester.wfssWebAPI.LogNewGameResponse LogNewGame(ProtocolTester.wfssWebAPI.LogNewGameRequest request);
        
        // CODEGEN: Generating message contract since element name fightManagerId from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/LogGameStats", ReplyAction="*")]
        ProtocolTester.wfssWebAPI.LogGameStatsResponse LogGameStats(ProtocolTester.wfssWebAPI.LogGameStatsRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetServerTime", ReplyAction="*")]
        System.DateTime GetServerTime();
        
        // CODEGEN: Generating message contract since element name GetRegisterFightManagersResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetRegisterFightManagers", ReplyAction="*")]
        ProtocolTester.wfssWebAPI.GetRegisterFightManagersResponse GetRegisterFightManagers(ProtocolTester.wfssWebAPI.GetRegisterFightManagersRequest request);
        
        // CODEGEN: Generating message contract since element name fightManagerId from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetGameIds", ReplyAction="*")]
        ProtocolTester.wfssWebAPI.GetGameIdsResponse GetGameIds(ProtocolTester.wfssWebAPI.GetGameIdsRequest request);
        
        // CODEGEN: Generating message contract since element name fightManagerId from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetGameStats", ReplyAction="*")]
        ProtocolTester.wfssWebAPI.GetGameStatsResponse GetGameStats(ProtocolTester.wfssWebAPI.GetGameStatsRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RegisterRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Register", Namespace="http://tempuri.org/", Order=0)]
        public ProtocolTester.wfssWebAPI.RegisterRequestBody Body;
        
        public RegisterRequest() {
        }
        
        public RegisterRequest(ProtocolTester.wfssWebAPI.RegisterRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class RegisterRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string fightManagerId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string fightManagerName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string operatorName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string operatorEmail;
        
        public RegisterRequestBody() {
        }
        
        public RegisterRequestBody(string fightManagerId, string fightManagerName, string operatorName, string operatorEmail) {
            this.fightManagerId = fightManagerId;
            this.fightManagerName = fightManagerName;
            this.operatorName = operatorName;
            this.operatorEmail = operatorEmail;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RegisterResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RegisterResponse", Namespace="http://tempuri.org/", Order=0)]
        public ProtocolTester.wfssWebAPI.RegisterResponseBody Body;
        
        public RegisterResponse() {
        }
        
        public RegisterResponse(ProtocolTester.wfssWebAPI.RegisterResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class RegisterResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string RegisterResult;
        
        public RegisterResponseBody() {
        }
        
        public RegisterResponseBody(string RegisterResult) {
            this.RegisterResult = RegisterResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class LogNewGameRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="LogNewGame", Namespace="http://tempuri.org/", Order=0)]
        public ProtocolTester.wfssWebAPI.LogNewGameRequestBody Body;
        
        public LogNewGameRequest() {
        }
        
        public LogNewGameRequest(ProtocolTester.wfssWebAPI.LogNewGameRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class LogNewGameRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string fightManagerId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int gameId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public System.DateTime start;
        
        public LogNewGameRequestBody() {
        }
        
        public LogNewGameRequestBody(string fightManagerId, int gameId, System.DateTime start) {
            this.fightManagerId = fightManagerId;
            this.gameId = gameId;
            this.start = start;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class LogNewGameResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="LogNewGameResponse", Namespace="http://tempuri.org/", Order=0)]
        public ProtocolTester.wfssWebAPI.LogNewGameResponseBody Body;
        
        public LogNewGameResponse() {
        }
        
        public LogNewGameResponse(ProtocolTester.wfssWebAPI.LogNewGameResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class LogNewGameResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string LogNewGameResult;
        
        public LogNewGameResponseBody() {
        }
        
        public LogNewGameResponseBody(string LogNewGameResult) {
            this.LogNewGameResult = LogNewGameResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class LogGameStatsRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="LogGameStats", Namespace="http://tempuri.org/", Order=0)]
        public ProtocolTester.wfssWebAPI.LogGameStatsRequestBody Body;
        
        public LogGameStatsRequest() {
        }
        
        public LogGameStatsRequest(ProtocolTester.wfssWebAPI.LogGameStatsRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class LogGameStatsRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string fightManagerId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int gameId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public Server.wfssWebAPI.GameStats stats;
        
        public LogGameStatsRequestBody() {
        }
        
        public LogGameStatsRequestBody(string fightManagerId, int gameId, Server.wfssWebAPI.GameStats stats) {
            this.fightManagerId = fightManagerId;
            this.gameId = gameId;
            this.stats = stats;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class LogGameStatsResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="LogGameStatsResponse", Namespace="http://tempuri.org/", Order=0)]
        public ProtocolTester.wfssWebAPI.LogGameStatsResponseBody Body;
        
        public LogGameStatsResponse() {
        }
        
        public LogGameStatsResponse(ProtocolTester.wfssWebAPI.LogGameStatsResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class LogGameStatsResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string LogGameStatsResult;
        
        public LogGameStatsResponseBody() {
        }
        
        public LogGameStatsResponseBody(string LogGameStatsResult) {
            this.LogGameStatsResult = LogGameStatsResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetRegisterFightManagersRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetRegisterFightManagers", Namespace="http://tempuri.org/", Order=0)]
        public ProtocolTester.wfssWebAPI.GetRegisterFightManagersRequestBody Body;
        
        public GetRegisterFightManagersRequest() {
        }
        
        public GetRegisterFightManagersRequest(ProtocolTester.wfssWebAPI.GetRegisterFightManagersRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class GetRegisterFightManagersRequestBody {
        
        public GetRegisterFightManagersRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetRegisterFightManagersResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetRegisterFightManagersResponse", Namespace="http://tempuri.org/", Order=0)]
        public ProtocolTester.wfssWebAPI.GetRegisterFightManagersResponseBody Body;
        
        public GetRegisterFightManagersResponse() {
        }
        
        public GetRegisterFightManagersResponse(ProtocolTester.wfssWebAPI.GetRegisterFightManagersResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetRegisterFightManagersResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ProtocolTester.wfssWebAPI.ArrayOfString GetRegisterFightManagersResult;
        
        public GetRegisterFightManagersResponseBody() {
        }
        
        public GetRegisterFightManagersResponseBody(ProtocolTester.wfssWebAPI.ArrayOfString GetRegisterFightManagersResult) {
            this.GetRegisterFightManagersResult = GetRegisterFightManagersResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetGameIdsRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetGameIds", Namespace="http://tempuri.org/", Order=0)]
        public ProtocolTester.wfssWebAPI.GetGameIdsRequestBody Body;
        
        public GetGameIdsRequest() {
        }
        
        public GetGameIdsRequest(ProtocolTester.wfssWebAPI.GetGameIdsRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetGameIdsRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string fightManagerId;
        
        public GetGameIdsRequestBody() {
        }
        
        public GetGameIdsRequestBody(string fightManagerId) {
            this.fightManagerId = fightManagerId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetGameIdsResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetGameIdsResponse", Namespace="http://tempuri.org/", Order=0)]
        public ProtocolTester.wfssWebAPI.GetGameIdsResponseBody Body;
        
        public GetGameIdsResponse() {
        }
        
        public GetGameIdsResponse(ProtocolTester.wfssWebAPI.GetGameIdsResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetGameIdsResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ProtocolTester.wfssWebAPI.ArrayOfInt GetGameIdsResult;
        
        public GetGameIdsResponseBody() {
        }
        
        public GetGameIdsResponseBody(ProtocolTester.wfssWebAPI.ArrayOfInt GetGameIdsResult) {
            this.GetGameIdsResult = GetGameIdsResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetGameStatsRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetGameStats", Namespace="http://tempuri.org/", Order=0)]
        public ProtocolTester.wfssWebAPI.GetGameStatsRequestBody Body;
        
        public GetGameStatsRequest() {
        }
        
        public GetGameStatsRequest(ProtocolTester.wfssWebAPI.GetGameStatsRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetGameStatsRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string fightManagerId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int gameId;
        
        public GetGameStatsRequestBody() {
        }
        
        public GetGameStatsRequestBody(string fightManagerId, int gameId) {
            this.fightManagerId = fightManagerId;
            this.gameId = gameId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetGameStatsResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetGameStatsResponse", Namespace="http://tempuri.org/", Order=0)]
        public ProtocolTester.wfssWebAPI.GetGameStatsResponseBody Body;
        
        public GetGameStatsResponse() {
        }
        
        public GetGameStatsResponse(ProtocolTester.wfssWebAPI.GetGameStatsResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetGameStatsResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Server.wfssWebAPI.GameStats[] GetGameStatsResult;
        
        public GetGameStatsResponseBody() {
        }
        
        public GetGameStatsResponseBody(Server.wfssWebAPI.GameStats[] GetGameStatsResult) {
            this.GetGameStatsResult = GetGameStatsResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WFStatsSoapChannel : ProtocolTester.wfssWebAPI.WFStatsSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WFStatsSoapClient : System.ServiceModel.ClientBase<ProtocolTester.wfssWebAPI.WFStatsSoap>, ProtocolTester.wfssWebAPI.WFStatsSoap {
        
        public WFStatsSoapClient() {
        }
        
        public WFStatsSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WFStatsSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WFStatsSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WFStatsSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ProtocolTester.wfssWebAPI.RegisterResponse ProtocolTester.wfssWebAPI.WFStatsSoap.Register(ProtocolTester.wfssWebAPI.RegisterRequest request) {
            return base.Channel.Register(request);
        }
        
        public string Register(string fightManagerId, string fightManagerName, string operatorName, string operatorEmail) {
            ProtocolTester.wfssWebAPI.RegisterRequest inValue = new ProtocolTester.wfssWebAPI.RegisterRequest();
            inValue.Body = new ProtocolTester.wfssWebAPI.RegisterRequestBody();
            inValue.Body.fightManagerId = fightManagerId;
            inValue.Body.fightManagerName = fightManagerName;
            inValue.Body.operatorName = operatorName;
            inValue.Body.operatorEmail = operatorEmail;
            ProtocolTester.wfssWebAPI.RegisterResponse retVal = ((ProtocolTester.wfssWebAPI.WFStatsSoap)(this)).Register(inValue);
            return retVal.Body.RegisterResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ProtocolTester.wfssWebAPI.LogNewGameResponse ProtocolTester.wfssWebAPI.WFStatsSoap.LogNewGame(ProtocolTester.wfssWebAPI.LogNewGameRequest request) {
            return base.Channel.LogNewGame(request);
        }
        
        public string LogNewGame(string fightManagerId, int gameId, System.DateTime start) {
            ProtocolTester.wfssWebAPI.LogNewGameRequest inValue = new ProtocolTester.wfssWebAPI.LogNewGameRequest();
            inValue.Body = new ProtocolTester.wfssWebAPI.LogNewGameRequestBody();
            inValue.Body.fightManagerId = fightManagerId;
            inValue.Body.gameId = gameId;
            inValue.Body.start = start;
            ProtocolTester.wfssWebAPI.LogNewGameResponse retVal = ((ProtocolTester.wfssWebAPI.WFStatsSoap)(this)).LogNewGame(inValue);
            return retVal.Body.LogNewGameResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ProtocolTester.wfssWebAPI.LogGameStatsResponse ProtocolTester.wfssWebAPI.WFStatsSoap.LogGameStats(ProtocolTester.wfssWebAPI.LogGameStatsRequest request) {
            return base.Channel.LogGameStats(request);
        }
        
        public string LogGameStats(string fightManagerId, int gameId, Server.wfssWebAPI.GameStats stats) {
            ProtocolTester.wfssWebAPI.LogGameStatsRequest inValue = new ProtocolTester.wfssWebAPI.LogGameStatsRequest();
            inValue.Body = new ProtocolTester.wfssWebAPI.LogGameStatsRequestBody();
            inValue.Body.fightManagerId = fightManagerId;
            inValue.Body.gameId = gameId;
            inValue.Body.stats = stats;
            ProtocolTester.wfssWebAPI.LogGameStatsResponse retVal = ((ProtocolTester.wfssWebAPI.WFStatsSoap)(this)).LogGameStats(inValue);
            return retVal.Body.LogGameStatsResult;
        }
        
        public System.DateTime GetServerTime() {
            return base.Channel.GetServerTime();
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ProtocolTester.wfssWebAPI.GetRegisterFightManagersResponse ProtocolTester.wfssWebAPI.WFStatsSoap.GetRegisterFightManagers(ProtocolTester.wfssWebAPI.GetRegisterFightManagersRequest request) {
            return base.Channel.GetRegisterFightManagers(request);
        }
        
        public ProtocolTester.wfssWebAPI.ArrayOfString GetRegisterFightManagers() {
            ProtocolTester.wfssWebAPI.GetRegisterFightManagersRequest inValue = new ProtocolTester.wfssWebAPI.GetRegisterFightManagersRequest();
            inValue.Body = new ProtocolTester.wfssWebAPI.GetRegisterFightManagersRequestBody();
            ProtocolTester.wfssWebAPI.GetRegisterFightManagersResponse retVal = ((ProtocolTester.wfssWebAPI.WFStatsSoap)(this)).GetRegisterFightManagers(inValue);
            return retVal.Body.GetRegisterFightManagersResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ProtocolTester.wfssWebAPI.GetGameIdsResponse ProtocolTester.wfssWebAPI.WFStatsSoap.GetGameIds(ProtocolTester.wfssWebAPI.GetGameIdsRequest request) {
            return base.Channel.GetGameIds(request);
        }
        
        public ProtocolTester.wfssWebAPI.ArrayOfInt GetGameIds(string fightManagerId) {
            ProtocolTester.wfssWebAPI.GetGameIdsRequest inValue = new ProtocolTester.wfssWebAPI.GetGameIdsRequest();
            inValue.Body = new ProtocolTester.wfssWebAPI.GetGameIdsRequestBody();
            inValue.Body.fightManagerId = fightManagerId;
            ProtocolTester.wfssWebAPI.GetGameIdsResponse retVal = ((ProtocolTester.wfssWebAPI.WFStatsSoap)(this)).GetGameIds(inValue);
            return retVal.Body.GetGameIdsResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ProtocolTester.wfssWebAPI.GetGameStatsResponse ProtocolTester.wfssWebAPI.WFStatsSoap.GetGameStats(ProtocolTester.wfssWebAPI.GetGameStatsRequest request) {
            return base.Channel.GetGameStats(request);
        }
        
        public Server.wfssWebAPI.GameStats[] GetGameStats(string fightManagerId, int gameId) {
            ProtocolTester.wfssWebAPI.GetGameStatsRequest inValue = new ProtocolTester.wfssWebAPI.GetGameStatsRequest();
            inValue.Body = new ProtocolTester.wfssWebAPI.GetGameStatsRequestBody();
            inValue.Body.fightManagerId = fightManagerId;
            inValue.Body.gameId = gameId;
            ProtocolTester.wfssWebAPI.GetGameStatsResponse retVal = ((ProtocolTester.wfssWebAPI.WFStatsSoap)(this)).GetGameStats(inValue);
            return retVal.Body.GetGameStatsResult;
        }
    }
}
