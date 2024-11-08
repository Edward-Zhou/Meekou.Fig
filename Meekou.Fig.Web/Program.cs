
using Meekou.Fig.Core.Filters;
using Meekou.Fig.Core.Middlewares;
using Meekou.Fig.Models;
using Meekou.Fig.Services;
using Meekou.Fig.Services.Math;
using Meekou.Fig.Services.Puppeteer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Meekou.Fig.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var env = builder.Environment;
            #region Meekou
            builder.Services.AddScoped<IMathService, MathService>();
            builder.Services.AddScoped<IPuppeteerService, PuppeteerService>();
            builder.Services.AddScoped<ITextService, TextService>();
            builder.Services.AddScoped<IConvertService, ConvertService>();
            #endregion
            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ResponseExceptionFilter>();
            });
            builder.Services.AddHttpClient(string.Empty, _ => { })
                    .ConfigurePrimaryHttpMessageHandler(() =>
                    {
                        return new HttpClientHandler
                        {
                            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                        };
                    });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(MeekouConsts.ApiVersion, new OpenApiInfo
                {
                    Version = MeekouConsts.ApiVersion,
                    Title = "Meekou Share",
                    Description = "Provide common functions which help to improve Power Platform development process.",
                    Contact = new OpenApiContact
                    {
                        Name = "米可爱分享",
                        Email = "support@meekou365.onmicrosoft.com",
                        Url = new Uri("https://blog.meekou.cn/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://github.com/aspnetboilerplate/aspnetboilerplate/blob/dev/LICENSE"),
                    }
                });
                options.AddServer(new OpenApiServer
                {
                    Url = "https://meekou-fig.azurewebsites.net/",
                    Description = "Meekou"
                });
                options.AddServer(new OpenApiServer
                {
                    Url = "https://localhost:7295/",
                    Description = "Meekou"
                });
                // Define the BearerAuth scheme that's in use
                options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme()
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                options.CustomOperationIds(apiDesc =>
                {
                    return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
                });
                var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
                foreach (var xmlFile in xmlFiles)
                {
                    options.IncludeXmlComments(xmlFile);
                }
                options.DocumentFilter<SwaggerFilter>();
                options.OperationFilter<SwaggerFilter>();
            });
            // Uncomment out this line during debug mode to check swagger json
            //builder.Services.Configure<SwaggerOptions>(c => c.SerializeAsV2 = env.IsDevelopment());
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseMiddleware<HostMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}
