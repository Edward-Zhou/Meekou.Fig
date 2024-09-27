using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Readers;
using SharpYaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Meekou.Fig.Services
{
    public class ConvertService: IConvertService
    {
        public async Task<byte[]> Swagger(string swaggerUrl, OpenApiSpecVersion version)
        {
            var httpClient = new HttpClient();

            var stream = await httpClient.GetStreamAsync(swaggerUrl);

            // Read V3 as YAML
            var openApiDocument = new OpenApiStreamReader().Read(stream, out var diagnostic);

            // Write V2 as JSON
            var outputString = openApiDocument.Serialize(OpenApiSpecVersion.OpenApi2_0, OpenApiFormat.Json);

            return Encoding.UTF8.GetBytes(outputString);
        }
    }
}
