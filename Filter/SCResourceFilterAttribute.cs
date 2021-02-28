using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMiddleware.Filter
{
    public class SCResourceFilterAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuting( ResourceExecutingContext context)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" >>>>> OnResourceExecuting");

            //As context.Result is of type IActionResult
            //So you can think what we can pass with it.
            //context.Result = new ContentResult()
            //{
            //    //We can set ContentType, StatusCode & Content
            //    //Set as per your need
            //    Content = "Resource unavailable - header should not be set"
            //};
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" >>>>> OnResourceExecuted");
        }


    }

}
