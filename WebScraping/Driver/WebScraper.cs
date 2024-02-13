using EasyAutomationFramework;
using EasyAutomationFramework.Model;
using WebScraping.Model;
using OpenQA.Selenium;
using System.Data;

namespace WebScraping.Driver;

public class WebScraper : Web
{
    public static bool CheckPrice(Item item, int priceLimit)
    {
        var price = item.price.Replace("$", "").Replace(".", "");
        return int.Parse(price) < priceLimit;
    }

    public DataTable GetData(string link)
    {
        StartBrowser();

        var items = new List<Item>();

        Navigate(link);

        //AssignValue(TypeElement.Id, "searchInputId", "Texto da pesquisa");

        var elements = GetValue(TypeElement.Xpath, "/html/body/div[1]/div[3]/div/div[2]/div[1]")
            .element.FindElements(By.ClassName("thumbnail"));

        foreach(var element in elements)
        {
            var item = new Item()
            {
                title = element.FindElement(By.ClassName("title")).Text,
                price = element.FindElement(By.ClassName("price")).Text,
                description = element.FindElement(By.ClassName("description")).Text
            };

            if(CheckPrice(item, 3000))
            {
                items.Add(item);
            }
        }

        return Base.ConvertTo(items);
    }


    public async Task ExecuteJob()
    {
        try
        {
            var computers = GetData("https://www.webscraper.io/test-sites/e-commerce/allinone/computers");
            var paramss = new ParamsDataTable("Dados", @"S:\Excels", new List<DataTables>()
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
