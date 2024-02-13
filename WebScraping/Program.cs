using WebScraping.Driver;

public class Program
{
    public static async Task Main(string[] args)
    {
        var webScraper = new WebScraper();

        Console.WriteLine("Pressione qualquer tecla para sair...");
        while (!Console.KeyAvailable)
        {
            await webScraper.ExecuteJob();
            await Task.Delay(1000); // Aguarda 1 segundo antes de iniciar o próximo trabalho
        }   
    }
}