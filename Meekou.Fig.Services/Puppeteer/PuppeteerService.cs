using PuppeteerSharp.Media;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meekou.Fig.Services.Puppeteer
{
    public class PuppeteerService: IPuppeteerService
    {
        public async Task<byte[]> HtmlToPdf(string htmlContent)
        {
            await new BrowserFetcher().DownloadAsync();

            // Launch the browser
            var browser = await PuppeteerSharp.Puppeteer.LaunchAsync(new LaunchOptions
            {
                Args = new string[] { "--no-sandbox" },
                Headless = true  // Set to true for headless mode (background)
            });

            // Open a new page
            var page = await browser.NewPageAsync();

            // Set the page content to the HTML file content
            await page.SetContentAsync(htmlContent);

            // Generate a PDF from the content
            var pdfBytes = await page.PdfDataAsync(new PdfOptions
            {
                Format = PaperFormat.A4,  // Specify the format, e.g., A4
                PrintBackground = true    // Set to true to print background colors/images
            });

            // Close the browser
            await browser.CloseAsync();

            // Return the PDF as a file download
            return pdfBytes;
        }
    }
}
