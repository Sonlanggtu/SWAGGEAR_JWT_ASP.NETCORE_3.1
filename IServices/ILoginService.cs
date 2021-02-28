using DemoMiddleware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMiddleware.IServices
{
    public interface ILoginService
    {
        object Loginsync(AppUser user);
    }
}
