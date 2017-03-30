using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iAsset.Web.UI.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            var model = new HandleErrorInfo(filterContext.Exception, "Error", "Index");
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Error/Index.cshtml",
                ViewData = new ViewDataDictionary(model)
               
            };
        }
	}
}