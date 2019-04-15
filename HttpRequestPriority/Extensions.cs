using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace OrderedMvcActionMethods
{
    public static class Extensions
    {
        public static ApiDescription ResolveActionUsingAttribute(this IEnumerable<ApiDescription> apiDescriptions)
        {
            ApiDescription returnDescription = null;
            int currentPriority = 0;

            foreach (var item in apiDescriptions.Where(f => f.ActionDescriptor.ActionConstraints.Any(a => a.GetType() == typeof(HttpRequestPriority))))
            {
                //check the current HttpRequestPriority and return the highest
                var priority = (HttpRequestPriority)item.ActionDescriptor.ActionConstraints.FirstOrDefault(a => a.GetType() == typeof(HttpRequestPriority));

                if (priority != null && (int)priority.Priority > currentPriority)
                {
                    currentPriority = (int)priority.Priority;
                    returnDescription = item;
                }
            }

            if (returnDescription == null)
                returnDescription = apiDescriptions.First();

            return returnDescription;
        }
    }
}