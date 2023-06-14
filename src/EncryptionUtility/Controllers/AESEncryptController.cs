using Microsoft.AspNetCore.Mvc;

namespace EncryptionUtility.Controllers;

public class AESEncryptController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public void Upload([FromForm] IFormFile[] files)
    {
        Console.WriteLine(files.Length);
    }
}