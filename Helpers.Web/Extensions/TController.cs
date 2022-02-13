using System;
using System.Net;

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
