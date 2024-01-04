using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles = "SuperAdmin")]
    public class AdminController: Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}