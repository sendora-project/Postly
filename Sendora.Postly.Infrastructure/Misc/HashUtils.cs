using System.Security.Cryptography;
using System.Text;

namespace Sendora.Core.Misc;

public class HashUtils
{
    public static string HashString(string input)
    {
        using var sha256 = SHA256.Create();
        
        var byteArray = Encoding.UTF8.GetBytes(input);
        var hashArray = sha256.ComputeHash(byteArray);
        
        var hexString = new StringBuilder(hashArray.Length * 2);
        foreach (var b in hashArray) {
            hexString.Append($"{b:x2}");
        }
        
        return hexString.ToString();
    }
}