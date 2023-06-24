using EncryptionUtility.Extensions;
using EncryptionUtility.Services;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionUtility.Controllers;

[Route("rsa-signature")]
public class RSASignatureController : Controller
{
    private readonly RSASignatureService _service;
    
    public RSASignatureController(RSASignatureService service)
    {
        _service = service;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost("generate")]
    public async Task<string> GenerateSignature([FromForm] IFormFile file, [FromForm] string privateKey)
    {
        var fileStream = await file.GetMemoryStream();
        return _service.GenerateSignature((MemoryStream) fileStream, privateKey);
    }

    [HttpPost("verify")]
    public async Task<bool> VerifySignature([FromForm] IFormFile file, [FromForm] string publicKey, [FromForm] string signature)
    {
        var fileStream = await file.GetMemoryStream();
        return _service.VerifySignature((MemoryStream)fileStream, publicKey, signature);
    }
}