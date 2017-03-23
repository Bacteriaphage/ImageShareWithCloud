using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using ImageSharingWebRole.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace ImageSharingWebRole.DAL
{
    public class ImageSharingDBInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext db)
        {
            if (db.Database.Exists())
            {
                db.Database.Delete();
            }

            db.Database.Create();

            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(db);
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);

            RoleManager<IdentityRole> rm = new RoleManager<IdentityRole>(roleStore);
            UserManager<ApplicationUser> um = new UserManager<ApplicationUser>(userStore);

            IdentityResult ir;
            ApplicationUser nobody = createUser("nobody@example.org");
            ApplicationUser jfk = createUser("jfk@example.org");
            ApplicationUser nixon = createUser("nixon@example.org");
            ApplicationUser supervisor = createUser("supervisor@example.org");

            ir = um.Create(nobody, "nobody1234");
            ir = um.Create(jfk, "jfk1234");
            ir = um.Create(nixon, "nixon1234");
            ir = um.Create(supervisor, "supervisor1234");

            rm.Create(new IdentityRole("User"));
            if (!um.IsInRole(nobody.Id, "User"))
            {
                um.AddToRole(nobody.Id, "User");
            }
            if (!um.IsInRole(jfk.Id, "User"))
            {
                um.AddToRole(jfk.Id, "User");
            }
            if (!um.IsInRole(nixon.Id, "User"))
            {
                um.AddToRole(nixon.Id, "User");
            }
            if (!um.IsInRole(supervisor.Id, "User"))
            {
                um.AddToRole(supervisor.Id, "User");
            }

            rm.Create(new IdentityRole("Admin"));
            if (!um.IsInRole(nixon.Id, "Admin"))
            {
                um.AddToRole(nixon.Id, "Admin");
            }

            rm.Create(new IdentityRole("Approver"));
            if (!um.IsInRole(jfk.Id, "Approver"))
            {
                um.AddToRole(jfk.Id, "Approver");
            }

            rm.Create(new IdentityRole("Supervisor"));
            if (!um.IsInRole(supervisor.Id, "Supervisor"))
            {
                um.AddToRole(supervisor.Id, "Supervisor");
            }

            db.Tags.Add(new Tag { Name = "game" });
            db.Tags.Add(new Tag { Name = "landscape" });               
            db.Tags.Add(new Tag { Name = "music" });


            db.SaveChanges();
            /*
            db.Images.Add(new Image
            {
                Caption = "Hello",
                Description = "Well nice!",
                DateTaken = new DateTime(2015, 01, 01),
                UserId = nobody.Id,
                TagId = 1,
                Approved = true,
                validated = true
            });
            */
            db.SaveChanges();

            //LogContext.CreateTable();
            base.Seed(db);
        }
        private ApplicationUser createUser(String userName)
        {
            return new ApplicationUser { UserName = userName, Email = userName,Active = true, ADA = false};
        }
    }  
    
}