using EncryptionUtility.Services;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionUtility.Controllers;

public record RsaKey(string PrivateKey, string PublicKey);

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

    [HttpPost]
    public RsaKey GenerateRsaKey([FromForm] string keySize)
    {
        var privateKey = _service.GeneratePrivateKey(keySize);
        var publicKey = _service.GeneratePublicKey(privateKey);
        return new RsaKey(privateKey, publicKey);
    }

    [HttpPost]
    public RsaKey GeneratePublicRsaKey([FromForm] string privateKey)
    {
        var publicKey = _service.GeneratePublicKey(privateKey);
        return new RsaKey(privateKey, publicKey);
    }
}