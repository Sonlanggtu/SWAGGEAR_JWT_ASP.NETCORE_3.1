using DemoMiddleware.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DemoMiddleware.Filter
{
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" >>> OnAuthorization");

            // W
            var user = context.HttpContext.Items["User"];
            if (true)
            {

            }

            // Check Claims with Ingration Identity
            //var claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
            //var permissionsClaim = context.HttpContext.User.Claims.SingleOrDefault(c => c.Type == SystemConstants.UserClaim.Permissions);
            //if (permissionsClaim != null)
            //{
            //    var permissions = JsonConvert.DeserializeObject<List<string>>(permissionsClaim.Value);
                //var functionArr = _function.ToString().Split("_");
                //string functionId = string.Join(".", functionArr);
                //if (!permissions.Contains(functionId + "_" + _action))
                //{
                //    context.Result = new ForbidResult();
                //}
            //}
            //else
            //{
            //    context.Result = new ForbidResult();
            //}

        }
    }
}
