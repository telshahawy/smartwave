﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EzagelSmsServive
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="EzagelSmsServive.ServiceSoap")]
    public interface ServiceSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Get_All_Providers", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<EzagelSmsServive.Get_All_ProvidersResponse> Get_All_ProvidersAsync(EzagelSmsServive.Get_All_ProvidersRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Update_Bloked_Mobiles", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<bool> Update_Bloked_MobilesAsync(string ACtiveCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Send_SMS", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> Send_SMSAsync(string Msg_ID, string Mobile_NO, string Body, string Validty, string StartTime, string Sender, string User, string Password, string Service);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Send_SMSOverloaded", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<EzagelSmsServive.Send_SMSOverloaded1> Send_SMS1Async(EzagelSmsServive.Send_SMSOverloaded request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Send_SMS_Port", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> Send_SMS_PortAsync(string Msg_ID, string Mobile_NO, string Body, string Validty, string StartTime, string Sender, string User, string Password, string Service, int SMS_Port);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Send_SMSWithoutResponse", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<EzagelSmsServive.Send_SMSWithoutResponse1> Send_SMS_Without_ResponseAsync(EzagelSmsServive.Send_SMSWithoutResponse request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Send_Arr_SMS", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<EzagelSmsServive.Send_Arr_SMS1> Send_ArrSMSAsync(EzagelSmsServive.Send_Arr_SMS request);
        
        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Authenticate_Email", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<EzagelSmsServive.Authenticate_EmailResponse> Authenticate_EmailAsync(EzagelSmsServive.Authenticate_EmailRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Upload_File", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> Upload_FileAsync(string File_Name, string Body, string Validty, string Sender, string StartTime, int EndTime, int Channel_ID, string User, string Password);
        
        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SMS_Count", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<EzagelSmsServive.SMS_CountResponse> SMS_CountAsync(EzagelSmsServive.SMS_CountRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Get_Account_Credit", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<int> Get_Account_CreditAsync(string Account_Username, string Account_Password);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class Provider
    {
        
        private int provider_IDField;
        
        private int user_IDField;
        
        private string provider_NameField;
        
        private string prefixField;
        
        private bool file_SpoolField;
        
        private string spool_PathField;
        
        private string receive_PathField;
        
        private string force_SenderField;
        
        private string sent_PathField;
        
        private string bad_PathField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int Provider_ID
        {
            get
            {
                return this.provider_IDField;
            }
            set
            {
                this.provider_IDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int User_ID
        {
            get
            {
                return this.user_IDField;
            }
            set
            {
                this.user_IDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Provider_Name
        {
            get
            {
                return this.provider_NameField;
            }
            set
            {
                this.provider_NameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string Prefix
        {
            get
            {
                return this.prefixField;
            }
            set
            {
                this.prefixField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public bool File_Spool
        {
            get
            {
                return this.file_SpoolField;
            }
            set
            {
                this.file_SpoolField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string Spool_Path
        {
            get
            {
                return this.spool_PathField;
            }
            set
            {
                this.spool_PathField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string Receive_Path
        {
            get
            {
                return this.receive_PathField;
            }
            set
            {
                this.receive_PathField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string Force_Sender
        {
            get
            {
                return this.force_SenderField;
            }
            set
            {
                this.force_SenderField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string Sent_Path
        {
            get
            {
                return this.sent_PathField;
            }
            set
            {
                this.sent_PathField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public string Bad_Path
        {
            get
            {
                return this.bad_PathField;
            }
            set
            {
                this.bad_PathField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class SMSBulk
    {
        
        private string msg_IDField;
        
        private string mobile_NOField;
        
        private string bodyField;
        
        private string validtyField;
        
        private string startTimeField;
        
        private string senderField;
        
        private string userField;
        
        private string passwordField;
        
        private string serviceField;
        
        private int file_IDField;
        
        private int channel_IDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Msg_ID
        {
            get
            {
                return this.msg_IDField;
            }
            set
            {
                this.msg_IDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Mobile_NO
        {
            get
            {
                return this.mobile_NOField;
            }
            set
            {
                this.mobile_NOField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Body
        {
            get
            {
                return this.bodyField;
            }
            set
            {
                this.bodyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string Validty
        {
            get
            {
                return this.validtyField;
            }
            set
            {
                this.validtyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string StartTime
        {
            get
            {
                return this.startTimeField;
            }
            set
            {
                this.startTimeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string Sender
        {
            get
            {
                return this.senderField;
            }
            set
            {
                this.senderField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string User
        {
            get
            {
                return this.userField;
            }
            set
            {
                this.userField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string Password
        {
            get
            {
                return this.passwordField;
            }
            set
            {
                this.passwordField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string Service
        {
            get
            {
                return this.serviceField;
            }
            set
            {
                this.serviceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public int File_ID
        {
            get
            {
                return this.file_IDField;
            }
            set
            {
                this.file_IDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public int Channel_ID
        {
            get
            {
                return this.channel_IDField;
            }
            set
            {
                this.channel_IDField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Get_All_Providers", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Get_All_ProvidersRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string ACtiveCode;
        
        public Get_All_ProvidersRequest()
        {
        }
        
        public Get_All_ProvidersRequest(string ACtiveCode)
        {
            this.ACtiveCode = ACtiveCode;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Get_All_ProvidersResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Get_All_ProvidersResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public EzagelSmsServive.Provider[] Get_All_ProvidersResult;
        
        public Get_All_ProvidersResponse()
        {
        }
        
        public Get_All_ProvidersResponse(EzagelSmsServive.Provider[] Get_All_ProvidersResult)
        {
            this.Get_All_ProvidersResult = Get_All_ProvidersResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Send_SMSOverloaded", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Send_SMSOverloaded
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string Msg_ID;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string Mobile_NO;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public string Body;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=3)]
        public string Validty;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=4)]
        public string StartTime;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=5)]
        public string Sender;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=6)]
        public string User;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=7)]
        public string Password;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=8)]
        public string Service;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=9)]
        public int File_ID;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=10)]
        public int Channel_ID;
        
        public Send_SMSOverloaded()
        {
        }
        
        public Send_SMSOverloaded(string Msg_ID, string Mobile_NO, string Body, string Validty, string StartTime, string Sender, string User, string Password, string Service, int File_ID, int Channel_ID)
        {
            this.Msg_ID = Msg_ID;
            this.Mobile_NO = Mobile_NO;
            this.Body = Body;
            this.Validty = Validty;
            this.StartTime = StartTime;
            this.Sender = Sender;
            this.User = User;
            this.Password = Password;
            this.Service = Service;
            this.File_ID = File_ID;
            this.Channel_ID = Channel_ID;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Send_SMSOverloadedResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Send_SMSOverloaded1
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string Send_SMSOverloadedResult;
        
        public Send_SMSOverloaded1()
        {
        }
        
        public Send_SMSOverloaded1(string Send_SMSOverloadedResult)
        {
            this.Send_SMSOverloadedResult = Send_SMSOverloadedResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Send_SMSWithoutResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Send_SMSWithoutResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string Msg_ID;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string Mobile_NO;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public string Body;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=3)]
        public string Validty;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=4)]
        public string StartTime;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=5)]
        public string Sender;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=6)]
        public string User;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=7)]
        public string Password;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=8)]
        public string Service;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=9)]
        public int File_ID;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=10)]
        public int Channel_ID;
        
        public Send_SMSWithoutResponse()
        {
        }
        
        public Send_SMSWithoutResponse(string Msg_ID, string Mobile_NO, string Body, string Validty, string StartTime, string Sender, string User, string Password, string Service, int File_ID, int Channel_ID)
        {
            this.Msg_ID = Msg_ID;
            this.Mobile_NO = Mobile_NO;
            this.Body = Body;
            this.Validty = Validty;
            this.StartTime = StartTime;
            this.Sender = Sender;
            this.User = User;
            this.Password = Password;
            this.Service = Service;
            this.File_ID = File_ID;
            this.Channel_ID = Channel_ID;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Send_SMSWithoutResponseResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Send_SMSWithoutResponse1
    {
        
        public Send_SMSWithoutResponse1()
        {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Send_Arr_SMS", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Send_Arr_SMS
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public EzagelSmsServive.SMSBulk[] Sms;
        
        public Send_Arr_SMS()
        {
        }
        
        public Send_Arr_SMS(EzagelSmsServive.SMSBulk[] Sms)
        {
            this.Sms = Sms;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Send_Arr_SMSResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Send_Arr_SMS1
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public int Send_Arr_SMSResult;
        
        public Send_Arr_SMS1()
        {
        }
        
        public Send_Arr_SMS1(int Send_Arr_SMSResult)
        {
            this.Send_Arr_SMSResult = Send_Arr_SMSResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Authenticate_Email", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Authenticate_EmailRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string Email;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string Username;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public string Password;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=3)]
        public int Credit;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=4)]
        public string ACtiveCode;
        
        public Authenticate_EmailRequest()
        {
        }
        
        public Authenticate_EmailRequest(string Email, string Username, string Password, int Credit, string ACtiveCode)
        {
            this.Email = Email;
            this.Username = Username;
            this.Password = Password;
            this.Credit = Credit;
            this.ACtiveCode = ACtiveCode;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Authenticate_EmailResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Authenticate_EmailResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string Authenticate_EmailResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string Username;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public string Password;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=3)]
        public int Credit;
        
        public Authenticate_EmailResponse()
        {
        }
        
        public Authenticate_EmailResponse(string Authenticate_EmailResult, string Username, string Password, int Credit)
        {
            this.Authenticate_EmailResult = Authenticate_EmailResult;
            this.Username = Username;
            this.Password = Password;
            this.Credit = Credit;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SMS_Count", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class SMS_CountRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string Body;
        
        public SMS_CountRequest()
        {
        }
        
        public SMS_CountRequest(string Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SMS_CountResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class SMS_CountResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public int SMS_CountResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public bool unicode;
        
        public SMS_CountResponse()
        {
        }
        
        public SMS_CountResponse(int SMS_CountResult, bool unicode)
        {
            this.SMS_CountResult = SMS_CountResult;
            this.unicode = unicode;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface ServiceSoapChannel : EzagelSmsServive.ServiceSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class ServiceSoapClient : System.ServiceModel.ClientBase<EzagelSmsServive.ServiceSoap>, EzagelSmsServive.ServiceSoap
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public ServiceSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(ServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), ServiceSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ServiceSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(ServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ServiceSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(ServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EzagelSmsServive.Get_All_ProvidersResponse> EzagelSmsServive.ServiceSoap.Get_All_ProvidersAsync(EzagelSmsServive.Get_All_ProvidersRequest request)
        {
            return base.Channel.Get_All_ProvidersAsync(request);
        }
        
        public System.Threading.Tasks.Task<EzagelSmsServive.Get_All_ProvidersResponse> Get_All_ProvidersAsync(string ACtiveCode)
        {
            EzagelSmsServive.Get_All_ProvidersRequest inValue = new EzagelSmsServive.Get_All_ProvidersRequest();
            inValue.ACtiveCode = ACtiveCode;
            return ((EzagelSmsServive.ServiceSoap)(this)).Get_All_ProvidersAsync(inValue);
        }
        
        public System.Threading.Tasks.Task<bool> Update_Bloked_MobilesAsync(string ACtiveCode)
        {
            return base.Channel.Update_Bloked_MobilesAsync(ACtiveCode);
        }
        
        public System.Threading.Tasks.Task<string> Send_SMSAsync(string Msg_ID, string Mobile_NO, string Body, string Validty, string StartTime, string Sender, string User, string Password, string Service)
        {
            return base.Channel.Send_SMSAsync(Msg_ID, Mobile_NO, Body, Validty, StartTime, Sender, User, Password, Service);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EzagelSmsServive.Send_SMSOverloaded1> EzagelSmsServive.ServiceSoap.Send_SMS1Async(EzagelSmsServive.Send_SMSOverloaded request)
        {
            return base.Channel.Send_SMS1Async(request);
        }
        
        public System.Threading.Tasks.Task<EzagelSmsServive.Send_SMSOverloaded1> Send_SMS1Async(string Msg_ID, string Mobile_NO, string Body, string Validty, string StartTime, string Sender, string User, string Password, string Service, int File_ID, int Channel_ID)
        {
            EzagelSmsServive.Send_SMSOverloaded inValue = new EzagelSmsServive.Send_SMSOverloaded();
            inValue.Msg_ID = Msg_ID;
            inValue.Mobile_NO = Mobile_NO;
            inValue.Body = Body;
            inValue.Validty = Validty;
            inValue.StartTime = StartTime;
            inValue.Sender = Sender;
            inValue.User = User;
            inValue.Password = Password;
            inValue.Service = Service;
            inValue.File_ID = File_ID;
            inValue.Channel_ID = Channel_ID;
            return ((EzagelSmsServive.ServiceSoap)(this)).Send_SMS1Async(inValue);
        }
        
        public System.Threading.Tasks.Task<string> Send_SMS_PortAsync(string Msg_ID, string Mobile_NO, string Body, string Validty, string StartTime, string Sender, string User, string Password, string Service, int SMS_Port)
        {
            return base.Channel.Send_SMS_PortAsync(Msg_ID, Mobile_NO, Body, Validty, StartTime, Sender, User, Password, Service, SMS_Port);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EzagelSmsServive.Send_SMSWithoutResponse1> EzagelSmsServive.ServiceSoap.Send_SMS_Without_ResponseAsync(EzagelSmsServive.Send_SMSWithoutResponse request)
        {
            return base.Channel.Send_SMS_Without_ResponseAsync(request);
        }
        
        public System.Threading.Tasks.Task<EzagelSmsServive.Send_SMSWithoutResponse1> Send_SMS_Without_ResponseAsync(string Msg_ID, string Mobile_NO, string Body, string Validty, string StartTime, string Sender, string User, string Password, string Service, int File_ID, int Channel_ID)
        {
            EzagelSmsServive.Send_SMSWithoutResponse inValue = new EzagelSmsServive.Send_SMSWithoutResponse();
            inValue.Msg_ID = Msg_ID;
            inValue.Mobile_NO = Mobile_NO;
            inValue.Body = Body;
            inValue.Validty = Validty;
            inValue.StartTime = StartTime;
            inValue.Sender = Sender;
            inValue.User = User;
            inValue.Password = Password;
            inValue.Service = Service;
            inValue.File_ID = File_ID;
            inValue.Channel_ID = Channel_ID;
            return ((EzagelSmsServive.ServiceSoap)(this)).Send_SMS_Without_ResponseAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EzagelSmsServive.Send_Arr_SMS1> EzagelSmsServive.ServiceSoap.Send_ArrSMSAsync(EzagelSmsServive.Send_Arr_SMS request)
        {
            return base.Channel.Send_ArrSMSAsync(request);
        }
        
        public System.Threading.Tasks.Task<EzagelSmsServive.Send_Arr_SMS1> Send_ArrSMSAsync(EzagelSmsServive.SMSBulk[] Sms)
        {
            EzagelSmsServive.Send_Arr_SMS inValue = new EzagelSmsServive.Send_Arr_SMS();
            inValue.Sms = Sms;
            return ((EzagelSmsServive.ServiceSoap)(this)).Send_ArrSMSAsync(inValue);
        }
        
        public System.Threading.Tasks.Task<EzagelSmsServive.Authenticate_EmailResponse> Authenticate_EmailAsync(EzagelSmsServive.Authenticate_EmailRequest request)
        {
            return base.Channel.Authenticate_EmailAsync(request);
        }
        
        public System.Threading.Tasks.Task<string> Upload_FileAsync(string File_Name, string Body, string Validty, string Sender, string StartTime, int EndTime, int Channel_ID, string User, string Password)
        {
            return base.Channel.Upload_FileAsync(File_Name, Body, Validty, Sender, StartTime, EndTime, Channel_ID, User, Password);
        }
        
        public System.Threading.Tasks.Task<EzagelSmsServive.SMS_CountResponse> SMS_CountAsync(EzagelSmsServive.SMS_CountRequest request)
        {
            return base.Channel.SMS_CountAsync(request);
        }
        
        public System.Threading.Tasks.Task<int> Get_Account_CreditAsync(string Account_Username, string Account_Password)
        {
            return base.Channel.Get_Account_CreditAsync(Account_Username, Account_Password);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.ServiceSoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.ServiceSoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.ServiceSoap))
            {
                return new System.ServiceModel.EndpointAddress("http://interface.ezagel.com/portex_ws/service.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.ServiceSoap12))
            {
                return new System.ServiceModel.EndpointAddress("http://interface.ezagel.com/portex_ws/service.asmx");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            ServiceSoap,
            
            ServiceSoap12,
        }
    }
}
