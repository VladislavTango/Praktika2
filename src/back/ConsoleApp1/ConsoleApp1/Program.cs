using DinkToPdf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Program
{
    private static async Task Main(string[] args)
    {

        string htmlContent = @"
        <!DOCTYPE html>
        <html>
        <head>
            <title>PDF Example</title>
        </head>
        <body>
            <h1>Привет, мир!</h1>
            <p>Этот PDF был сгенерирован из HTML с помощью C# и DinkToPdf.</p>
        </body>
        </html>";

        // Создаем конвертер
        var converter = new SynchronizedConverter(new PdfTools());

        // Настройки для конвертации
        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
            },
            Objects = {
                new ObjectSettings
                {
                    HtmlContent = htmlContent,
                    WebSettings = { DefaultEncoding = "utf-8" }
                }
            }
        };

        converter.Convert(doc);

        Console.WriteLine($"PDF успешно создан по пути: ");

    }
}
