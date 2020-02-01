using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AWSFederatedDemoApp.Models;
using Amazon.S3;

namespace AWSFederatedDemoApp.Controllers
{
    public class IAMUserSDKController : Controller
    {
        // For demonstration purpose, enter the IAM User Access Key information here. 
        String MyAccessKeyID = "YOUR-ACCESS-KEY-HERE";                          // Put AWS Access Key ID here.  Example: AKIABC6EMXOYWE23SABC
        String MySecretAccessKey = "YOUR-ACCESS-SECRET-KEY-HERE";  // Put AWS Secret Key ID here.  Example: qYOUwAA5f1fWsTUYOODkIyAJcWUonA+gfNp0ABC

        // GET: IAMUserSDK
        public ActionResult Index()
        {
            return View(new IAMUserSDKModel());
        }

        public ActionResult AWSAction(IAMUserSDKModel iamUserSDKDemoModel, string submitButton)
        {

            ModelState.Clear();

            switch (submitButton)
            {
                
                case "ListBuckets":
                    AWSListBuckets(iamUserSDKDemoModel);
                    break;
                case "ListObjects":
                    AWSListObjects(iamUserSDKDemoModel);
                    break;
                default:

                    break;
            }

            return View("Index", iamUserSDKDemoModel);

        }

        public void AWSListBuckets(IAMUserSDKModel iamUserSDKDemoModel)
        {
            // Clean up view model data for display
            iamUserSDKDemoModel.AWSBucketList.Clear();
            iamUserSDKDemoModel.AWSObjectList.Clear();
            iamUserSDKDemoModel.AWSBucketToGet = "";
            iamUserSDKDemoModel.AWSErrorMsg = "";

            #region Get S3 bucket list

            // Create AWS S3 client object - credential is 1) access key, 2) secret access key
            AmazonS3Client s3Client = new AmazonS3Client(MyAccessKeyID, MySecretAccessKey, Amazon.RegionEndpoint.USEast1);
            
            // Get list of buckets.  Policy attached to the role goversn what you can do here.
            Amazon.S3.Model.ListBucketsResponse bucketResponse = s3Client.ListBuckets();

            // Loop through the list of buckets and set the view model data to display
            foreach (Amazon.S3.Model.S3Bucket bucket in bucketResponse.Buckets)
            {
                iamUserSDKDemoModel.AWSBucketList.Add(bucket.BucketName);
            }

            #endregion

            return;

        }

        public void AWSListObjects(IAMUserSDKModel iamUserSDKDemoModel)
        {
            // Clean up view model data for display
            iamUserSDKDemoModel.AWSObjectList.Clear();
            iamUserSDKDemoModel.AWSErrorMsg = "";

            #region Get object in the bucket specified

            try
            {
                // Create AWS S3 client object - credential is 1) access key, 2) secret access key
                AmazonS3Client s3Client = new AmazonS3Client(MyAccessKeyID, MySecretAccessKey, Amazon.RegionEndpoint.USEast1);

                // Get list of objects in the specified bucket
                Amazon.S3.Model.ListObjectsResponse objectResponse = s3Client.ListObjects(iamUserSDKDemoModel.AWSBucketToGet);

                // Loop through the list of objects and set the view model to display
                foreach (Amazon.S3.Model.S3Object s3Object in objectResponse.S3Objects)
                {
                    iamUserSDKDemoModel.AWSObjectList.Add(s3Object.Key);
                }
            }
            catch (Exception ex)
            {
                // If effor occurs such as access denied from AWS, set the view model to display.  
                iamUserSDKDemoModel.AWSErrorMsg = ex.Message;
            }


            #endregion

            return;
        }

    }
}