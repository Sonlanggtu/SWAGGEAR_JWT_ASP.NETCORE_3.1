using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMiddleware.Middleware
{
    public class InjectMiddleware
    {
        private readonly RequestDelegate _next;

        public InjectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("Middleware 3 =>");
            await _next.Invoke(context);
            await context.Response.WriteAsync("Edit Middleware 3 =>");
        }
    }
}
