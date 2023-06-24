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
    
    public FileNameInfo CreateDecryptedFile(string fileId, string fileName, MemoryStream fileStream, string privateKey)
    {
        var fileInfo = new FileInfo(fileName, CreateDecryptedMemoryStream(fileStream, privateKey));
        _memoryCache.Set(fileId, fileInfo, TimeSpan.FromMinutes(5));
        return new FileNameInfo(fileId, fileName);
    }

    public FileInfo? TryGetFile(string fileId)
    {
        return _memoryCache.TryGetValue(fileId, out FileInfo? file) ? file : null;
    }

    private byte[] CreateEncryptedMemoryStream(MemoryStream fileStream, string publicKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(publicKey);
        return rsa.Encrypt(fileStream.ToArray(), RSAEncryptionPadding.Pkcs1);
    }
    
    private byte[] CreateDecryptedMemoryStream(MemoryStream fileStream, string privateKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(privateKey);
        return rsa.Decrypt(fileStream.ToArray(), RSAEncryptionPadding.Pkcs1);
    }
}