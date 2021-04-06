using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniBook;
using MiniBook.Strings;
using MiniBookIdentity.Models;
using MiniBookIdentity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBookIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        UserManager<User> _userManager { get; }
        public AccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string GetById(int id)
        {
            return "value";
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            var user = new User() { Email = registerViewModel.Email, UserName = registerViewModel.Email };

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (result.Succeeded)
            {
                return this.OkResult();
            }
            else if (result.Errors.Any(x => x.Code == "DuplicateUserName"))
            {
                /*this.ErrorResult((int)ErrorCode.REGISTER_DUPLICATE_USER_NAME, 
                    ErrorResource.ResourceManager.GetString("REGISTER_DUPLICATE_USER_NAME"));*/
                this.ErrorResult(ErrorCode.REGISTER_DUPLICATE_USER_NAME);
            }
            return this.ErrorResult(ErrorCode.BAD_REQUEST);
        }

        [HttpPost("Login")]
        public void Login([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
