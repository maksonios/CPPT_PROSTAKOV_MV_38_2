using Microsoft.Extensions.Caching.Memory;

namespace EncryptionUtility.Services;

public class AESEncryptService
{
    private readonly IMemoryCache _memoryCache;

    public AESEncryptService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    public FileInfo? TryGetFile(string fileId)
    {
        return _memoryCache.TryGetValue(fileId, out FileInfo? file) ? file : null;
    }
}