using EncryptionUtility.Services;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionUtility.Controllers;

[Route("aes-encrypt")]
public class AESEncryptController : Controller
{
    private readonly AESEncryptService _service;

    public AESEncryptController(AESEncryptService service)
    {
        _service = service;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("upload-encrypt")]
    public void UploadEncrypt(IFormFile file)
    {
        Console.Write(file.Length);
    }
    
    [HttpPost("upload-decrypt")]
    public void UploadDecrypt(IFormFile file)
    {
        Console.Write(file.Length);
    }

    [Route("download/{fileId}")]
    public IActionResult Download(string fileId)
    {
        var file = _service.TryGetFile(fileId);
        if (file == null)
            return NotFound();

        return File(file.File, "application/octet-stream", file.Name);
    }
}