using System;
using System.Net;
using System.Web;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Mvc;

        public static partial class Web
        {
            public static ViewResult ErrorView<TController, TViewModel>(this TController controller, HttpStatusCode httpStatusCode, TViewModel viewModel, String viewName = "Error")
                where TController : Controller
            {
                controller.HttpContext.Response.StatusCode = (Int32)httpStatusCode;
                return controller.View(viewName, viewModel);
            }

            public static PartialViewResult ErrorPartialView<TController, TViewModel>(this TController controller, HttpStatusCode httpStatusCode, TViewModel viewModel, String viewName = "Error")
                where TController : Controller
            {
                controller.HttpContext.Response.StatusCode = (Int32)httpStatusCode;
                return controller.PartialView(viewName, viewModel);
            }
        }
    }
}
