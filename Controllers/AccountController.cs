using asp.net_workshop_real_app_public.Models;
using asp.net_workshop_real_app_public.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asp.net_workshop_real_app_public.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("")]
        public async Task<IActionResult> Signup([FromBody] SignupModel signupModel)
        {
            var res = await _accountRepository.SignUp(signupModel);
            if (res.Succeeded)
            {
                return Ok(res.Succeeded);
            }
            return Unauthorized();
        }






        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel signinModel)
        {
            var res = await _accountRepository.Login(signinModel);
            if (string.IsNullOrWhiteSpace(res))
            {
                return Unauthorized();
            }
            return Ok(res);
        }

        [HttpPost("update")]
        public async Task<IActionResult> updateUserInfo([FromBody] UserUpdateRequest user)
        {
            string email = _accountRepository.getUserNameByToken();

            var res = await _accountRepository.updateUserInfo(user,email);
            if (res)
            {
                return Ok();
            }
            return BadRequest("cannot chage");
        }
    }
}
