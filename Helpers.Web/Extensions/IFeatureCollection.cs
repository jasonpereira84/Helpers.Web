using System;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Http.Features;
        using Microsoft.AspNetCore.Diagnostics;

        public static partial class Web
        {
            public static Boolean TryGet<TFeature>(this IFeatureCollection featureCollection, out TFeature feature)
            {
                try 
                { 
                    feature = featureCollection.Get<TFeature>();
                    return feature != null;
                }
                catch 
                {
                    feature = default(TFeature);
                    return false; 
                }
            }

            public static Boolean TryGet<TFeature, TResult>(this IFeatureCollection featureCollection, Func<TFeature, TResult> getter, out TResult result) 
            {
                getter = getter ?? throw new ArgumentNullException(nameof(getter));

                if (TryGet(featureCollection, out TFeature feature).IsFalse())
                {
                    result = default(TResult);
                    return false;
                }

                try
                {
                    result = getter.Invoke(feature);
                    return true;
                }
                catch
                {
                    result = default(TResult);
                    return false;
                }
            }

            public static Boolean TryGetException(this IFeatureCollection featureCollection, out Exception exception)
                => TryGet<IExceptionHandlerFeature, Exception>(featureCollection, feature => feature.Error, out exception);
            
            public static Boolean TryGetExceptionAndPath(this IFeatureCollection featureCollection, out Exception exception, out String path)
            {
                if (TryGet(featureCollection, out IExceptionHandlerPathFeature feature) &&
                    feature.Error != default(Exception) &&
                    feature.Path.IsNotNullOrEmptyOrWhiteSpace())
                {
                    exception = feature.Error;
                    path = feature.Path;
                    return true;
                }

                exception = default(Exception);
                path = default(String);
                return false;
            }

        }
    }
}