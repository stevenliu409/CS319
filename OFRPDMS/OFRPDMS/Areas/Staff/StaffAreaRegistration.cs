using System.Web.Mvc;

namespace OFRPDMS.Areas.Staff
{
    public class StaffAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Staff";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Staff_default",
                "Staff/Center/{centerIdArg}/{controller}/{action}/{id}",
                new { centerIdArg = UrlParameter.Optional, controller = "Staff", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
