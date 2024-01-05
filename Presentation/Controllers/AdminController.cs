using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extentions;
using Shared.Models;

namespace Presentation.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles = "SuperAdmin")]
    public class AdminController: Controller
    {
        private readonly ICategoryService? _categoryService;
        private readonly IProductService? _productService;
        public AdminController(ICategoryService? categoryService,IProductService? productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            var categoryListModel = await _categoryService!.GetAllAsync();
            ViewBag.Categories = categoryListModel.Categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductModel model,int[] categoriesIds)
        {
            var categoryListModel = await _categoryService!.GetAllAsync();
            ViewBag.Categories = categoryListModel.Categories;

            if(!ModelState.IsValid)
            {
                return View(model);
            }
            if(await _productService!.CreateAsync(model,categoriesIds))
            {
          
                TempData.Put("infoMessage",new MessageModel
                {
                    Title = $"Başarılı",
                    Message = $"{_productService.Message}",
                    AlertType = "success"
                });

                return RedirectToAction("Index");

            }

            TempData.Put("infoMessage",new MessageModel
            {
                Title = $"Hata",
                Message = $"{_productService.Message}",
                AlertType = "danger"
            });
            return View(model);
        }
    
        public async Task<IActionResult> ProductList()
        {
            var model = await _productService!.GetAllProductsAsync();
            return View(model);
        }
    
    }
}