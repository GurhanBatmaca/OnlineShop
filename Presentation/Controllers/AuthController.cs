using Business.Abstract;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extentions;
using Presentation.Identity.Abstract;
using Shared.Models;

namespace Presentation.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AuthController: Controller
    {
        private readonly ISignService _signService;
        private readonly IUserService _userService;
        private readonly ICartService? _cartService;
        public AuthController(ISignService signService,IUserService userService,ICartService? cartService)
        {
            _signService = signService;
            _userService = userService;
            _cartService = cartService;
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
                TempData.Put("toastMessage",new MessageModel
                {
                    Title = $"Merhaba;",
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

            TempData.Put("toastMessage",new MessageModel
            {
                Title = $"Başarılı",
                Message = $"Hesaptan çıkış yapıldı",
                AlertType = "warning"
            });

            return RedirectToAction("Login");
        }
    
        [HttpGet]
        public IActionResult Register()
        {
            if(User.Identity!.IsAuthenticated)
            {
                return Redirect("~/");
            }
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
                TempData.Put("infoMessage",new MessageModel
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
            if(await _userService.ComfirmEmail(token,userId))
            {
                TempData.Put("infoMessage",new MessageModel
                {
                    Title = $"Başarılı",
                    Message = $"{_userService.Message}",
                    AlertType = "success"
                });

                await _cartService!.CreateAsync(new Cart{UserId=userId});

                return RedirectToAction("Login");
            }

            TempData.Put("infoMessage",new MessageModel
            {
                Title = $"Hata",
                Message = $"{_userService.Message}",
                AlertType = "danger"
            });

            return RedirectToAction("Register");
        }

        [HttpGet]
        public IActionResult FargotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FargotPassword(FargotPasswordModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            if(await _userService.FargotPassword(model))
            {
                TempData.Put("infoMessage",new MessageModel
                {
                    Title = $"Başarılı",
                    Message = $"{_userService.Message}",
                    AlertType = "warning"
                });

                return RedirectToAction("Login");
            }

            ModelState.AddModelError("",$"{_userService.Message}");
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string token,string userId)
        {
            if(string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
            {
                TempData.Put("infoMessage",new MessageModel
                {
                    Title = $"Hata",
                    Message = $"Geçersiz link",
                    AlertType = "danger"
                });
                return RedirectToAction("FargotPassword");
            }
            var model = new ResetPasswordModel
            {
                Token = token,
                UserId = userId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            if(await _userService.ResetPassword(model))
            {
                TempData.Put("infoMessage",new MessageModel
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
    
    }
}