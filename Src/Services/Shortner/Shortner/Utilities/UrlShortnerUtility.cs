using System.Security.Cryptography;
using System.Text;

namespace Shortner.Utilities;

public class UrlShortnerUtility
{
    private readonly Dictionary<string, string> _urlMappings = new Dictionary<string, string>();
    private const string BaseUrl = "http://localhost:5152/";

    public string ShortenUrl(string originalUrl)
    {
        // Generate a unique short code
        var shortCode = GenerateShortCode(originalUrl);
        
        // Store mapping
        _urlMappings[shortCode] = originalUrl;

        // Return the full short URL
        return BaseUrl + shortCode;
    }

    public string RetrieveUrl(string shortCode)
    {
        // Lookup the original URL
        return _urlMappings.TryGetValue(shortCode, out var originalUrl) ? originalUrl : null;
    }

    private string GenerateShortCode(string url)
    {
        using (var sha256 = SHA256.Create())
        {
            // Hash the URL
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(url));
            // Convert to a base64 string
            var hashString = Convert.ToBase64String(hashBytes);
            // Take the first 6 characters and replace characters to make it URL safe
            return hashString.Substring(0, 6).Replace("+", "").Replace("/", "").Replace("=", "");
        }
    }
}