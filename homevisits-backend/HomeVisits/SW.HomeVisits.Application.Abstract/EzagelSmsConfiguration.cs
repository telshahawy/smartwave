using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract
{
    public class EzagelSmsConfiguration
    {
        public string SMSUserName { get; set; }
        public string SMSPassword { get; set; }
        public string SMSSenderName { get; set; }
        public string SecretKey { get; set; }
        public string AccountId { get; set; }
        public string CredentialSmsBody { get; set; }
        public string SMSService { get; set; }
        public int SMSPort { get; set; }
        public string EndPoindAddress { get; set; }
        public string EndPoindParameter { get; set; }
    }
}
