using System.IO.Compression;
using Microsoft.Extensions.Caching.Memory;

namespace EncryptionUtility.Services;

public class ArchiveHelperService
{
    private readonly IMemoryCache _memoryCache;

    public ArchiveHelperService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public FileNameInfo AddFileToZip(string fileName, MemoryStream fileStream)
    {
        var fileId = Guid.NewGuid().ToString();
        var zip = Create(fileStream);
        var fileInfo = new FileInfo(fileName, zip); 
        _memoryCache.Set(fileId, fileInfo, TimeSpan.FromMinutes(5));
        return new FileNameInfo(fileId, fileName);
    }

    public FileInfo? TryGetFile(string fileId)
    {
        return _memoryCache.TryGetValue(fileId, out FileInfo? file) ? file : null;
    }

    public byte[] Create(MemoryStream inputFile)
    {
        using var memoryStream = new MemoryStream();
        using var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);
        
        var demoFile = archive.CreateEntry("foo.txt");
        //using var entryStream = demoFile.Open();
        //inputFile.CopyTo(entryStream);
        using var sw = new StreamWriter(demoFile.Open());
        sw.WriteLine("Etiam eros nunc, hendrerit nec malesuada vitae, pretium at ligula.");
        
        // using (FileStream fs = new FileStream(@"C:\Users\maksy\OneDrive\Робочий стіл\test.zip", FileMode.Create))
        // {
        //     memoryStream.Position = 0;
        //     memoryStream.CopyTo(fs);
        // }

        memoryStream.Position = 0;
        return memoryStream.ToArray();
    }
}