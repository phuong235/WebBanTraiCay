using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebBanHang.Context;

namespace WebBanHang.Models.Common
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedroles;
        public CustomAuthorizeAttribute(params string[] roles)
        {
            allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var userId = Convert.ToString(httpContext.Session["idUser"]);
            if (!string.IsNullOrEmpty(userId))
                using (var context = new WebBanHangEntities())
                {
                    var userRole = (from u in context.Users
                                    join r in context.Roles on u.RoleId equals r.Id
                                    where u.Id.ToString() == userId
                                    select new
                                    {
                                        r.Name
                                    }).FirstOrDefault();
                    foreach (var role in allowedroles)
                    {
                        if (role == userRole.Name) return true;
                    }
                }


            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Users" },
                    { "action", "Login" }
               });
        }
    }
}