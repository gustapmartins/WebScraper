using EasyAutomationFramework;
using WebScraping.Validation;
using WebScraping.Model;
using OpenQA.Selenium;
using System.Data;
using WebScraping.Utils;

namespace WebScraping.Driver;

public class WebScraper : Web
{
    private DataTable GetData(string link)
    {
        try
        {
            StartBrowser();

            List<Item> items = new();

            Navigate(link);

            var searchField = GetValue(TypeElement.Name, "q").element;

            searchField.SendKeys("Playstation 5");

            searchField.SendKeys(Keys.Enter);

            WaitForLoad();

            var elements = GetValue(TypeElement.Xpath, "//*[@id=\"bGmlqc\"]/div/div/div/div/div[2]/div")
                .element.FindElements(By.ClassName("pla-unit"));

            foreach (var element in elements)
            {

                Item item = new()
                {
                    title = GetElementTextIfExists(element, By.ClassName("pymv4e")),
                    price = GetElementTextIfExists(element, By.ClassName("e10twf")),
                    discount = GetElementTextIfExists(element, By.ClassName("zPEcBd")),
                    linkAttribute = GetElementAttributeIfExists(element, By.ClassName("pla-unit-title-link"), "href"),
                };

                if (string.IsNullOrEmpty(item.title) || string.IsNullOrEmpty(item.price) || string.IsNullOrEmpty(item.discount) || string.IsNullOrEmpty(item.linkAttribute))
                {
                    // Se algum campo estiver vazio, pula para a próxima iteração
                    continue;
                }


                if (CheckPrice.CheckPriceValidation(item))
                {
                    items.Add(item);
                }
            }

            CreateFiles.CreateItemJson(items);

            return Base.ConvertTo(items);

        }catch(Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    private string GetElementTextIfExists(IWebElement parentElement, By by)
    {
        try
        {
            var element = parentElement.FindElement(by);
            return element.Text;
        }
        catch (NoSuchElementException)
        {
            return null;
        }
    }

    private string GetElementAttributeIfExists(IWebElement parentElement, By by, string attributeName)
    {
        try
        {
            var element = parentElement.FindElement(by);
            return element.GetAttribute(attributeName);
        }
        catch (NoSuchElementException)
        {
            return null;
        }
    }

    public async Task ExecuteJob(string linkSite)
    {
        try
        {
            var computers = GetData(linkSite);
            //var paramss = new ParamsDataTable("Dados", @"C:\Excels", new List<DataTables>()
            //{
            //    new("Computers", computers)
            //});
            //Base.GenerateExcel(paramss);
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
