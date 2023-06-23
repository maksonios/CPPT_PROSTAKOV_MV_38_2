using EncryptionUtility.Extensions;
using EncryptionUtility.Services;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionUtility.Controllers;

[Route("rsa-encrypt")]
public class RSAEncryptController : Controller
{
    private readonly RSAEncryptService _service;

    public RSAEncryptController(RSAEncryptService service)
    {
        _service = service;
    }
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost("upload")]
    public async Task<FileNameInfo> Upload([FromForm] IFormFile file, [FromForm] string publicKey)
    {   
        var fileId = Guid.NewGuid().ToString();
        var fileName = "encrypted_" + file.FileName;
        var fileStream = await file.GetMemoryStream();
        return _service.CreateEncryptedFile(fileId, fileName, (MemoryStream) fileStream, publicKey);
    }
    
    [Route("download/{fileId}")]
    public IActionResult Download(string fileId)
    {
        var file = _service.TryGetEncryptedFile(fileId);
        if (file == null)
            return NotFound();
        
        return File(file.File, "application/octet-stream", file.Name);
    }
}