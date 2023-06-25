using EncryptionUtility.Extensions;
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

    [HttpPost("encrypt")]
    public async Task<FileNameInfoAES> UploadEncrypt([FromForm] IFormFile file, [FromForm] string key)
    {   
        var fileId = Guid.NewGuid().ToString();
        var fileName = "encrypted_" + file.FileName;
        var fileStream = await file.GetMemoryStream();
        return _service.CreateEncryptedFile(fileId, fileName, (MemoryStream) fileStream, key);
    }
    
    [HttpPost("decrypt")]
    public async Task<FileNameInfoAES> UploadDecrypt([FromForm] IFormFile file, [FromForm] string key)
    {   
        var fileId = Guid.NewGuid().ToString();
        var fileName = "decrypted_" + file.FileName;
        var fileStream = await file.GetMemoryStream();
        return _service.CreateDecryptedFile(fileId, fileName, (MemoryStream) fileStream, key);
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