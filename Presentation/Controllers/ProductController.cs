using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{

    [AutoValidateAntiforgeryToken]
    public class ProductController: Controller
    {
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor? _accessor;

        public ProductController(IProductService productService,IHttpContextAccessor? accessor)
        {
            _productService = productService;
            _accessor = accessor;
        }
        public async Task<IActionResult> Products(string kategori,int sayfa=1)
        {
            var model = await _productService.GetProductsByCategory(kategori,sayfa);
            return View(model);
        }

        public async Task<IActionResult> Details(string url)
        {
            var model = await _productService.GetProductDetails(url);
            return View(model);
        }

        public async Task<IActionResult> Search(string sorgu,int sayfa=1)
        {
            var model = await _productService.GetSearchResult(sorgu,sayfa);
            return View(model);
        }
    }
}