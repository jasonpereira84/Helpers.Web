using System;
using System.Net;
using System.Threading.Tasks;

namespace JasonPereira84.Helpers
{
    using Newtonsoft.Json;

    using Microsoft.AspNetCore.Mvc;

    public class AjaxResult : IActionResult
    {
        [JsonObject]
        public struct Content<TValue>
        {
            [JsonProperty]
            public Boolean IsSuccess { get; set; }

            [JsonProperty]
            public TValue Value { get; set; }

            public override String ToString()
                => JsonConvert.SerializeObject(this);
        }

        public HttpStatusCode HttpStatusCode { get; set; }

        public String ContentType { get; set; }

        public String Data { get; set; }

        public async Task ExecuteResultAsync(ActionContext actionContext)
            => await Extensions.Web.WriteAsync(actionContext.HttpContext.Response, this);

        public static AjaxResult From<TValue>(Boolean isSuccess, TValue value)
            => new AjaxResult
            {
                HttpStatusCode = HttpStatusCode.OK,
                ContentType = MimeTypes.Application.Json,
                Data = JsonConvert.SerializeObject(
                    new Content<TValue>
                    {
                        IsSuccess = isSuccess,
                        Value = value
                    })
            };

        public static AjaxResult Ok<TValue>(TValue value) => From(true, value);

        public static AjaxResult Fail(String error) => From(false, error);
    }
}
