using System.Security.Cryptography;
using Microsoft.Extensions.Caching.Memory;

namespace EncryptionUtility.Services;

public record FileNameInfo(string Id, string Name);
public record FileInfo(string Name, byte[] File);

public class RSAEncryptService
{
    private readonly IMemoryCache _memoryCache;

    public RSAEncryptService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    
    public FileNameInfo CreateEncryptedFile(string fileId, string fileName, MemoryStream fileStream, string publicKey)
    {
        var fileInfo = new FileInfo(fileName, CreateEncryptedMemoryStream(fileStream, publicKey)); 
        _memoryCache.Set(fileId, fileInfo, TimeSpan.FromMinutes(5));
        return new FileNameInfo(fileId, fileName);
    }

    public FileInfo? TryGetEncryptedFile(string fileId)
    {
        return _memoryCache.TryGetValue(fileId, out FileInfo? file) ? file : null;
    }

    private byte[] CreateEncryptedMemoryStream(MemoryStream fileStream, string publicKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(publicKey);
        return rsa.Encrypt(fileStream.ToArray(), RSAEncryptionPadding.Pkcs1);
    }
}