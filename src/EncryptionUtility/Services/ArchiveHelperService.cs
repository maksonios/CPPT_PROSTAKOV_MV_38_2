using EncryptionUtility.Models;
using Ionic.Zip;
using Microsoft.Extensions.Caching.Memory;

namespace EncryptionUtility.Services;

public class ArchiveHelperService
{
    private const string ArchiveName = "archive.zip";
    
    private readonly IMemoryCache _memoryCache;

    public ArchiveHelperService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public FileNameInfo CreateArchive(FileNameContent[] files, string password)
    {
        var stream = new MemoryStream();
        using (var zip = new ZipFile())
        {
            zip.Password = password;
            zip.Encryption = EncryptionAlgorithm.WinZipAes256;

            foreach (var file in files) 
                zip.AddEntry(file.Name, file.File);

            zip.Save(stream);
        }
        
        var fileId = Guid.NewGuid().ToString();
        var fileInfo = new FileNameContent(ArchiveName, stream.ToArray());
        _memoryCache.Set(fileId, fileInfo, TimeSpan.FromMinutes(5));
        return new FileNameInfo(fileId, ArchiveName);
    }

    public FileNameContent? TryGetFile(string fileId)
    {
        return _memoryCache.TryGetValue(fileId, out FileNameContent? file) ? file : null;
    }
}