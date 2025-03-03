using System.Text.RegularExpressions;

namespace Sendora.Core.Misc;

public static class MailUtils
{
    // Regular expression to match a typical email address
    const string Pattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";
    
    public static string? ExtractAddress(string input)
    {
        var match = Regex.Match(input, Pattern);

        return match.Success ? match.Value :
            null;
    }
}