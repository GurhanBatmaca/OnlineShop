using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public IActionResult Index()
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
    
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if(id is null)
            {
                TempData.Put("infoMessage",new MessageModel
                {
                    Title = $"Hata",
                    Message = $"Eksik adres",
                    AlertType = "danger"
                });
                return RedirectToAction("ProductList");
            }
            if(await _productService!.DeleteProduct((int)id))
            {
                TempData.Put("infoMessage",new MessageModel
                {
                    Title = $"Başarılı",
                    Message = $"{_productService.Message}",
                    AlertType = "warning"
                });
                return RedirectToAction("ProductList");
            }
            TempData.Put("infoMessage",new MessageModel
            {
                Title = $"Hata",
                Message = $"{_productService.Message}",
                AlertType = "danger"
            });
            return RedirectToAction("ProductList");
        }
        
        public async Task<IActionResult> ProductList(int sayfa=1)
        {
            var model = await _productService!.GetAllProductsAsync(sayfa);
            return View(model);
        }
    
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int? id)
        {
            if(id is null)
            {
                TempData.Put("infoMessage",new MessageModel
                {
                    Title = $"Hata",
                    Message = $"Eksik adres",
                    AlertType = "danger"
                });
                return RedirectToAction("ProductList");
            }
            
            var categoryListModel = await _categoryService!.GetAllAsync();
            ViewBag.Categories = categoryListModel.Categories;
            var model = await _productService!.GetProductForUpdate((int)id!);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductModel model,int[] categoriesIds)
        {
            var categoryListModel = await _categoryService!.GetAllAsync();
            ViewBag.Categories = categoryListModel.Categories;
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            
            if(await _productService!.UpdateProductAsync(model,categoriesIds))
            {
                TempData.Put("infoMessage",new MessageModel
                {
                    Title = $"Ürün güncellendi",
                    Message = $"{_productService.Message}",
                    AlertType = "success"
                });
                return RedirectToAction("ProductList");
            }
            
            TempData.Put("infoMessage",new MessageModel
            {
                Title = $"Hata",
                Message = $"{_productService.Message}",
                AlertType = "danger"
            });
            return View(model);
        }
    
        [HttpGet]
        public async Task<IActionResult> AddCategory()
        {
            var categoryListModel = await _categoryService!.GetAllAsync();
            ViewBag.Categories = categoryListModel.Categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryModel model)
        {
            var categoryListModel = await _categoryService!.GetAllAsync();
            ViewBag.Categories = categoryListModel.Categories;
            
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            if(await _categoryService.CreateAsync(model))
            {
                TempData.Put("infoMessage",new MessageModel
                {
                    Title = $"Kategori eklendi",
                    Message = $"{_categoryService.Message}",
                    AlertType = "success"
                });

                return RedirectToAction("CategoryList");
            }

            TempData.Put("infoMessage",new MessageModel
            {
                Title = $"Hata",
                Message = $"{_categoryService.Message}",
                AlertType = "danger"
            });
            return View(model);
        }

        public async Task<IActionResult> CategoryList()
        {
            var model = await _categoryService!.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int? id)
        {
            if(id is null)
            {
                TempData.Put("infoMessage",new MessageModel
                {
                    Title = $"Hata",
                    Message = $"Adres hatası",
                    AlertType = "danger"
                });

                return RedirectToAction("CategoryList");
            }
            var model = await _categoryService!.GetByIdAsync((int)id);
            if(model is null)
            {
                TempData.Put("infoMessage",new MessageModel
                {
                    Title = $"Hata",
                    Message = $"Kategori bulunamadı",
                    AlertType = "danger"
                });

                return RedirectToAction("CategoryList");
            }
            var categoryListModel = await _categoryService!.GetAllAsync();
            ViewBag.Categories = categoryListModel.Categories;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var categoryListModel = await _categoryService!.GetAllAsync();
            ViewBag.Categories = categoryListModel.Categories;
            if(await _categoryService.UpdateAsync(model))
            {
                TempData.Put("infoMessage",new MessageModel
                {
                    Title = $"Kategori güncellendi",
                    Message = $"{_categoryService.Message}",
                    AlertType = "success"
                });

                return RedirectToAction("CategoryList");
            }
            TempData.Put("infoMessage",new MessageModel
            {
                Title = $"Hata",
                Message = $"{_categoryService.Message}",
                AlertType = "danger"
            });
            return View(model);
        }
        
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if(id is null)
            {
                TempData.Put("infoMessage",new MessageModel
                {
                    Title = $"Hata",
                    Message = $"Adres hatası",
                    AlertType = "danger"
                });

                return RedirectToAction("CategoryList");
            }
            
            if(await _categoryService!.DeleteAsync((int) id))
            {
                TempData.Put("infoMessage",new MessageModel
                {
                    Title = $"Kategori silindi",
                    Message = $"{_categoryService.Message}",
                    AlertType = "success"
                });

                return RedirectToAction("CategoryList");
            }
            
            TempData.Put("infoMessage",new MessageModel
            {
                Title = $"Hata",
                Message = $"{_categoryService.Message}",
                AlertType = "danger"
            });

            return RedirectToAction("CategoryList");
        }
    
        public async Task<IActionResult> OrderList(string orderState,int sayfa=1)
        {
            return View();
        }

    }
}