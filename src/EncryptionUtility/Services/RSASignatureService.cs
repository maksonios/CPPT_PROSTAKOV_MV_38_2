using System.Security.Cryptography;

namespace EncryptionUtility.Services;

public class RSASignatureService
{
    public string GenerateSignature(MemoryStream fileStream, string privateKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(privateKey);
        var signatureArray = rsa.SignData(fileStream, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        return Convert.ToBase64String(signatureArray);
    }

    public bool VerifySignature(MemoryStream fileStream, string publicKey, string signature)
    {
        var signatureBytes = Convert.FromBase64String(signature);
        using var rsa = RSA.Create();
        rsa.ImportFromPem(publicKey);
        return rsa.VerifyData(fileStream, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }
}