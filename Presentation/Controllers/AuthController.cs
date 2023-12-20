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
        public AuthController(ISignService signService)
        {
            _signService = signService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            if(!CheckInput.IsValid(model.Email!) || !CheckInput.IsValid(model.Password!))
            {
                ModelState.AddModelError("",$"{CheckInput.ErrorMessage}");
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

            TempData.Put("message",new MessageModel
            {
                Title = $"Hata",
                Message = $"{_signService.Message}",
                AlertType = "danger"
            });
            
            return View(model);
        }
    }
}