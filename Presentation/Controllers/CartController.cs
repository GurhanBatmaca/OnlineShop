using Microsoft.AspNetCore.Mvc;
using Presentation.Identity.Abstract;

namespace Presentation.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class CartController: Controller
    {
        private readonly IHttpContextAccessor? _accessor;
        private readonly ISignService? _signService;
        private readonly IUserService? _userService;
        public CartController(IHttpContextAccessor? accessor,ISignService signService,IUserService userService)
        {
            _accessor = accessor;
            _signService = signService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }

}