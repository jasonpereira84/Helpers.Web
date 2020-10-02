using System;
using System.Net;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    using JasonPereira84.Helpers.Extensions;

    public sealed class HttpStatusMessage
    {
        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested() { }

            internal static readonly HttpStatusMessage instance = new HttpStatusMessage();
        }

        private Dictionary<Int32, String> _table;

        private HttpStatusMessage()
        {
            _table = new Dictionary<Int32, String>()
            {
                { 100, "Continue"},
                { 101, "Switching Protocols"},
                { 102, "Processing"},
                { 200, "OK"},
                { 201, "Created"},
                { 202, "Accepted"},
                { 203, "Non-Authoritive Information"},
                { 204, "No Content"},
                { 205, "Reset Content"},
                { 206, "Partial Content"},
                { 207, "Multi-Status"},
                { 208, "Already Reported"},
                { 226, "IM Used"},
                { 300, "Multiple Choices"},
                { 301, "Moved Permanently"},
                { 302, "Found"},
                { 303, "See Other"},
                { 304, "Not Modified"},
                { 305, "Use Proxy"},
                { 306, "Switch Proxy"},
                { 307, "Temporary Redirect"},
                { 308, "Permanent Redirect"},
                { 400, "Bad Request"},
                { 401, "Unauthorized"},
                { 402, "Payment Required"},
                { 403, "Forbidden"},
                { 404, "Not Found"},
                { 405, "Method Not Allowed"},
                { 406, "Not Acceptable"},
                { 407, "Proxy Authentication Required"},
                { 408, "Request Timeout"},
                { 409, "Conflict"},
                { 410, "Gone"},
                { 411, "Length Required"},
                { 412, "Precondition Failed"},
                { 413, "Payload Too Large"},
                { 414, "URI Too Long"},
                { 415, "Unsupported Media Type"},
                { 416, "Range Not Satisfiable"},
                { 417, "Expectation Failed"},
                { 418, "I’m a teapot"},
                { 421, "Misdirected Request"},
                { 422, "Unprocessable Entity"},
                { 423, "Locked"},
                { 424, "Failed Dependency"},
                { 426, "Upgrade Required"},
                { 428, "Precondition Required"},
                { 429, "Too Many Requests"},
                { 431, "Request Header Fields Too Large"},
                { 451, "Unavailable For Legal Reasons"},
                { 500, "Internal Server Error"},
                { 507, "Insufficient Storage"},
                { 508, "Loop Detected"}
            };
        }

        public static Dictionary<Int32, String> Table => Nested.instance._table;

        public static String For(Int32 code, String defaultValue = "Unknown Http Status Code")
            => Table.ContainsKey(code)
                ? Table[code]
                : defaultValue;
    }
}
