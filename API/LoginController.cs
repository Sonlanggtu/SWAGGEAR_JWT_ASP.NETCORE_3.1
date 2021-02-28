using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoMiddleware.IServices;
using DemoMiddleware.Models;
using DemoMiddleware.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoMiddleware.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<object> Login(LoginModel model)
        {
            var user = model.Username; //await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var result = model.Password; //await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, true);
                //if (!result.Succeeded)
                if (user != "DamSon")
                {
                    return BadRequest("Mật khẩu không đúng");
                }
                return Ok(_loginService.Loginsync(new AppUser
                {
                    Id = Guid.NewGuid(),
                    UserName = model.Username,
                    Email = "Sondn@gmail.com",
                    PasswordHash = model.Password,
                    PhoneNumber = "099324324",
                    FullName = "DamNgocSon",
                    Adress = "Xuan Quan, Van Giang, Hung Yen"
                }));
            }
            else
            {
                return NotFound($"Không tìm thấy tài khoản {model.Username}");
            }
        }


    }
}
