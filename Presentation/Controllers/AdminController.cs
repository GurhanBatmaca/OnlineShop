using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Presentation.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles = "SuperAdmin")]
    public class AdminController: Controller
    {
        private readonly ICategoryService? _categoryService;
        public AdminController(ICategoryService? categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> AddProduct()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> AddProduct(ProductModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            return View();
        }
    }
}