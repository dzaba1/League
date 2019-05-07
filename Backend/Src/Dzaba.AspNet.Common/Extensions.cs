using System.Collections.Generic;
using Dzaba.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Dzaba.AspNet.Common
{
    public static class Extensions
    {
        public static void CopyErrorsFrom(this ModelStateDictionary modelState, IEnumerable<IdentityError> errors)
        {
            Require.NotNull(modelState, nameof(modelState));
            Require.NotNull(errors, nameof(errors));

            foreach (var error in errors)
            {
                modelState.AddModelError(error.Code, error.Description);
            }
        }
    }
}
