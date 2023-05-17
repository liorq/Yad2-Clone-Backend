using asp.net_workshop_real_app_public.Models;
using Microsoft.AspNetCore.Identity;

namespace asp.net_workshop_real_app_public.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUp(SignupModel signupModel);
        Task<string> Login(LoginModel loginModel);
    }
}