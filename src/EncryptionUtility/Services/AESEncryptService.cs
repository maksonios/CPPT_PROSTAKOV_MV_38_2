using System.Security.Cryptography;
using System.Text;
using EncryptionUtility.Models;
using Microsoft.Extensions.Caching.Memory;

namespace EncryptionUtility.Services;

public class AESEncryptService
{
    private readonly IMemoryCache _memoryCache;

    public AESEncryptService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    
    public FileNameInfo CreateEncryptedFile(string fileId, string fileName, MemoryStream fileStream, string key)
    {
        var fileInfo = new FileNameContent(fileName, CreateEncryptedBytes(fileStream, key)); 
        _memoryCache.Set(fileId, fileInfo, TimeSpan.FromMinutes(5));
        return new FileNameInfo(fileId, fileName);
    }
    
    public FileNameInfo CreateDecryptedFile(string fileId, string fileName, MemoryStream fileStream, string key)
    {
        var fileInfo = new FileNameContent(fileName, CreateDecryptedBytes(fileStream, key));
        _memoryCache.Set(fileId, fileInfo, TimeSpan.FromMinutes(5));
        return new FileNameInfo(fileId, fileName);
    }
    
    public FileNameContent? TryGetFile(string fileId)
    {
        return _memoryCache.TryGetValue(fileId, out FileNameContent? file) ? file : null;
    }
    
    private byte[] CreateEncryptedBytes(MemoryStream fileStream, string key)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        byte[] bytes = fileStream.ToArray();

        using var aes = new RijndaelManaged
        {
            Key = CreateDeriveBytes(keyBytes, 32),
        };
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor();
        using var encryptedStream = new MemoryStream();
        using (var cryptoStream = new CryptoStream(encryptedStream, encryptor, CryptoStreamMode.Write))
        {
            cryptoStream.Write(bytes);
        }
        var encrypted = encryptedStream.ToArray();

        return aes.IV.Concat(encrypted).ToArray();
    }

    private byte[] CreateDecryptedBytes(MemoryStream fileStream, string key)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        byte[] encrypted = fileStream.ToArray();

        using var rijndeal = new RijndaelManaged
        {
            Key = CreateDeriveBytes(keyBytes, 32)
        };
        var iv = encrypted.Take(16).ToArray();
        rijndeal.IV = iv;

        var encryptedBytes = encrypted.Skip(16).ToArray();
        using var memoryStream = new MemoryStream();
        using var decryptor = rijndeal.CreateDecryptor();
        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
        {
            cryptoStream.Write(encryptedBytes);
        }

        return memoryStream.ToArray();
    }
    
    private static byte[] CreateDeriveBytes(byte[] keyBytes, int size)
    {
        using var sha512 = SHA512.Create();
        var saltBytes = sha512.ComputeHash(keyBytes);
        using var derivBytes = new Rfc2898DeriveBytes(keyBytes, saltBytes, 65536);
        return derivBytes.GetBytes(size);
    }
}