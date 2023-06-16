namespace EncryptionUtility.Extensions;

public static class FormFileExtensions
{
    public static async Task<Stream> GetMemoryStream(this IFormFile file)
    {
        var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        stream.Position = 0;
        return stream;
    }
}