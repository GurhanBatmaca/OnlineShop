using System.Diagnostics;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extentions;
using Presentation.Models;
using Shared.Models;

namespace Presentation.Controllers;

[AutoValidateAntiforgeryToken]
public class HomeController : Controller
{
    private readonly IProductService _productService;

    public HomeController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index(int sayfa=1)
    {
         TempData.Put("cartMessage",new MessageModel
        {
            Title = $"Başarılı",
            Message = $"message",
            AlertType = "success"
        });
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
