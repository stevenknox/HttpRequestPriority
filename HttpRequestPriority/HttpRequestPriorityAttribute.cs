using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace OrderedMvcActionMethods
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpRequestPriority : Attribute, IActionConstraint
    {
        public readonly Priority Priority;

        public HttpRequestPriority(Priority priority = Priority.First)
        {
            Priority = priority;
        }

        public int Order
        {
            get
            {
                return 0;
            }
        }

        public bool Accept(ActionConstraintContext context)
        {
            if (Priority == Priority.First || context.Candidates.Count == 1)
                return true;

            //check the other candidates
            foreach (var item in context.Candidates.Where(f => !f.Equals(context.CurrentCandidate)))
            {
                var attr = item.Action.ActionConstraints.FirstOrDefault(f => f.GetType() == typeof(HttpRequestPriority));

                if (attr == null)
                {
                    return true;
                }
                else
                {
                    HttpRequestPriority x = attr as HttpRequestPriority;
                    if (x.Priority > Priority)
                        return false;
                }
            }

            return true;
        }
    }
}