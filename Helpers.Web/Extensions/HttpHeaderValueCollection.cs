using System;
using System.Net.Http.Headers;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        public static partial class Web
        {
            public static void Add(this HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> mediaTypeHeaders, String mediaType)
                => mediaTypeHeaders.Add(new MediaTypeWithQualityHeaderValue(mediaType));

            /// <summary>
            /// Even though 'Authentication' is the correct definition of what is happening, this actually creates an 'Authorizarion' header (per HTTP standards)
            /// </summary>
            /// <param name="authenticationHeaders"></param>
            /// <param name="scheme"></param>
            /// <param name="parameter"></param>
            //NOTE: Even though 'Authentication' is the correct definition of what is happening, this creates an 'Authorizarion' header
            public static void Add(this HttpHeaderValueCollection<AuthenticationHeaderValue> authenticationHeaders, String scheme, String parameter)
                => authenticationHeaders.Add(new AuthenticationHeaderValue(scheme, parameter));

            public static void Add(this HttpHeaderValueCollection<AuthenticationHeaderValue> authenticationHeaders, String scheme)
                => authenticationHeaders.Add(new AuthenticationHeaderValue(scheme));
        }
    }
}