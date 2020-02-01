using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AWSFederatedDemoApp.Models;
using Amazon.SecurityToken;
using Amazon.S3;

namespace AWSFederatedDemoApp.Controllers
{
    public class FacebookDemoController : Controller
    {
        // GET: FacebookDemo
        public ActionResult Index()
        {
            return View(new FacebookDemoModel());
        }

        public ActionResult AWSAction(FacebookDemoModel facebookDemoModel, string submitButton)
        {

            ModelState.Clear();

            switch (submitButton)
            {
                case "Assume Role":
                    AWSAssumeRole(facebookDemoModel);
                    break;
                case "ListBuckets":
                    AWSListBuckets(facebookDemoModel);
                    break;
                case "ListObjects":
                    AWSListObjects(facebookDemoModel);
                    break;
                default:
                   
                    break;
            }

            return View("Index", facebookDemoModel);

        }

        private void AWSAssumeRole(FacebookDemoModel facebookDemoModel)
        {
            // This is the Facebook token that we got from authenticating with Facebook
            String facebookToken = facebookDemoModel.FacebookToken;

            // Create AWS Security Token Service client object with anonymous credential.  This methods is not using any AWS long term key or any other type of credential.
            AmazonSecurityTokenServiceClient awsSTSClient = new AmazonSecurityTokenServiceClient(new Amazon.Runtime.AnonymousAWSCredentials());

            // Set attribute to pass to the Security Token Service client using the AssumeRoleWithWebIdentityRequest model
            Amazon.SecurityToken.Model.AssumeRoleWithWebIdentityRequest myWebRequest = new Amazon.SecurityToken.Model.AssumeRoleWithWebIdentityRequest();
            myWebRequest.ProviderId = "graph.facebook.com";         // Set provider to Facebook
            myWebRequest.RoleSessionName = "StuzioDemoSession";     // Set role session name for tracking - Name can be anything. 
            myWebRequest.WebIdentityToken = facebookToken;          // Set the identity token to be the token we got from Facebook log in
            myWebRequest.RoleArn = "arn:aws:iam::01234567890:role/YourRoleName";  // Set the AWS role in the specified account with role's Amazon Resource Name.   Example: arn:aws:iam::081274447123:role/StuzioWebUserRole
            myWebRequest.DurationSeconds = 3600;                    // Set the durtion of session.

            // Using the model with attributes set, assume the role with web identity.
            Amazon.SecurityToken.Model.AssumeRoleWithWebIdentityResponse awsResponse = awsSTSClient.AssumeRoleWithWebIdentity(myWebRequest);

            // Get the response from AWS once Facebook user assumes a role.  Set the model data to display on UI
            facebookDemoModel.AWSAccessKeyID = awsResponse.Credentials.AccessKeyId;
            facebookDemoModel.AWSSecretAccessKey = awsResponse.Credentials.SecretAccessKey;
            facebookDemoModel.AWSSessionToken = awsResponse.Credentials.SessionToken;

            return;
        }

        public void AWSListBuckets(FacebookDemoModel facebookDemoModel)
        {

            // Clean up view model data for display
            facebookDemoModel.AWSBucketList.Clear();
            facebookDemoModel.AWSObjectList.Clear();
            facebookDemoModel.AWSBucketToGet = "";
            facebookDemoModel.AWSErrorMsg = "";

            #region Get S3 bucket list

            // Create AWS S3 client object - credential is 1) access key, 2) secret access key and 3) session token
            AmazonS3Client s3Client = new AmazonS3Client(
                facebookDemoModel.AWSAccessKeyID, 
                facebookDemoModel.AWSSecretAccessKey, 
                facebookDemoModel.AWSSessionToken, 
                Amazon.RegionEndpoint.USEast1);

            // Get list of buckets.  Policy attached to the role goversn what you can do here.
            Amazon.S3.Model.ListBucketsResponse bucketResponse = s3Client.ListBuckets();

            // Loop through the list of buckets and set the view model data to display
            foreach (Amazon.S3.Model.S3Bucket bucket in bucketResponse.Buckets)
            {
                facebookDemoModel.AWSBucketList.Add(bucket.BucketName);
            }

            #endregion

            return;

        }

        public void AWSListObjects(FacebookDemoModel facebookDemoModel)
        {
            // Clean up view model data for display
            facebookDemoModel.AWSObjectList.Clear();
            facebookDemoModel.AWSErrorMsg = "";

            #region Get object in the bucket specified

            try
            {
                // Create AWS S3 client object - credential is 1) access key, 2) secret access key and 3) session token
                AmazonS3Client s3Client = new AmazonS3Client(
                    facebookDemoModel.AWSAccessKeyID, 
                    facebookDemoModel.AWSSecretAccessKey, 
                    facebookDemoModel.AWSSessionToken, 
                    Amazon.RegionEndpoint.USEast1);

                // Get list of objects in the specified bucket
                Amazon.S3.Model.ListObjectsResponse objectResponse = s3Client.ListObjects(facebookDemoModel.AWSBucketToGet);

                // Loop through the list of objects and set the view model to display
                foreach (Amazon.S3.Model.S3Object s3Object in objectResponse.S3Objects)
                {
                    facebookDemoModel.AWSObjectList.Add(s3Object.Key);
                }
            }
            catch (Exception ex)
            {
                // If effor occurs such as access denied from AWS, set the view model to display.  
                facebookDemoModel.AWSErrorMsg = ex.Message;
            }


            #endregion

            return;
        }

    }



}