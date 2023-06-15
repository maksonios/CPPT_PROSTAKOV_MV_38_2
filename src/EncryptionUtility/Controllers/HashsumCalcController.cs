using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionUtility.Controllers;

public class HashsumCalcController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<string> Upload([FromForm] IFormFile[] files)
    {
        using var md5 = MD5.Create();
        using var stream = new MemoryStream();
        await files[0].CopyToAsync(stream);
        var hashBytes = await md5.ComputeHashAsync(stream);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }
}