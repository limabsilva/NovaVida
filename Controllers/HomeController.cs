using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NovaVida.Models;
using NovaVida.Services;

namespace NovaVida.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(string pesquisar)
    {
        if (!String.IsNullOrEmpty(pesquisar))
        {
            Crawler crawler = new Crawler();
            var productsList = crawler.Pesquisar();
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
