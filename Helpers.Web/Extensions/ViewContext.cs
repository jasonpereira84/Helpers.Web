using System;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Mvc.Rendering;

        public static partial class Web
        {
            public static Int32 StatusCode(this ViewContext viewContext, Int32 defaultValue)
                => viewContext?.HttpContext?.Response?.StatusCode ?? defaultValue;
        }
    }
}
