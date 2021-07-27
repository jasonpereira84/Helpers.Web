using System;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        public static partial class Web
        {
            public static IReadOnlyDictionary<String, Object> GetAppProperties(this Assembly assembly)
            {
                String _get<T>(Func<T, String> sanitizer) where T : Attribute
                {
                    var retVal = default(T);
                    try { retVal = Misc.GetAssemblyAttribute<T>(assembly); }
                    catch (Exception) { }
                    return sanitizer.Invoke(retVal);
                }

                return new Dictionary<String, Object>
                {
                    { "Company", _get<AssemblyCompanyAttribute>(attr => Misc.SanitizeTo(attr?.Company, "?")) },
                    { "Product", _get<AssemblyProductAttribute>(attr => Misc.SanitizeTo(attr?.Product, "?")) },
                    { "Copyright", _get<AssemblyCopyrightAttribute>(attr => Misc.SanitizeTo(attr?.Copyright, "?")) },
                    { "Version", _get<AssemblyInformationalVersionAttribute>(attr => Misc.SanitizeTo(attr?.InformationalVersion, "?")) },
                    { "Title", _get<AssemblyTitleAttribute>(attr => Misc.SanitizeTo(attr?.Title, "?")) },
                    { "Description", _get<AssemblyDescriptionAttribute>(attr => Misc.SanitizeTo(attr?.Description, "?")) },
                };
            }
        }
    }
}
