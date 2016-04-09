using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using System.Security.Claims;

namespace BikeGround.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new CustomAuthorizationFilter());
        }
    }

    //public class CustomAuthorizationFilter : System.Web.Mvc.FilterAttribute, System.Web.Mvc.IAuthorizationFilter
    //{
    //    public void OnAuthorization(AuthorizationContext filterContext)
    //    {
    //        //GenericPrincipal MyPrincipal = new GenericPrincipal(null, null);
    //        //IPrincipal Identity = (IPrincipal)MyPrincipal;

    //        //filterContext.RequestContext.HttpContext.User = Identity;
    //        //This method is responsible for validating the user or properties (authentication/authorization ) and checking if he as authorization 
    //        //access to the action / controller or system (if the custom authorization filter is registered as a global filter)

    //        //The user can be validate by the role or some authorization criteria that needs to be applied.
    //    }
    //}
}
