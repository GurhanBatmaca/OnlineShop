using Microsoft.AspNetCore.Identity;
using Shared.Models;

namespace Presentation.Identity.Abstract
{
    public class SignService : ISignService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public SignService(SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public string? Message { get ; set ; }


        public async Task<bool> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email!);
            
            if(user is null)
            {
                Message += "Bu email ile kayıtlı kullanıcı bulunamadı.";
                return false;
            }

            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                Message += "Lütfen e-posta adresinizi onaylayın.";
                return false;
            }

            var result = await _signInManager.PasswordSignInAsync(user,model.Password!,model.RememberMe,true);

            if(result.Succeeded)
            {
                await _userManager.ResetAccessFailedCountAsync(user);
                await _userManager.SetLockoutEndDateAsync(user,null);

                Message += $"{user.UserName}";
                return true;
            }
            else if(result.IsLockedOut)
            {
                var logoutDate = await _userManager.GetLockoutEndDateAsync(user);
                var timeLeft = logoutDate.Value - DateTime.UtcNow;
                Message = $"Hesabın kilitli, lütfen {timeLeft.Minutes} dakika sonra tekrar dene.";
            }

            Message = "Şifre Yanlış.";
            return false;

        }

    }
}