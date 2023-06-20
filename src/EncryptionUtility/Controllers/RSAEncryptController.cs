using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionUtility.Controllers;

[Route("rsa-encrypt")]
public class RSAEncryptController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost("upload")]
    public void Upload([FromForm] IFormFile file)
    {
        Console.Write(file.Length);
    }
    
    [Route("download")]
    public IActionResult GetBlobDownload()
    {
        var s = "test!";
        var content = new MemoryStream(Encoding.UTF8.GetBytes(s));
        var contentType = "APPLICATION/octet-stream";
        var fileName = "something.txt";
        return File(content, contentType, fileName);
    }
}