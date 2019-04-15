using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OrderedMvcActionMethods.Controllers
{
   public class BaseApiController : Controller
    {
        [HttpGet] 
        [HttpRequestPriority(Priority.Last)]
        public ActionResult<IEnumerable<object>> Get()
        {
            return new string[] { "value from base", "another value from base" };
        }

    }

}