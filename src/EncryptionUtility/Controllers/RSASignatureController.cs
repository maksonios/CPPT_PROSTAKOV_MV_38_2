using Microsoft.AspNetCore.Mvc;

namespace EncryptionUtility.Controllers;

public class RSASignatureController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public void Upload([FromForm] IFormFile file)
    {
        Console.WriteLine(file.Length);
    }
}