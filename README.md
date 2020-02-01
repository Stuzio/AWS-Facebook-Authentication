# AWS-Facebook-Authentication
Web application to demonstrate web identity federated login using Facebook as identity provider using IAM Role to access S3 resources in AWS account

Application is a .NET ASP.NET MVC application using .NET Framework 4.6.1.  The solution is built using Microsoft Visual Studio 2017. 

The main web page demonstrates authenticating to a Facebook application then using the response token to assume a AWS IAM Role which has permission to read S3 buckets and objects.  In order to run this application, you will need to complete the following steps.

1. Set up a Facebook application and get the FAcebook APP ID
2. Enter your Facebook App ID into the Views/FacebookDemo/index.cshtml on the window.fbAsyncInit function.  
3. On the AWS account, set up an IAM Role with trust relationship with Facebook.
4. Attach a permission to access the S3 resources to the IAM Role
5. Enter the IAM role's ARN into the Controller/FacebookDemoController.cs on the AWS AssumeRole method.  Set the role ARN on the myWebRequest.RoleArn variable.  
6. Build the application and follow the UI steps 
  
    Step 1 - Log in to Facebook with known Facebook user, 
  
    Step 2 - Copy the Facebook accessToken to step 2 Facebook Access Token text box. Assume IAM Role
  
    Step 3 - List Buckets
  
    Step 4 - Copy name of a bucket to Step 4 Bucket Name and list objects in the bucket.  
