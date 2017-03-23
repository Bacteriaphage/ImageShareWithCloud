using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;

using System.Drawing;

using Microsoft.Azure;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Core;
using ImageSharingWebRole.Controllers;

using ImageSharingWebRole.Models;


namespace ImageSharingWebRole.DAL
{
    public class ImageStorage
    {
        public const bool USE_BLOB_STOREAGE = true;
        public const string ACCOUNT = "hycloudsd";
        public const string CONTAINER = "images";

        public static void SaveFile(HttpServerUtilityBase server, HttpPostedFileBase imageFile, int imageId)
        {
            if (USE_BLOB_STOREAGE)
            {
                CloudStorageAccount account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureStorageString")); //6
                //CloudStorageAccount account = CloudStorageAccount.DevelopmentStorageAccount;
                CloudBlobClient client = account.CreateCloudBlobClient();
                CloudBlobContainer container = client.GetContainerReference(CONTAINER);
                container.CreateIfNotExists();
                CloudBlockBlob blob = container.GetBlockBlobReference(FilePath(server, imageId));
                

                //blob.UploadFromStream(imageFile.InputStream);

                string imgFileName = server.MapPath("~/Images/" + "temp" + ".jpg");
                imageFile.SaveAs(imgFileName);
                using (var fileStream = System.IO.File.OpenRead(imgFileName))
                {
                    blob.UploadFromStream(fileStream);
                }

                /*
  
               */

            }
            else
            {
                string imgFileName = FilePath(server, imageId);
                imageFile.SaveAs(imgFileName);
            }
        }
        public static void DeleteFile(HttpServerUtilityBase server, int imageId)
        {
            if (USE_BLOB_STOREAGE)
            {
                CloudStorageAccount account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureStorageString")); //6
                //CloudStorageAccount account = CloudStorageAccount.DevelopmentStorageAccount;
                CloudBlobClient client = account.CreateCloudBlobClient();
                CloudBlobContainer container = client.GetContainerReference(CONTAINER);

                CloudBlockBlob blob = container.GetBlockBlobReference(FilePath(server, imageId));

                blob.Delete();
            }
        }



        public static string FilePath(HttpServerUtilityBase server, int imageId)
        {
            if (USE_BLOB_STOREAGE)
            {
                return FileName(imageId);
            }
            else
            {
                string imgFileName = server.MapPath("~/Content/Images/" + FileName(imageId));
                return imgFileName;
            }
        }

        public static string FileName(int imageId)
        {
            return imageId + ".jpg";
        }

        public static string ImageURI(UrlHelper urlHelper, int imageId)
        {
            if (USE_BLOB_STOREAGE)
            {
                return "https://" + ACCOUNT + ".blob.core.windows.net/" + CONTAINER + "/" + FileName(imageId);
            }
            else
            {
                return urlHelper.Content("~/Content/Images/" + FileName(imageId));
            }
        }

        public static Boolean Validate(int id)
        {
            if (USE_BLOB_STOREAGE)
            {
                //CloudStorageAccount account = CloudStorageAccount.DevelopmentStorageAccount;
                CloudStorageAccount account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureStorageString")); //6
                CloudBlobClient client = account.CreateCloudBlobClient();
                CloudBlobContainer container = client.GetContainerReference(CONTAINER);

                CloudBlockBlob blob = container.GetBlockBlobReference(FilePath(null, id));
                ApplicationDbContext db = new ApplicationDbContext();

                ImageSharingWebRole.Models.Image myimage = db.Images.Find(id);
                //blob.UploadFromStream(imageFile.InputStream);

                //string imgFileName = server.MapPath("~/Images/" + "temp" + ".jpg");
                //imageFile.SaveAs(imgFileName);
                //using (var fileStream = System.IO.File.OpenRead(imgFileName))
                //{
                //    blob.UploadFromStream(fileStream);
                //}

                MemoryStream imageStream = new MemoryStream();
                blob.DownloadToStream(imageStream);

                System.Drawing.Image image = System.Drawing.Image.FromStream(imageStream);

                if(image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid)
                {          
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //else
            //{
            //    //string imgFileName = FilePath(server, imageId);
            //    //imageFile.SaveAs(imgFileName);
            //}
        }

    }
}