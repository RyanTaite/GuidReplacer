using System.Text.RegularExpressions;

namespace GuidReplacer;

public static class Program
{
    public static void Main(string[] args)
    {
        // Set your file path
        const string filePath = @"<Your File Here>";
        
        const int numberOfGuids = 100;

        var guids = new string[numberOfGuids];
        for (var i = 0; i < numberOfGuids; i++)
        {
            guids[i] = Guid.NewGuid().ToString();
        }

        var content = File.ReadAllText(filePath);
        var index = 0;
        
        // For each ID = Guid.NewGuid() found in `content`, replace with $"ID = Guid.Parse(\"{guids[index++]}\"
        
        // Pattern to match ID = Guid.NewGuid()
        const string pattern = @"ID\s*=\s*Guid\.NewGuid\(\)";
        
        // Replacement logic with incremented index
        content = Regex.Replace(content, pattern, match => index < guids.Length // Ensure the index is within bounds
            ? $"ID = Guid.Parse(\"{guids[index++]}\")"
            : match.Value);

        File.WriteAllText(filePath, content);
        Console.WriteLine("Replacement complete!");
    }
}