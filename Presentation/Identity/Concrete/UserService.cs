using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Shared.Helpers;
using Shared.Models;

namespace Presentation.Identity.Abstract
{
    public class UserService: IUserService 
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUrlHelperFactory  _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;
        public UserService(SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager,IUrlHelperFactory urlHelperFactory,IActionContextAccessor actionContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;

        }

        public string? Message { get ; set ; }

        public async Task<bool> Register(RegisterModel model)
        {

            if(!CheckInput.IsValid(model.Password!))
            {
                Message = $"{CheckInput.ErrorMessage}";
                return false;
            }
            var checkEmailExist = await _userManager.FindByEmailAsync(model.Email!);
            if(checkEmailExist is not null)
            {
                Message = "Bu e-posta adresi ile daha önce üye olunmuş.Giriş sayfasınadan giriş yapabilirsiniz.";
                return false;
            }
            var checkUserNameExist = await _userManager.FindByNameAsync(model.UserName!);
            if(checkUserNameExist is not null)
            {
                Message = "Bu kullanıcı adı daha önce alınmış.Lütfen farklı bir tane seçin.";
                return false;
            }

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
                Message = "Hesabınız oluşturuldu,Lütfen email adresinize gelen link ile hesabınızı onaylayın.";

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext!);

                var url = urlHelper.Action("ComfirmEmail","Auth", new {
                    token = token,
                    userId = user.Id
                });

           
                return true;
            }

            Message = "Hesap oluşturulamadı!Şifre en az 6 karakter uzunluğunda olmalı ve büyük-küçük harf,rakam ve özel karakter (_& vs) içermelidir.";
            return false;
        }

    }
}