using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionUtility.Controllers;

public class HashsumCalcController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public class HashResult
    {
        public string? Md5Hash { get; set; }
        public string? Sha1Hash { get; set; }
        public string? Sha256Hash { get; set; }
    }

    private static string CalculateHash(HashAlgorithm algorithm, Stream stream)
    {
        stream.Position = 0;
        var hashBytes = algorithm.ComputeHash(stream);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }

    [HttpPost]
    public async Task<ActionResult<HashResult>> Upload([FromForm] IFormFile file)
    {
        using var md5 = MD5.Create();
        using var sha1 = SHA1.Create();
        using var sha256 = SHA256.Create();
        
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);

        var hashResult = new HashResult
        {
            Md5Hash = CalculateHash(md5, stream),
            Sha1Hash = CalculateHash(sha1, stream),
            Sha256Hash = CalculateHash(sha256, stream)
        };

        return hashResult;
    }
}