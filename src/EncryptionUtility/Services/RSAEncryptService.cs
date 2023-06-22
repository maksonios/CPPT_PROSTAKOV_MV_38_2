using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace EncryptionUtility.Services;

public class RSAEncryptService
{
    private readonly IMemoryCache _memoryCache;

    public RSAEncryptService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    
    public MemoryStream CreateEncryptedFile(IFormFile file, string publicKey)
    {
        byte[] fileBytes;
        using (var memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            fileBytes = memoryStream.ToArray();
        }
        
        byte[] publicKeyBytes = Encoding.UTF8.GetBytes(publicKey);
        using (var rsa = RSA.Create())
        {
            rsa.ImportFromPem(publicKey);
            byte[] encryptedBytes = rsa.Encrypt(fileBytes, RSAEncryptionPadding.Pkcs1);
            var encryptedStream = new MemoryStream(encryptedBytes);
            return encryptedStream;
        }
    }
    
    // public Dictionary<string, byte[]> encryptedFiles = new Dictionary<string, byte[]>();
    //
    // public byte[] RetrieveEncryptedFile(string fileId)
    // {
    //     if (encryptedFiles.TryGetValue(fileId, out var encryptedFile))
    //     {
    //         // File found, return the encrypted content
    //         return encryptedFile;
    //     }
    //
    //     return null; // File not found
    // }

}