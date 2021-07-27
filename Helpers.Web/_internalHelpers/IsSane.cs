using System;

namespace JasonPereira84.Helpers
{
    using Extensions;

    internal static partial class _internalHelpers
    {
        public static Boolean IsSane(this String value,
            String valueIfNull, String valueIfEmpty, String valueIfWhitespace,
            out String saneValue,
            Boolean dontTrim = false)
        {
            if (value.IsNull())
            {
                saneValue = "NULL";
                return false;
            }

            if (value.IsEmpty())
            {
                saneValue = "EMPTY";
                return false;
            }

            if (value.IsWhiteSpace())
            {
                saneValue = "WHITESPACE";
                return false;
            }

            saneValue = dontTrim ? value : value.Trim();
            return true;
        }

        public static Boolean IsSane(this String value, out String saneValue, Boolean dontTrim = false)
            => IsSane(value, null, null, null, out saneValue, dontTrim);
    }
}
