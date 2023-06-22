using System.Net;
using System.Text;
using EncryptionUtility.Extensions;
using EncryptionUtility.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EncryptionUtility.Controllers;

[Route("rsa-encrypt")]
public class RSAEncryptController : Controller
{
    private readonly RSAEncryptService _service;
    private readonly IMemoryCache _memoryCache;

    public RSAEncryptController(RSAEncryptService service, IMemoryCache cache)
    {
        _service = service;
        _memoryCache = cache;
    }
    public IActionResult Index()
    {
        return View();
    }

    public record FileNameInfo(string Id, string Name);
    public record FileInfo(string Name, byte[] file);
    
    [HttpPost("upload")]
    public FileNameInfo Upload([FromForm] IFormFile file, [FromForm] string publicKey)
    {   
        var encryptedStream = _service.CreateEncryptedFile(file, publicKey);
        var fileId = Guid.NewGuid().ToString();
        var fileName = "encrypted_" + file.FileName;
        var fileInfo = new FileInfo(fileName, encryptedStream.ToArray());

        _memoryCache.Set(fileId, fileInfo, TimeSpan.FromMinutes(5));
        
        return new FileNameInfo(fileId, fileName);
    }
    
    [Route("download/{fileId}")]
    public IActionResult? Download(string fileId)
    {
        if (_memoryCache.TryGetValue(fileId, out FileInfo fileInfo))
        {
            return File(fileInfo.file, "application/octet-stream", fileInfo.Name);
        }
        return null;
    }
}