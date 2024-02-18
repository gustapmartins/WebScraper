using Newtonsoft.Json;
using WebScraping.Model;

namespace WebScraping.Utils;

public class CreateFiles
{
    private static readonly string directoryPath = @"C:\Excels";
    private static readonly string filePath = Path.Combine(directoryPath, "Dados.json");

    public static void CreateItemJson(List<Item> Items)
    {
        string json = JsonConvert.SerializeObject(Items, Formatting.Indented);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        File.WriteAllText(filePath, json);
    }
}
