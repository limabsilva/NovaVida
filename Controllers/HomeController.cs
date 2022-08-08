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
    private readonly IProductsRepository _productsRepository;

    public HomeController(ILogger<HomeController> logger, ICrawlerService crawlerService, IProductsRepository productsRepository)
    {
        _logger = logger;
        _crawlerService = crawlerService;
        _productsRepository = productsRepository;
    }

    public IActionResult Index(string strSearch)
    {
        if (!String.IsNullOrEmpty(strSearch))
        {            
            var productsList = _crawlerService.SearchProduct(strSearch);
            if(productsList != null)
            {
                ViewBag.ProductsList = productsList;
                _productsRepository.SaveManyProducts(productsList);
            }            
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
