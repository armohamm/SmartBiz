using System.Web;
using System.Web.Mvc;
using Serilog;

namespace Biz
{

    public class HandleAndLogErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //Log the Exception
            try
            {
                string action = filterContext.RouteData.Values["action"].ToString();
                string controller = filterContext.RouteData.Values["controller"].ToString();
                Log.Error(filterContext.Exception, string.Format("{0}.{1}", controller, action));
            }
            catch { } // No errors from Loging handling
            base.OnException(filterContext);
        }
    }
}
