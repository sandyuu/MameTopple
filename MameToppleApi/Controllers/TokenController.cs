using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MameToppleApi.Helpers;
using MameToppleApi.Interfaces;
using MameToppleApi.Models;
using MameToppleApi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MameToppleApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JwtHelpers _jwt;
        private readonly ToppleDBContext _context;
        private readonly IUserService _userService;

        public TokenController(JwtHelpers jwt, ToppleDBContext context, IUserService userService)
        {
            _jwt = jwt;
            _context = context;
            _userService = userService;
        }


        /// <summary>
        /// 登入取得Token
        /// </summary>
        /// <param name="login"></param>
        /// <returns>回傳Token</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> SignIn(LoginViewModel login)
        {
            if (await ValidateUser(login))
            {
                return _jwt.GenerateToken(login.Account, "admin@gmail.com");
            }
            else
            {
                return BadRequest();
            }
        }
        private async Task<bool> ValidateUser(LoginViewModel login)
        {
            if (await _userService.SignInCheck(login))
            {
                return true;
            }
            return false;
        }

        [Authorize] //通過驗證才能存取
        [HttpGet]
        public IActionResult GetClaims()
        {
            return Ok(User.Claims.Select(p => new { p.Type, p.Value }));
        }

        [Authorize(Roles = "Admin")] //通過驗證才能存取
        [HttpGet]
        public IActionResult GetUserName()
        {
            return Ok(User.Identity.Name);
        }

        [HttpGet]
        public IActionResult GetUniqueId() //取得Token的GUID
        {
            var jti = User.Claims.FirstOrDefault(p => p.Type == "jti");
            return Ok(jti.Value);
        }
    }
}