using WebScraping.Driver;

public class Program
{
    public static async Task Main(string[] args)
    {
        var webScraper = new WebScraper();

        string linkSite = "https://www.mercadolivre.com.br";

        Console.WriteLine("Pressione qualquer tecla para sair...");
        while (!Console.KeyAvailable)
        {
            await webScraper.ExecuteJob(linkSite);
            await Task.Delay(1000); // Aguarda 1 segundo antes de iniciar o próximo trabalho
        }   
    }
}