using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MameToppleApi.Helpers;
using MameToppleApi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MameToppleApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JwtHelpers _jwt;

        public TokenController(JwtHelpers jwt)
        {
            _jwt = jwt;
        }


        /// <summary>
        /// 登入取得Token
        /// </summary>
        /// <param name="login"></param>
        /// <returns>回傳Token</returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult<string> SignIn(LoginViewModel login)
        {
            if (ValidateUser(login))
            {
                return _jwt.GenerateToken(login.Username);
            }
            else
            {
                return BadRequest();
            }
        }
        private bool ValidateUser(LoginViewModel login)
        {
            return true; // TODO 和資料庫比對使用者登入資料
        }

        [Authorize] //通過驗證才能存取
        [HttpGet]
        public IActionResult GetClaims()
        {
            return Ok(User.Claims.Select(p => new { p.Type, p.Value }));
        }

        [Authorize] //通過驗證才能存取
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