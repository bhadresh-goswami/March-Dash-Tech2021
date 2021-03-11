using System.Web.Mvc;

namespace DashTechCMS.Areas.expertcvcoach
{
    public class expertcvcoachAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "expertcvcoach";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "expertcvcoach_default",
                "expertcvcoach/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}