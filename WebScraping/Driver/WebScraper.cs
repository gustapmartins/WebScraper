using EasyAutomationFramework;
using EasyAutomationFramework.Model;
using WebScraping.Validation;
using WebScraping.Model;
using OpenQA.Selenium;
using System.Data;
using Newtonsoft.Json;
using sun.swing;

namespace WebScraping.Driver;

public class WebScraper : Web
{
    public static void ConvertItemJson(List<Item> Items)
    {
        string json = JsonConvert.SerializeObject(Items, Formatting.Indented);

        string filePath = @"S:\Excels\Dados.json";

        // Escrever o JSON em um arquivo
        File.WriteAllText(filePath, json);
    }

    public DataTable GetData(string link)
    {
        try
        {
            StartBrowser();

            var items = new List<Item>();

            Navigate(link);

            AssignValue(TypeElement.Id, "cb1-edit", "Playstation 5");

            Click(TypeElement.Xpath, "/html/body/header/div/div[2]/form/button");

            WaitForLoad();

            var elements = GetValue(TypeElement.Xpath, "//*[@id=\"root-app\"]/div/div[2]/section/ol")
                .element.FindElements(By.ClassName("ui-search-layout__item"));

            foreach (var element in elements)
            {

                var discountExist = element.FindElement(By.ClassName("ui-search-price__second-line__label")).Text;

                var item = new Item()
                {
                    title = element.FindElement(By.ClassName("ui-search-item__title")).Text,
                    price = element.FindElement(By.ClassName("andes-money-amount__fraction")).Text,
<<<<<<< HEAD
                    description = null,
                    link = element.FindElement(By.ClassName("ui-search-link__title-card")).GetAttribute("href")
                };

                if (CheckPrice.CheckPriceValidation(item, 6000))
=======
                    discount = !string.IsNullOrEmpty(discountExist) ? discountExist : "N/A",
                    link = element.FindElement(By.ClassName("ui-search-link__title-card")).GetAttribute("href")
                };

                if (CheckPrice.CheckPriceValidation(item, 7000))
>>>>>>> fbf7ab4ac3e1a1fd9aa1623fb6bd12ba2e174a72
                {
                    // Verificar se houve redução de preço
                    if (CheckPriceReduction(item))
                    {
                        items.Add(item);
                    }
                }
            }

            ConvertItemJson(items);

            return Base.ConvertTo(items);

        }finally
        {
            CloseBrowser();
        }
    }

    private bool CheckPriceReduction(Item item)
    {
        // Recuperar histórico de preços do produto, se existir
        List<Item> historicalPrices = GetHistoricalPrices(item);

        if (historicalPrices.Count > 0)
        {
            // Comparar o preço atual com o último preço registrado
            decimal currentPrice = decimal.Parse(item.price);
            decimal lastPrice = decimal.Parse(historicalPrices.Last().price);

            return currentPrice < lastPrice;
        }

        // Se não houver histórico de preços, considerar como uma promoção
        return true;
    }

    private List<Item> GetHistoricalPrices(Item item)
    {
        string filePath = @"S:\Excels\Dados.json";
        List<Item> historicalPrices = new List<Item>();

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            historicalPrices = JsonConvert.DeserializeObject<List<Item>>(json);
        }

        return historicalPrices;
        return new List<Item>();
    }

    public async Task ExecuteJob(string linkSite)
    {
        try
        {
            var computers = GetData(linkSite);
            var paramss = new ParamsDataTable("Dados", @"C:\Excel", new List<DataTables>()
            {
                new("Computers", computers)
            });
            Base.GenerateExcel(paramss);
            Console.WriteLine("Cron job executado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro ao executar o cron job: {ex.Message}");
        }finally
        {
            CloseBrowser();
        }
    }
}
