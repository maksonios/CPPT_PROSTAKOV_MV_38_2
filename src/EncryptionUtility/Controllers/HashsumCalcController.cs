using EncryptionUtility.Extensions;
using EncryptionUtility.Services;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionUtility.Controllers;

public class HashsumCalcController : Controller
{
    private readonly HashsumCalculationService _service;

    public HashsumCalcController(HashsumCalculationService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<string> Upload([FromForm] IFormFile file, [FromForm] Algorithm algorithm)
    {
        await using var stream = await file.GetMemoryStream();
        return _service.CalculateHash(stream, algorithm);
    }
}