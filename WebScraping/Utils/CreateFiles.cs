using Newtonsoft.Json;
using WebScraping.Model;

namespace WebScraping.Utils;

public class CreateFiles
{
    private static readonly string directoryPath = @"C:\Excels";
    private static readonly string filePath = Path.Combine(directoryPath, "Dados.json");

    public static void CreateItemJson(List<Item> Items)
    {
        List<Item> existingItems = ReadItemJson();

        existingItems.AddRange(Items);

        string json = JsonConvert.SerializeObject(existingItems, Formatting.Indented);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        File.WriteAllText(filePath, json);
    }

    private static List<Item> ReadItemJson() 
    {
        if(File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Item>>(json);
        }else
        {
            return new List<Item>();
        }
    }
}
