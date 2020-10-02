using System;
using System.Linq;
using System.Threading;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Http;
        using Microsoft.AspNetCore.Http.Features;
        using Microsoft.AspNetCore.Diagnostics;

        public static partial class Web
        {
            public static Boolean TryGet<TFeature>(this IFeatureCollection featureCollection, Func<TFeature, Boolean> predicate, out TFeature feature, TFeature defaultValue = default(TFeature))
            {
                feature = defaultValue;
                try { feature = featureCollection.Get<TFeature>(); return predicate.Invoke(feature); }
                catch { return false; }
            }

            public static Boolean TryGetException(this IFeatureCollection featureCollection, out Exception exception, Exception defaultValue)
            {
                exception = defaultValue;
                try
                {
                    var error = featureCollection.Get<IExceptionHandlerFeature>()?.Error;
                    if (error == null)
                        return false;

                    exception = error;
                    return true;
                }
                catch { return false; }
            }

            public static Boolean TryGetExceptionAndPath(this IFeatureCollection featureCollection, out (Exception Exception, String Path) feature, (Exception Exception, String Path) defaultValue)
            {
                feature = defaultValue;
                try
                {
                    var exceptionHandlerPathFeature = featureCollection.Get<IExceptionHandlerPathFeature>();

                    var error = exceptionHandlerPathFeature?.Error;
                    var path = exceptionHandlerPathFeature?.Path;
                    if (error == null)
                    {
                        if (path != null)
                            feature.Path = path;

                        return false;
                    }
                    else
                    {
                        feature.Exception = error;

                        if (path == null)
                            return false;

                        feature.Path = path;
                        return true;
                    }
                }
                catch { return false; }
            }

            public static Boolean TryGetException(this IFeatureCollection featureCollection, out Exception exception)
                => TryGetException(featureCollection, out exception, new Exception($"NULL {nameof(IExceptionHandlerFeature.Error)}"));

            public static Boolean TryGetExceptionAndPath(this IFeatureCollection featureCollection, out (Exception Exception, String Path) feature)
                => TryGetExceptionAndPath(featureCollection, out feature, (Exception: new Exception($"NULL {nameof(IExceptionHandlerPathFeature.Error)}"), Path: $"NULL {nameof(IExceptionHandlerPathFeature.Error)}"));
        }
    }
}