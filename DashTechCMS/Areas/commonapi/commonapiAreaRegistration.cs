using System.Web.Mvc;

namespace DashTechCMS.Areas.commonapi
{
    public class commonapiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "commonapi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "commonapi_default",
                "commonapi/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}