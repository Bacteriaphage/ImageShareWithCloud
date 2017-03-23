using ImageSharingWebRole.DAL;
using ImageSharingWebRole.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageSharingWebRole.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected ApplicationDbContext ApplicationDBContext { get; set; }

        protected UserManager<ApplicationUser> UserManager { get; set; }

        protected BaseController()
        {
            this.ApplicationDBContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDBContext));
        }
        protected void CheckAda()
        {
            HttpCookie cookie = Request.Cookies.Get("ImageSharing");
            if (cookie != null)
            {

                if ("true".Equals(cookie["ADA"]))
                    ViewBag.isADA = true;
                else
                    ViewBag.isADA = false;
            }
            else
            {
                ViewBag.isADA = false;
            }
        }

        protected void SaveCookie(bool ADA)
        {
            HttpCookie cookie = new HttpCookie("ImageSharing");
            cookie.Expires = DateTime.Now.AddDays(3);
            cookie.HttpOnly = true;
            //cookie["Userid"] = userid;
            cookie["ADA"] = ADA ? "true" : "false";
            Response.Cookies.Add(cookie);
        }

        protected IEnumerable<ApplicationUser> ActiveUsers()
        {
            var db = new ApplicationDbContext();
            return db.Users.Where(u => u.Active);
        }


        protected IEnumerable<Image> ApprovedImages(IEnumerable<Image> images)
        {
            var db = new ApplicationDbContext();
            return images.Where(img => img.Approved);
        }


        protected IEnumerable<Image> ApprovedImages()
        {

            var db = new ApplicationDbContext();
            return ApprovedImages(db.Images);
        }

        protected SelectList UserSelectList()
        {
            String defaultId = GetLoggedInUser().Id;
            return new SelectList(ActiveUsers(), "Id", "UserName", defaultId);
        }
        protected ApplicationUser GetLoggedInUser()
        {

            return UserManager.FindById(User.Identity.GetUserId());
        }
        
    }
}