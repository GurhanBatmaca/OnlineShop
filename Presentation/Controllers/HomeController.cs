using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extentions;
using Presentation.Models;
using Shared.Models;

namespace Presentation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        TempData.Put("message",new MessageModel
        {
            Title = $"Başarılı.",
            Message = "Ekleme başarılı",
            AlertType = "success"
        });
        
        return View();
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
