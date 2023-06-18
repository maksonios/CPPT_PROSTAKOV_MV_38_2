using System.Security.Cryptography;
using EncryptionUtility.Services;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionUtility.Controllers;

public record RsaKey(string PrivateKey, string PublicKey);

// public record GenerateRsaKeyRequest(string Type);

public class RSAKeygenController : Controller
{
    private readonly RSAKeyGenerationService _service;

    public RSAKeygenController(RSAKeyGenerationService service)
    {
        _service = service;
    }
    public IActionResult Index()
    {
        return View();
    }
    
//    public RsaKey GenerateRsaKey([FromBody] GenerateRsaKeyRequest request)

    [HttpPost]
    public RsaKey GenerateRsaKey()
    {
        var privateKey = _service.GeneratePrivateKey();
        var publicKey = _service.GeneratePublicKey(privateKey);
        return new RsaKey(privateKey, publicKey);
    }
}