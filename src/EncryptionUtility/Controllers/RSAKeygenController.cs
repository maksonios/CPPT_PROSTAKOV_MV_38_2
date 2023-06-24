using EncryptionUtility.Services;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionUtility.Controllers;

public record RsaKey(string PrivateKey, string PublicKey);

[Route("rsa-keygen")]
public class RSAKeygenController : Controller
{
    private readonly RSAKeyGenerationService _service;
    
    private static readonly string[] ValidKeySizes = { "1024", "2048", "4096" };

    public RSAKeygenController(RSAKeyGenerationService service)
    {
        _service = service;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("generate-pair")]
    public IActionResult GenerateRsaKey([FromForm] string keySize)
    {
        if (!ValidKeySizes.Contains(keySize))
            return Problem("Key size is not valid", statusCode: 400);
        
        var privateKey = _service.GeneratePrivateKey(keySize);
        var publicKey = _service.GeneratePublicKey(privateKey);
        return new ObjectResult(new RsaKey(privateKey, publicKey));
    }

    [HttpPost("generate-public")]
    public string GeneratePublicRsaKey([FromForm] string privateKey)
    {
        var publicKey = _service.GeneratePublicKey(privateKey);
        return publicKey;
    }
}