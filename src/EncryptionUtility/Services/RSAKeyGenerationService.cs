using System.Security.Cryptography;

namespace EncryptionUtility.Services;

public class RSAKeyGenerationService
{
    public string GeneratePrivateKey(string keySize)
    {
        using var rsa = RSA.Create();
        rsa.KeySize = ParseKeySize(keySize);
        return rsa.ExportPkcs8PrivateKeyPem();
    }

    public string GeneratePublicKey(string privateKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(privateKey.AsSpan());
        return rsa.ExportSubjectPublicKeyInfoPem();
    }

    private int ParseKeySize(string keySize)
    {
        return keySize switch
        {
            "1024" => 1024,
            "2048" => 2048,
            "4096" => 4096,
            _ => throw new ArgumentOutOfRangeException(nameof(keySize), keySize, null)
        };
    }
}

