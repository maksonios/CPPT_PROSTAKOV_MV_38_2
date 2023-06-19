using System.Security.Cryptography;

namespace EncryptionUtility.Services;

public class RSAKeyGenerationService
{
    public string GeneratePrivateKey(string keySize)
    {
        using var rsa = RSA.Create();
        switch (keySize)
        {
            case "1024":
                rsa.KeySize = 1024;
                break;
            case "2048":
                rsa.KeySize = 2048;
                break;
            case "4096":
                rsa.KeySize = 4096;
                break;
            case "None":
                return "Choose the key size first.";
        }
        return rsa.ExportPkcs8PrivateKeyPem();
    }

    public string GeneratePublicKey(string privateKey)
    {
        if (privateKey == "Choose the key size first.")
        {
            return "Choose the key size first.";
        }
        
        using var rsa = RSA.Create();
        rsa.ImportFromPem(privateKey.AsSpan());
        return rsa.ExportSubjectPublicKeyInfoPem();
    }
}

