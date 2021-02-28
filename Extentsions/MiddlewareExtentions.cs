using DemoMiddleware.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMiddleware.Extentsions
{
    public static class MiddlewareExtentions
    {
        public static IApplicationBuilder UseMiddlewareExtention(this IApplicationBuilder applicationBuilder)
        {
           return applicationBuilder.UseMiddleware<InjectMiddleware>();
        }
    }
}
