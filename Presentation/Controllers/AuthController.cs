using Microsoft.AspNetCore.Mvc;
using Presentation.Extentions;
using Presentation.Identity.Abstract;
using Shared.Helpers;
using Shared.Models;

namespace Presentation.Controllers
{
    public class AuthController: Controller
    {
        private readonly ISignService _signService;
        private readonly IUserService _userService;
        public AuthController(ISignService signService,IUserService userService)
        {
            _signService = signService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if(User.Identity!.IsAuthenticated)
            {
                return Redirect("~/");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
           
            if(await _signService.Login(model))
            {
                TempData.Put("message",new MessageModel
                {
                    Title = $"Hoşgeldin",
                    Message = $"{_signService.Message}",
                    AlertType = "success"
                });

                return Redirect("~/");
            }

            ModelState.AddModelError("",$"{_signService.Message}");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {

            if(!User.Identity!.IsAuthenticated)
            {
                return Redirect("~/");
            }

            await _signService.Logout();
            TempData.Put("message",new MessageModel
            {
                Title = $"Çıkış yapıldı",
                Message = $"Hesaptan güvenli çıkış yapıldı",
                AlertType = "danger"
            });
            return RedirectToAction("Login");
        }
    
        [HttpGet]
        public IActionResult Register()
        {
            return View();
            
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            
            if(await _userService.Register(model))
            {
                TempData.Put("message",new MessageModel
                {
                    Title = $"Başarılı",
                    Message = $"{_userService.Message}",
                    AlertType = "success"
                });

                return RedirectToAction("Login");
            }

            ModelState.AddModelError("",$"{_userService.Message}");
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string token,string userId)
        {
            return View();
        }
    }
}