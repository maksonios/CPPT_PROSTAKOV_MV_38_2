using System.Security.Cryptography;

namespace EncryptionUtility.Services;

public class RSAKeyGenerationService
{
    
    public string GeneratePrivateKey()
    {
        using RSA rsa = RSA.Create();
        rsa.KeySize = 2048;

        byte[] privateKeyBytes = rsa.ExportPkcs8PrivateKey();
        string privateKeyBase64 = Convert.ToBase64String(privateKeyBytes, Base64FormattingOptions.InsertLineBreaks);
            
        string privateKeyWrapped = $"-----BEGIN RSA PRIVATE KEY-----\n{privateKeyBase64}\n-----END RSA PRIVATE KEY-----";
        return privateKeyWrapped;
    }

    public string GeneratePublicKey(string privateKey)
    {
        string privateKeyBase64 = ExtractBase64FromPrivateKey(privateKey);
        byte[] privateKeyBytes = Convert.FromBase64String(privateKeyBase64);

        using RSA rsa = RSA.Create();
        rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);

        RSAParameters privateKeyParams = rsa.ExportParameters(true);
        RSAParameters publicKeyParams = new RSAParameters
        {
            Modulus = privateKeyParams.Modulus,
            Exponent = privateKeyParams.Exponent
        };

        rsa.ImportParameters(publicKeyParams);

        byte[] publicKeyBytes = rsa.ExportSubjectPublicKeyInfo();
        string publicKeyBase64 = Convert.ToBase64String(publicKeyBytes, Base64FormattingOptions.InsertLineBreaks);

        string publicKeyWrapped = $"-----BEGIN PUBLIC KEY-----\n{publicKeyBase64}\n-----END PUBLIC KEY-----";
        return publicKeyWrapped;
    }
    
    private string ExtractBase64FromPrivateKey(string privateKey)
    {
        const string startMarker = "-----BEGIN RSA PRIVATE KEY-----";
        const string endMarker = "-----END RSA PRIVATE KEY-----";

        int startIndex = privateKey.IndexOf(startMarker) + startMarker.Length;
        int endIndex = privateKey.IndexOf(endMarker, startIndex);

        string base64 = privateKey.Substring(startIndex, endIndex - startIndex).Trim();
        return base64;
    }
}

