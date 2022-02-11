using System;
using System.Linq;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Mvc.ModelBinding;

        public static partial class Web
        {
            public static Dictionary<String, String[]> Errors(this ModelStateDictionary modelState)
                => modelState
                    .Where(
                        item => item.Value.Errors != null && item.Value.Errors.Any())
                    .ToDictionary(
                        item => item.Key, 
                        item => item.Value.Errors.Select(err => err.ErrorMessage).ToArray());

            public static Boolean IsNotValid(this ModelStateDictionary modelState)
                => !modelState.IsValid;

            public static Boolean IsNotValid(this ModelStateDictionary modelState, out Dictionary<String, String[]> errors)
            {
                if(modelState.IsValid)
                {
                    errors = new Dictionary<String, String[]>();
                    return false;
                }

                errors = Errors(modelState);
                return true;
            }

            private static void remove(ModelStateDictionary dic, IEnumerable<String> keys)
                => keys.Each(key => dic.Remove(key));

            public static void Remove(this ModelStateDictionary modelState, IEnumerable<String> keys)
                => remove(modelState, keys.Where(key => key.IsNotEmptyOrWhiteSpace()));

            public static void Remove(this ModelStateDictionary modelState, params String[] keys)
                => Remove(modelState, keys?.AsEnumerable() ?? Enumerable.Empty<String>());
        }
    }
}
