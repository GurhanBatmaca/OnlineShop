using System.Diagnostics;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;

    public HomeController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index(int sayfa=1)
    {
        // TempData.Put("message",new MessageModel
        // {
        //     Title = $"Başarılı.",
        //     Message = "Ekleme başarılı",
        //     AlertType = "success"
        // });

        var model = await _productService.GetHomePageProducts(sayfa);
        
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
