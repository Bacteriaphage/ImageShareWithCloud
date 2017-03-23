using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageSharingWebRole.Filters
{
    public class RefererHeaderAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var context = filterContext.HttpContext.Request.RequestContext;
            UrlHelper urlHelper = new UrlHelper(context);
            if(filterContext.HttpContext != null)
            {
                if (filterContext.HttpContext.Request.UrlReferrer == null)
                    throw new System.Web.HttpException("Invalid submission");
                if (urlHelper.IsLocalUrl(filterContext.HttpContext.Request.UrlReferrer.AbsoluteUri))
                    throw new System.Web.HttpException("This form wasn't submitted from this site!");
            }
            
        }
    }
}