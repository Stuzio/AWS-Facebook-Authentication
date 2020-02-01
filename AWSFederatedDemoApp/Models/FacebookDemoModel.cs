using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWSFederatedDemoApp.Models
{
    public class FacebookDemoModel
    {
        public FacebookDemoModel()
        {
            AWSBucketList = new List<string>();
            AWSObjectList = new List<string>();

        }

        public string FacebookToken { get; set; }

        public string AWSAccessKeyID { get; set; }
        public string AWSSecretAccessKey { get; set; }
        public string AWSSessionToken { get; set; }

        public List<String> AWSBucketList { get; set; }
        public List<String> AWSObjectList { get; set; }

        public string AWSBucketToGet { get; set; }
        public string AWSErrorMsg { get; set; }

    }
}