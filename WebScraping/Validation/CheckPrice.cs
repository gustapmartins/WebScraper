using Newtonsoft.Json;
using System.Text.RegularExpressions;
using WebScraping.Model;

namespace WebScraping.Validation
{
    public static class CheckPrice
    {
        private static readonly string directoryPath = @"C:\Excels";
        private static readonly string filePath = Path.Combine(directoryPath, "Dados.json");

        public static bool CheckPriceValidation(Item item)
        {
            // Recuperar histórico de preços do produto, se existir
            List<Item> historicalPrices = GetHistoricalPrices();

            if (historicalPrices.Count > 0)
            {
                int currentPrice = ConvertStringOrInt(item);

                Item ProdutoComMenorPreco = historicalPrices.OrderBy(p => p.price).FirstOrDefault();

                int lastPrice = ConvertStringOrInt(ProdutoComMenorPreco);

                return currentPrice < lastPrice;
            }

            return true;
        }

        private static List<Item> GetHistoricalPrices()
        {
            List<Item> historicalPrices = new();

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                historicalPrices = JsonConvert.DeserializeObject<List<Item>>(json);
            }

            return historicalPrices;
        }

        private static int ConvertStringOrInt(Item item)
        {
            string apenasNumeros = Regex.Replace(item.price, @"[^\d,]", "").Split(",")[0];
            return int.Parse(apenasNumeros);
        }
    }
}
