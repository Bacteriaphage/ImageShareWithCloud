using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ImageSharingWebRole.Models;
using ImageSharingWebRole.DAL;
using System.Data;


namespace ImageSharingWebRole.Controllers
{
    public class ImagesController : BaseController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        private ValidationQueue validationQ = new ValidationQueue();
        private AzureStorageQueue azureStorageQueue = new AzureStorageQueue();

        [HttpGet]
        public ActionResult Upload()
        {
            CheckAda();
            ApplicationUser user = GetLoggedInUser();

            if (user == null)
            {
                return RedirectToAction("Login", "Account");

            }
            else
            {
                ViewBag.Message = "";

                ViewBag.Tags = new SelectList(ApplicationDBContext.Tags, "Id", "Name", 1);
                return View("");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[RequireHttps]
        public ActionResult Upload(ImageView image, HttpPostedFileBase ImageFile)
        {
            CheckAda();
            ApplicationUser user = GetLoggedInUser();

            if (user == null)
            {
                return RedirectToAction("Login", "Account");

            }
            String userid = user.Id;
            Image imageEntity = new Image();
            imageEntity.Caption = image.Caption;
            imageEntity.Description = image.Description;
            imageEntity.DateTaken = image.DateTaken;
            imageEntity.User = user;
            imageEntity.UserId = userid;
            imageEntity.TagId = image.TagId;
            imageEntity.Approved = false;
            imageEntity.Validated = false;
            if (ImageFile != null && ImageFile.ContentLength > 0)
            {

                System.Drawing.Image img = System.Drawing.Image.FromStream(ImageFile.InputStream);
                //if (img.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid)
                //{
                ApplicationDBContext.Images.Add(imageEntity);
                ApplicationDBContext.SaveChanges();
                ImageStorage.SaveFile(Server, ImageFile, imageEntity.Id);
                ValidationRequest validationReq = new ValidationRequest();
                validationReq.imageId = imageEntity.Id;
                validationReq.UserId = user.Id;
                validationQ.Initialize();
                validationQ.Send(validationReq);
                validationQ.Finalize();
                azureStorageQueue.addMessage(user.UserName.ToString() + " " + image.Caption + " ");
                return RedirectToAction("Details", new { Id = imageEntity.Id });
                
                //}
                //else
                //{
                //    ViewBag.Message = "Invalid JPEG image!";
                //    return RedirectToAction("Error", "Home", new { errid = "NoImageToUpload" });
                //}
            }
            else
            {
                ViewBag.Message = "Invalid JPEG image!";
                return RedirectToAction("Error", "Home", new { errid = "NoImageToUpload" });
            }
        }
        [HttpGet]
        public ActionResult Query()
        {
            CheckAda();
            ViewBag.Message = "";
            return View();
        }

        [HttpGet]
        public ActionResult Details(int Id)
        {
            CheckAda();
            ApplicationUser user = GetLoggedInUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Image imageEntity = ApplicationDBContext.Images.Find(Id);
            if (imageEntity != null)
            {
                ImageView imageView = new ImageView();
                imageView.Id = imageEntity.Id;
                imageView.Caption = imageEntity.Caption;
                imageView.Description = imageEntity.Description;
                imageView.DateTaken = imageEntity.DateTaken;
                imageView.TagName = imageEntity.Tag.Name;
                imageView.UserId = imageEntity.User.UserName;
                imageView.Uri = ImageStorage.ImageURI(Url, imageEntity.Id);
                LogContext.AddLogEntry(user.UserName, imageView);
                return View(imageView);
            }
            else
            {

                return RedirectToAction("Error", "Home", new { errid = "ImageNotFound" });
            }
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            CheckAda();
            ApplicationUser user = GetLoggedInUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Image imageEntity = ApplicationDBContext.Images.Find(Id);
            if (imageEntity != null)
            {
                HttpCookie cookie = Request.Cookies.Get("ImageSharing");

                if (imageEntity.User.UserName == user.Id)
                {

                    ViewBag.Message = "";
                    ViewBag.Tags = new SelectList(ApplicationDBContext.Tags, "Id", "Name", imageEntity.TagId);
                    ImageView image = new ImageView();
                    image.Id = imageEntity.Id;
                    image.TagId = imageEntity.TagId;
                    image.Caption = imageEntity.Caption;
                    image.Description = imageEntity.Description;
                    image.DateTaken = imageEntity.DateTaken;
                    image.Uri = ImageStorage.ImageURI(Url, imageEntity.Id);
                    return View("Edit", image);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {

                return RedirectToAction("Error", "Home", new { errid = "ImageNotFound" });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, ImageView image)
        {
            CheckAda();
            ApplicationUser user = GetLoggedInUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");

            }
            if (ModelState.IsValid)
            {
                Image imageEntity = ApplicationDBContext.Images.Find(Id);
                if (imageEntity != null)
                {
                    HttpCookie cookie = Request.Cookies.Get("ImageSharing");
                    //if (imageEntity.UserId == user.Id)
                    if (imageEntity.User.UserName == user.Id)
                    {
                        imageEntity.TagId = image.TagId;
                        imageEntity.Caption = image.Caption;
                        imageEntity.Description = image.Description;
                        imageEntity.DateTaken = image.DateTaken;
                        ApplicationDBContext.Entry(imageEntity).State = System.Data.Entity.EntityState.Modified;
                        ApplicationDBContext.SaveChanges();

                        return RedirectToAction("Details", new { Id = imageEntity.Id });
                    }
                    else
                    {
                        return RedirectToAction("Login", "Account");
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Home", new { errid = "EditNotFound" });
                }
            }
            else
            {
                return View("Edit", image);
            }

        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            CheckAda();
            ApplicationUser user = GetLoggedInUser();

            if (user == null)
            {
                return RedirectToAction("Login", "Account");

            }
            Image imageEntity = ApplicationDBContext.Images.Find(Id);
            if (imageEntity != null)
            {
                ImageView imageview = new ImageView();
                imageview.Id = imageEntity.Id;
                imageview.Caption = imageEntity.Caption;
                imageview.Description = imageEntity.Description;
                imageview.DateTaken = imageEntity.DateTaken;
                imageview.TagName = imageEntity.Tag.Name;
                imageview.UserId = imageEntity.User.UserName;
                imageview.Uri = ImageStorage.ImageURI(Url, imageEntity.Id);
                return View(imageview);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { errid = "ImageNotFound" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(FormCollection values, int Id)
        {
            CheckAda();
            ApplicationUser user = GetLoggedInUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");

            }
            Image imageEntity = ApplicationDBContext.Images.Find(Id);
            if (imageEntity != null)
            {

                if (imageEntity.UserId == user.Id)
                {
                    //db.Entry(imageEntity).State = EntityState.Deleted;
                    ApplicationDBContext.Images.Remove(imageEntity);
                    ApplicationDBContext.SaveChanges();
                    ImageStorage.DeleteFile(Server, Id);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return RedirectToAction("Error", "Home", new { errid = "DeleteNotFound" });
            }
        }



        [HttpGet]
        public ActionResult ListAll()
        {
            CheckAda();
            ApplicationUser user = GetLoggedInUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");

            }
            IEnumerable<Image> images = ApprovedImages().ToList();

            ViewBag.Userid = user.Id;

            return View(images);

        }

        [HttpGet]
        public ActionResult ListByUser()
        {
            CheckAda();
            ApplicationUser user = GetLoggedInUser();

            if (user == null)
            {
                return RedirectToAction("Login", "Account");

            }

            SelectList users = UserSelectList();

            return View(users);
        }

        [HttpGet]
        public ActionResult DoListByUser(String Id)
        {
            CheckAda();
            IEnumerable<Image> Images = ApplicationDBContext.Images.ToList();
            ApplicationUser user = UserManager.Users.FirstOrDefault(u => u.Id == Id);
            if (user != null)
            {
                ViewBag.Userid = user.Id;

                return View("ListAll", ApprovedImages(user.Images).ToList());
            }
            else
            {
                ViewBag.Message = Id + " User Id Not Found";
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public ActionResult ListByTag()
        {
            CheckAda();
            ApplicationUser user = GetLoggedInUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");

            }

            SelectList tags = new SelectList(ApplicationDBContext.Tags, "Id", "Name", 1);
            ViewBag.Title = "List by Tag Name";
            return View(tags);
        }

        [HttpGet]
        public ActionResult DoListByTag(int Id)
        {
            CheckAda();
            ApplicationUser user = GetLoggedInUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            IEnumerable<Image> images = ApplicationDBContext.Images.ToList();


            if (user.Id != null)
            {
                Tag tag = ApplicationDBContext.Tags.Find(Id);

                if (tag != null)
                {
                    ViewBag.UserId = user.Id;
                    return View("ListAll", tag.Images);
                }
                else
                {
                    return RedirectToAction("Error", "Home", new { errid = "ImageNotFound" });
                }
            }
            else
            {
                ViewBag.Message = Id + " User Id Not Found";
                return RedirectToAction("Login", "Account");
            }

        }


        [HttpGet]
        [Authorize(Roles = "Approver")]
        public ActionResult Approve()
        {
            CheckAda();
            ViewBag.Message = "";
            List<SelectItemViewTag> model = new List<SelectItemViewTag>();
            foreach (var u in ApplicationDBContext.Images.ToArray())
            {
                if (u.Validated && !u.Approved)
                {
                    model.Add(new SelectItemViewTag(u.Id, u.Caption, u.Approved));

                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Approver")]
        public ActionResult Approve(List<SelectItemViewTag> model)
        {

            CheckAda();
            foreach (var img in model)
            {
                Image image = ApplicationDBContext.Images.Find(img.Id);
                if (!image.Approved && img.Checked)
                {
                    image.Approved = true;
                }
            }
            ApplicationDBContext.SaveChanges();
            ViewBag.Message = "Images Approved.";
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Supervisor")]
        public ActionResult ImageViewsDefault()
        {
            CheckAda();
            IEnumerable<Week> weeks = new List<Week>() { new Week(0), new Week(1), new Week(2) };
            SelectList weeklist = new SelectList(weeks, "Id", "Name", 0);
            return View(weeklist);
        }

        [HttpGet]
        [Authorize(Roles = "Supervisor")]
        public ActionResult ImageViews(int id)
        {
            CheckAda();

            IEnumerable<LogEntry> entries = LogContext.Select(id).ToList();
            return View(entries);
        }
        [HttpGet]
        [Authorize(Roles = "Approver")]
        public ActionResult ValidationMessage()
        {
            CheckAda();
            ViewBag.size = azureStorageQueue.size();
            IEnumerable<String> messageQueue = azureStorageQueue.peekAllMessage();
            return View(messageQueue);
            //return View();
        }

        [HttpPost]
        [Authorize(Roles = "Approver")]
        //[RequireHttps]
        [ValidateAntiForgeryToken]
        public ActionResult ValidationMessage(bool flag = true)
        {
            CheckAda();
            azureStorageQueue.ClearMessage();
            return RedirectToAction("Index", "Home");
        }
    }
}