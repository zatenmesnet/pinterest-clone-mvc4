using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Amazon.S3;
using Amazon.S3.Transfer;
using TTModels;
using WebMatrix.WebData;
using System.Drawing;
using System.Drawing.Imaging;

namespace TT.Controllers
{
    public class ServiceController : Controller
    {
        //
        // GET: /Service/

        public ActionResult Index(int id = -1)
        {
            if (id == -1)
                return HttpNotFound();

            return View(new TTRESTService().GetPostUser(id));
        }

        public ActionResult Comments(int id = -1)
        {
            if (id == -1)
                return HttpNotFound();

            return View(new TTRESTService().GetComments(id));
        }

        public ActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        public void Upload()
        {
            // Loop through each file in the request
            for (int i = 0; i < HttpContext.Request.Files.Count; i++)
            {
                // Pointer to file
                var file = HttpContext.Request.Files[i];
                var filename = User.Identity.Name + "_" + DateTime.UtcNow + "_" + file.FileName;
                int width = 0, height = 0;
                try
                {
                    using (var client = new TransferUtility(AuthConfig.AWSPUBLIC, AuthConfig.AWSPRIVATE))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var yourBitmap = new Bitmap(file.InputStream))
                            {
                                width = yourBitmap.Width;
                                height = yourBitmap.Height;

                                yourBitmap.Save(memoryStream, ImageFormat.Jpeg);

                                AsyncCallback callback = new AsyncCallback(uploadComplete);
                                var request = new TransferUtilityUploadRequest();
                                request.BucketName = "TTPosts";
                                //create a hash of the user, the current time, and the file name
                                //to avoid collisions
                                request.Key = filename;
                                request.InputStream = memoryStream;
                                //makes public
                                request.AddHeader("x-amz-acl", "public-read");
                                IAsyncResult ar = client.BeginUpload(request, callback, null);
                                client.EndUpload(ar);
                            }
                        }
                    }
                }
                catch (AmazonS3Exception amazonS3Exception)
                {
                    if (amazonS3Exception.ErrorCode != null &&
                        (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                        amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                    {
                        Console.WriteLine("Please check the provided AWS Credentials.");
                        Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                    }
                    else
                    {
                        Console.WriteLine("An error occurred with the message '{0}' when writing an object", amazonS3Exception.Message);
                    }
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                Posts p = new Posts() { title = "$" + HttpContext.Request.Form["tags"], filename = "http://s3.amazonaws.com/TTPosts/" + filename, owner = WebSecurity.GetUserId(User.Identity.Name), dateuploaded = DateTime.UtcNow, width = width, height = height };

                new TTRESTService().PostPost(p);
            }
        }

        private static void uploadComplete(IAsyncResult result)
        {

        }

        public ActionResult TradingView(string symbol)
        {
            ViewBag.Symbol = "FX:" + symbol;

            return View();
        }
    }
}