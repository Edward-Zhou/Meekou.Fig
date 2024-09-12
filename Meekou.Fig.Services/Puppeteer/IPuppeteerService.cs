
namespace Meekou.Fig.Services.Puppeteer
{
    public interface IPuppeteerService
    {
        Task<byte[]> HtmlToPdf(string htmlContent);
    }
}