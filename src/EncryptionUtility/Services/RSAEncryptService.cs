using System.Security.Cryptography;
using Microsoft.Extensions.Caching.Memory;
using EncryptionUtility.Models;

namespace EncryptionUtility.Services;

public class RSAEncryptService
{
    private readonly IMemoryCache _memoryCache;

    public RSAEncryptService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    
    public FileNameInfo CreateEncryptedFile(string fileId, string fileName, MemoryStream fileStream, string publicKey)
    {
        var fileInfo = new FileNameContent(fileName, CreateEncryptedBytes(fileStream, publicKey)); 
        _memoryCache.Set(fileId, fileInfo, TimeSpan.FromMinutes(5));
        return new FileNameInfo(fileId, fileName);
    }
    
    public FileNameInfo CreateDecryptedFile(string fileId, string fileName, MemoryStream fileStream, string privateKey)
    {
        var fileInfo = new FileNameContent(fileName, CreateDecryptedBytes(fileStream, privateKey));
        _memoryCache.Set(fileId, fileInfo, TimeSpan.FromMinutes(5));
        return new FileNameInfo(fileId, fileName);
    }

    public FileNameContent? TryGetFile(string fileId)
    {
        return _memoryCache.TryGetValue(fileId, out FileNameContent? file) ? file : null;
    }

    private byte[] CreateEncryptedBytes(MemoryStream fileStream, string publicKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(publicKey);
        return rsa.Encrypt(fileStream.ToArray(), RSAEncryptionPadding.Pkcs1);
    }
    
    private byte[] CreateDecryptedBytes(MemoryStream fileStream, string privateKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(privateKey);
        return rsa.Decrypt(fileStream.ToArray(), RSAEncryptionPadding.Pkcs1);
    }
}