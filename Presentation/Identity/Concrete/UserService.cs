using Microsoft.AspNetCore.Identity;
using Presentation.EmailServices.Abstract;
using Shared.Helpers;
using Shared.Models;

namespace Presentation.Identity.Abstract
{
    public class UserService: IUserService 
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _accessor;
        private readonly LinkGenerator _generator;

        public UserService(SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,IEmailSender emailSender,IHttpContextAccessor accessor, LinkGenerator generator)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _accessor = accessor;
            _generator = generator;
        }

        public string? Message { get ; set ; }

        public async Task<bool> Register(RegisterModel model)
        {
            var checkEmailExist = await _userManager.FindByEmailAsync(model.Email!);
            if(checkEmailExist is not null)
            {
                Message = "Bu e-posta adresi ile daha önce üye olunmuş.Giriş sayfasınadan giriş yapabilirsiniz.";
                return false;
            };
            var checkUserNameExist = await _userManager.FindByNameAsync(model.UserName!);
            if(checkUserNameExist is not null)
            {
                Message = "Bu kullanıcı adı daha önce alınmış.Lütfen farklı bir tane seçin.";
                return false;
            };

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                FirstName = model.FirsName,
                LastName = model.LastName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user,model.Password!);

            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user,"Customer");
                Message = "Hesabınız oluşturuldu,Lütfen email adresinize gelen link ile hesabınızı onaylayın.";

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var url = _generator.GetUriByAction(_accessor.HttpContext!, 
                    action: "confirmemail", 
                    values: new { token = token, userId = user.Id }
                );

                try
                {
                    await _emailSender.SendEmailAsync(user.Email!,"Üyelik onayı",$"Hesabınızı onaylamak için lütfen <a href='{url}'>linke</a> tıklayınız");
                    return true;
                }
                catch (Exception)
                {                   
                    await _userManager.DeleteAsync(user);
                    Message = "Tanımlanmamış email adresi,lütfen geçerli bir email adresi girin.";
                    return false;
                }        
                
            };

            Message = "Hesap oluşturulamadı!Şifre en az 6 karakter uzunluğunda olmalı ve büyük-küçük harf,rakam ve özel karakter (_& vs) içermelidir.";
            return false;
        }

        public async Task<bool> ComfirmEmail(string token,string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if(user is null)
            {
                Message = "Kullanıcı bulunamadı";
                return false;
            }
            if(string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
            {
                Message = "Link hatası";
                return false;
            }

            var result = await _userManager.ConfirmEmailAsync(user!,token);

            if(result.Succeeded)
            {
                Message = "Üyeliğiniz onaylandı,giriş yapabilirsiniz";
                return true;
            }

            Message = "Token hatası";
            return false;
        }

        public async Task<bool> FargotPassword(FargotPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email!);

            if(user is null)
            {
                Message = "Bu e-posta ile kayıtlı kullanıcı bulunamadı.";
                return false;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var url = _generator.GetUriByAction(_accessor.HttpContext!, 
                action: "resetpassword", 
                values: new { token = token, userId = user.Id }
            );

            await _emailSender.SendEmailAsync(user.Email!,"Şifre sıfırlama",$"Şifrenizi sıfırlamak için lütfen <a href='{url}'>linke</a> tıklayınız");

            Message = "Şifrenizi sıfırlamak için lütfen e-posta adresinizi kontrol edin";

            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordModel model)
        {

            if(string.IsNullOrEmpty(model.Token) || string.IsNullOrEmpty(model.UserId))
            {
                Message = "Geçersiz adres";
                return false;
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if(user is null)
            {
                Message = "Kullanıcı bulunamadı";
                return false;
            }

            var result = await _userManager.ResetPasswordAsync(user,model.Token,model.Password!);

            if(result.Succeeded)
            {
                Message = "Şifre değiştirildi,belirlediğiniz şifreyi kullanarak giriş yapabilirsiniz";
                return true;
            }

            Message = "Token hatası";
            return false;
        }

        public string? GetUserId(HttpContext httpContext)
        {
            return _userManager.GetUserId(httpContext.User);
        }

    }
}