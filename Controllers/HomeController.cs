using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NovaVida.Models;
using NovaVida.Services;
using NovaVida.Interfaces;

namespace NovaVida.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICrawlerService _crawlerService;

    public HomeController(ILogger<HomeController> logger, ICrawlerService crawlerService)
    {
        _logger = logger;
        _crawlerService = crawlerService;
    }

    public IActionResult Index(string pesquisar)
    {
        if (!String.IsNullOrEmpty(pesquisar))
        {            
            var productsList = _crawlerService.Pesquisar(pesquisar);
            ViewBag.ProductsList = productsList;
        }
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
