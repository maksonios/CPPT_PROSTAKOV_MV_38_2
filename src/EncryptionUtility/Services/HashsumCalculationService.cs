using System.Security.Cryptography;

namespace EncryptionUtility.Services;

public enum Algorithm
{
    None = 0,
    MD5,
    SHA1,
    SHA256
}

public class HashsumCalculationService
{
    public string CalculateHash(Stream stream, Algorithm algorithm)
    {
        return algorithm switch
        {
            Algorithm.MD5 => CalculateMD5(stream),
            Algorithm.SHA1 => CalculateSHA1(stream),
            Algorithm.SHA256 => CalculateSHA256(stream),
            _ => "Please, select one of algorithms to proceed"
        };
    }
    
    private static string CalculateMD5(Stream stream)
    {
        using var md5 = MD5.Create();
        var hashBytes = md5.ComputeHash(stream);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }
    
    private static string CalculateSHA1(Stream stream)
    {
        using var sha1 = SHA1.Create();
        var hashBytes = sha1.ComputeHash(stream);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }
    
    private static string CalculateSHA256(Stream stream)
    {
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(stream);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }
}