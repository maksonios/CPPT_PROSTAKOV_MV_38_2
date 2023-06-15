using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EncryptionUtility.Models;

namespace EncryptionUtility.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Privacy()
    {
        return View();
    }
    
    [HttpPost]
    public void Upload([FromForm] IFormFile file)
    {
        Console.WriteLine(file.Length);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}