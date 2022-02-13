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
            public static IDictionary<String, String[]> Errors(this ModelStateDictionary modelStateDictionary)
                => modelStateDictionary
                    .Where(
                        item => item.Value.Errors != null && item.Value.Errors.Any())
                    .ToDictionary(
                        item => item.Key, 
                        item => item.Value.Errors.Select(err => err.ErrorMessage).ToArray());

            public static Boolean IsNotValid(this ModelStateDictionary modelStateDictionary)
                => !modelStateDictionary.IsValid;

            public static Boolean IsNotValid(this ModelStateDictionary modelStateDictionary, out IDictionary<String, String[]> errors)
            {
                if(modelStateDictionary.IsValid)
                {
                    errors = default(IDictionary<String, String[]>);
                    return false;
                }

                errors = Errors(modelStateDictionary);
                return true;
            }

            public static void Remove(this ModelStateDictionary modelStateDictionary, IEnumerable<String> keys)
                => keys.Each(key => modelStateDictionary.Remove(key));

            public static void Remove(this ModelStateDictionary modelStateDictionary, params String[] keys)
                => Remove(modelStateDictionary, keys ?? Enumerable.Empty<String>());
        }
    }
}
