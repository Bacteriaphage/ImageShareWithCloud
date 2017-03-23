using System.Web;
using System.Web.Mvc;

namespace ImageSharingWebRole
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new System.Web.Mvc.AuthorizeAttribute());
            //filters.Add(new System.Web.Mvc.RequireHttpsAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
