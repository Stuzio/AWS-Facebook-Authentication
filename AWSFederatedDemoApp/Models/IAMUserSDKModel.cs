using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWSFederatedDemoApp.Models
{
    public class IAMUserSDKModel
    {
        public IAMUserSDKModel()
        {
            AWSBucketList = new List<string>();
            AWSObjectList = new List<string>();

        }
        
        public string AWSAccessKeyID { get; set; }
        public string AWSSecretAccessKey { get; set; }
        
        public List<String> AWSBucketList { get; set; }
        public List<String> AWSObjectList { get; set; }

        public string AWSBucketToGet { get; set; }
        public string AWSErrorMsg { get; set; }

    }
}