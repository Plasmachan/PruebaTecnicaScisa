using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace PruebaTecnica
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null || authCookie.Value == "")
                return;


            FormsAuthenticationTicket authTicket;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var claims = authTicket.UserData.Split(';')
                  .Select(data =>
                  {
                      var parts = data.Split(':');
                      return new Claim(parts[0], parts[1]);
                  })
                  .ToList();


                var claimsIdentity = new ClaimsIdentity(claims, "Forms");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                if (Context.User != null)
                    HttpContext.Current.User = claimsPrincipal;
            }
            catch
            {
                return;
            }

        }





        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if(Request.RequestType == "POST" && !User.Identity.IsAuthenticated && !HasAnonymousAccess(Context))
            {
                Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
               SystemWebSectionGroup grp = (SystemWebSectionGroup)configuration.GetSectionGroup("system.web");
                AuthenticationSection auth = grp.Authentication;

                if(auth.Mode == AuthenticationMode.Forms)
                {
                    Response.Redirect(FormsAuthentication.LoginUrl, true);
                    Response.End();
                }
            }

        }

        public static bool HasAnonymousAccess(HttpContext context)
        {
            return UrlAuthorizationModule.CheckUrlAccessForPrincipal(context.Request.Path, new GenericPrincipal(new GenericIdentity(string.Empty), null), context.Request.HttpMethod);
        }

    }
}
