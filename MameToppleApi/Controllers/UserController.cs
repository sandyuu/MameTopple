using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MameToppleApi.Repository;
using MameToppleApi.Models.ViewModels;
using MameToppleApi.Interfaces;
using MameToppleApi.Models;
using Microsoft.Extensions.Logging;

namespace MameToppleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // GET: api/User/admin@gmail.com
        /// <summary>
        /// 取得使用者個人資料
        /// </summary>
        /// <param name="id">帳號Email</param>
        /// <returns>User資料</returns>
        [HttpGet("{id}")]
        public ActionResult<UserViewModel> GetUser(string id)
        {
            var userVM = _userService.GetById(id);
            if (userVM == null)
            {
                return NotFound();
            }
            _logger.LogDebug("Test logger = 1(Debug)");
            _logger.LogInformation("我是資訊:Info");
            _logger.LogWarning("我是警告:Warn");
            return userVM;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutUser(string id, User user)
        // {
        //     if (id != user.Account)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(user).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!UserExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // POST: api/User
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.

        /// <summary>
        /// 註冊帳號
        /// </summary>
        /// <param name="user">User資料模型</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateUser(User user) //Create新增註冊
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool success = _userService.Create(user);
            if (!success)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }

        // DELETE: api/User/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<User>> DeleteUser(string id)
        // {
        //     var user = await _context.Users.FindAsync(id);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Users.Remove(user);
        //     await _context.SaveChangesAsync();

        //     return user;
        // }

        // private bool UserExists(string id)
        // {
        //     return _context.Users.Any(e => e.Account == id);
        // }
    }
}
