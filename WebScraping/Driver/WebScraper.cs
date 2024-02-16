using EasyAutomationFramework;
using EasyAutomationFramework.Model;
using WebScraping.Model;
using OpenQA.Selenium;
using System.Data;
using WebScraping.Validation;

namespace WebScraping.Driver;

public class WebScraper : Web
{
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
                var item = new Item()
                {
                    title = element.FindElement(By.ClassName("ui-search-item__title")).Text,
                    price = element.FindElement(By.ClassName("andes-money-amount__fraction")).Text,
                    description = null,
                    link = element.FindElement(By.ClassName("ui-search-link__title-card")).GetAttribute("href")
                };

                if (CheckPrice.CheckPriceValidation(item, 6000))
                {
                    items.Add(item);
                }
            }

            return Base.ConvertTo(items);

        }finally
        {
            CloseBrowser();
        }
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
