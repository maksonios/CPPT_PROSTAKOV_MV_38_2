using EncryptionUtility.Extensions;
using EncryptionUtility.Services;
using Microsoft.AspNetCore.Mvc;
using FileInfo = EncryptionUtility.Services.FileInfo;

namespace EncryptionUtility.Controllers;

[Route("archive-helper")]
public class ArchiveHelperController : Controller
{
    private readonly ArchiveHelperService _service;

    public ArchiveHelperController(ArchiveHelperService service)
    {
        _service = service;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost("upload")]
    public async Task<FileNameInfo> Upload(string password)
    {
        var files = await GetFiles(Request.Form.Files);
        return _service.CreateArchive(files, password);
    }
    
    [Route("download/{fileId}")]
    public IActionResult Download(string fileId)
    {
        var file = _service.TryGetFile(fileId);
        if (file == null)
            return NotFound();

        return File(file.File, "application/zip", "archive.zip");
    }

    private async Task<FileInfo[]> GetFiles(IFormFileCollection files)
    {
        var result = new FileInfo[files.Count];
        for (var i = 0; i < files.Count; i++)
        {
            var stream = await files[i].GetMemoryStream();
            result[i] = new FileInfo(files[i].FileName, ((MemoryStream)stream).ToArray());
        }
        return result;
    }
}