using System;

namespace JasonPereira84.Helpers
{
    using Extensions;

    internal static partial class _internalHelpers
    {
        public static String IsSane(this String value,
            String valueIfNull, String valueIfEmpty, String valueIfWhitespace,
            out String result,
            Boolean dontTrim = false)
        {
            result = "UNKNOWN";

            if (value.IsNull())
            {
                result = "NULL";
                return valueIfNull;
            }

            if (value.IsEmpty())
            {
                result = "EMPTY";
                return valueIfEmpty;
            }

            if (value.IsWhiteSpace())
            {
                result = "WHITESPACE";
                return valueIfWhitespace;
            }

            result = "SANE";
            return dontTrim ? value : value.Trim();
        }

        public static String IsSane(this String value, out String result, Boolean dontTrim = false)
            => IsSane(value, null, null, null, out result, dontTrim);

        public static String IsSane(this String value, Boolean dontTrim = false)
            => IsSane(value, null, null, null, out String dump, dontTrim);

    }
}
