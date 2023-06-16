using EncryptionUtility.Services;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionUtility.Controllers;

public class HashsumCalcController : Controller
{
    private readonly HashsumCalculationService _service;

    public HashsumCalcController()
    {
        _service = new HashsumCalculationService();
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<string> Upload([FromForm] IFormFile file, [FromForm] Algorithm algorithm)
    {
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        stream.Position = 0;

        return _service.CalculateHash(stream, algorithm);
    }
}