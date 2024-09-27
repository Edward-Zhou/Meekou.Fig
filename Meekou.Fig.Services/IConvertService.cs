using Microsoft.OpenApi;

namespace Meekou.Fig.Services
{
    public interface IConvertService
    {
        Task<byte[]> Swagger(string swaggerUrl, OpenApiSpecVersion version);
    }
}