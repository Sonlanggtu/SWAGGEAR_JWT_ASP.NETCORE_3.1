using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMiddleware.Filter
{
    public class ValidateModel : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" >>> OnResultExecuting");
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" >>> OnResultExecuted");
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" >>> OnActionExecuting");
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" >>> OnActionExecuted");
            if (context.ModelState.IsValid == false)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
