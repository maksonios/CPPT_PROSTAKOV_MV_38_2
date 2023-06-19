using System.Security.Cryptography;

namespace EncryptionUtility.Services;

public class RSAKeyGenerationService
{
    public string GeneratePrivateKey()
    {
        using var rsa = RSA.Create();
        rsa.KeySize = 2048;
        return rsa.ExportPkcs8PrivateKeyPem();
    }

    public string GeneratePublicKey(string privateKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(privateKey.AsSpan());
        return rsa.ExportSubjectPublicKeyInfoPem();
    }
}

