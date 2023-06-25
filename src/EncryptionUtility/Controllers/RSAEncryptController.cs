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
    
    [HttpPost("encrypt")]
    public async Task<FileNameInfo> UploadEncrypt([FromForm] IFormFile file, [FromForm] string publicKey)
    {   
        var fileId = Guid.NewGuid().ToString();
        var fileName = "encrypted_" + file.FileName;
        var fileStream = await file.GetMemoryStream();
        return _service.CreateEncryptedFile(fileId, fileName, (MemoryStream) fileStream, publicKey);
    }
    
    [HttpPost("decrypt")]
    public async Task<FileNameInfo> UploadDecrypt([FromForm] IFormFile file, [FromForm] string privateKey)
    {   
        var fileId = Guid.NewGuid().ToString();
        var fileName = "decrypted_" + file.FileName;
        var fileStream = await file.GetMemoryStream();
        return _service.CreateDecryptedFile(fileId, fileName, (MemoryStream) fileStream, privateKey);
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