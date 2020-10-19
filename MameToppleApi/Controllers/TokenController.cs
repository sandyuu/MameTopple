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
        /// 登入取得Bearer Token
        /// </summary>
        /// <param name="login"></param>
        /// <returns>回傳Bearer Token</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginViewModel login)
        {
            if (await ValidateUser(login))
            {
                return _jwt.GenerateToken(login.Account);
            }
            else
            {
                return BadRequest();
            }
        }
        private async Task<bool> ValidateUser(LoginViewModel login)
        {
            if (await _userService.LoginVerify(login))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 取得Token內的資訊（需Admin權限）
        /// </summary>
        /// <returns>Token資訊</returns>
        [Authorize(Roles = "Admin")] //通過驗證才能存取
        [HttpGet]
        public IActionResult GetClaims()
        {
            return Ok(User.Claims.Select(p => new { p.Type, p.Value }));
        }

        /// <summary>
        /// 取得使用者的名稱（帳號Email）
        /// Header夾帶Bearer Token
        /// </summary>
        /// <returns>回傳使用者的名稱（帳號Email）</returns>
        [Authorize] //通過驗證才能存取
        [HttpGet]
        public IActionResult GetUserName()
        {
            return Ok(User.Identity.Name);
        }

        /// <summary>
        /// 取得Token的GUID
        /// </summary>
        /// <returns>GUID</returns>
        [Authorize] //通過驗證才能存取
        [HttpGet]
        public IActionResult GetUniqueId() //取得Token的GUID
        {
            var jti = User.Claims.FirstOrDefault(p => p.Type == "jti");
            return Ok(jti.Value);
        }
    }
}