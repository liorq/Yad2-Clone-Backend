using asp.net_workshop_real_app_public.Data;
using asp.net_workshop_real_app_public.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace asp.net_workshop_real_app_public.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApartementContext _context;
        public AccountRepository(ApartementContext context,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<IdentityResult> SignUp(SignupModel signupModel)
        {
            AppUser user = new()
            {
                FirstName = signupModel.FirstName,
                LastName = signupModel.LastName,
                Email = signupModel.Email,
                UserName = signupModel.Email
            };
            var result = await _userManager.CreateAsync(user, signupModel.Password);

            return result;
        }

        public async Task<string> Login(LoginModel loginModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false);
            if (!result.Succeeded)
            {
                return null;
            }
            string token = NewToken(loginModel.Email);
            return token;
        }
        public async Task<bool> updateUserInfo(UserUpdateRequest user,string email)
        {
            var result=await _userManager.FindByEmailAsync(email);
            result.PhoneNumber = user.Phone;
            result.FirstName = user.FirstName;
            result.LastName = user.LastName;
            result.City=user.City;
            result.StreetName = user.StreetName;
            result.HouseNumber = user.HouseNumber;
            result.BirthDate = user.BirthDate;
            await _context.SaveChangesAsync();
            return true;

    }
        public string? getUserNameByToken()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (token == "")
                return "you dont have token send";

            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);
            return decodedToken?.Claims?.ToArray()[0]?.Value;
        }
   
        public async Task<AppUser> getUserObject(string email)
        {
        
            var res = await _userManager.FindByEmailAsync(email);
            return res;
        }
        private string NewToken(string email)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
