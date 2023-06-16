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
        if (algorithm == Algorithm.None)
            return "Please, select one of algorithms to proceed";

        using var hashAlgorithm = GetHashAlgorithm(algorithm);
        var hashBytes = hashAlgorithm.ComputeHash(stream);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }

    private static HashAlgorithm GetHashAlgorithm(Algorithm algorithm)
    {
        return algorithm switch
        {
            Algorithm.MD5 => MD5.Create(),
            Algorithm.SHA1 => SHA1.Create(),
            Algorithm.SHA256 => SHA256.Create(),
            _ => throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, null)
        };
    }
}