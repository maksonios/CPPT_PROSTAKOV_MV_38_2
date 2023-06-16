using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionUtility.Controllers;

public class AESEncryptController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        // Read the file contents into a byte array
        using (var memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();

            // Encrypt the file using AES256
            byte[] encryptedBytes = EncryptFile(fileBytes);


            string filePath = "C:\\Users\\maksy\\OneDrive\\Робочий стіл\\New folder";
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await fileStream.WriteAsync(encryptedBytes);
            }

            // Return a success response
            return Ok("File encrypted and saved successfully");
        }
    }

    private byte[] EncryptFile(byte[] fileBytes)
    {
        // Generate a random 256-bit key and IV (Initialization Vector)
        byte[] key;
        byte[] iv;
        using (Aes aes = Aes.Create())
        {
            aes.GenerateKey();
            aes.GenerateIV();
            key = aes.Key;
            iv = aes.IV;
        }

        // Create an AES256 encryptor using the generated key and IV
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            // Perform the encryption
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(fileBytes, 0, fileBytes.Length);
                    cryptoStream.FlushFinalBlock();
                }

                // Return the encrypted file as a byte array
                return memoryStream.ToArray();
            }
        }
    }
}