using EncryptionUtility.Extensions;
using EncryptionUtility.Services;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionUtility.Controllers;

[Route("archive-helper")]
public class ArchiveHelperController : Controller
{
    private readonly ArchiveHelperServiceReserve _service;

    public ArchiveHelperController(ArchiveHelperServiceReserve service)
    {
        _service = service;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost("upload")]
    public async Task<FileNameInfo> Upload()
    {
        var files = Request.Form.Files;
        
        var fileName = files[0].Name;
        var fileStream = await files[0].GetMemoryStream();
        return _service.AddFileToZip(fileName, (MemoryStream)fileStream);
    }
    
    [Route("download/{fileId}")]
    public IActionResult Download(string fileId)
    {
        var file = _service.TryGetFile(fileId);
        if (file == null)
            return NotFound();

        return File(file.File, "application/zip", "archive.zip");
    }
}