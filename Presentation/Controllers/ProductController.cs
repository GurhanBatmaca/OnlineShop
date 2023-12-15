using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ProductController: Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Products(string kategori,int sayfa=1)
        {
            return View();
        }
    }
}