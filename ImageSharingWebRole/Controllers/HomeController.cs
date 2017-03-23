using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageSharingWebRole.Models;
using System.IO;
using System.Web.Script.Serialization;

namespace ImageSharingWebRole.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        [AllowAnonymous]
        public ActionResult Index(String id = "Stranger")
        {
            CheckAda();
            ViewBag.Title = "Welcome!";
            ApplicationUser user = GetLoggedInUser();
            if (user == null)
            {
                ViewBag.Id = id;
            }
            else
            {
                ViewBag.Id = user.UserName;
            }
            return View();
        }

        public ActionResult Error(String errid = "Image is invalidate")
        {  
            ViewBag.Message = errid;
            return View();
        }  
    }
}