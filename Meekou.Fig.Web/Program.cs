
using Meekou.Fig.Core.Filters;
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
            #region Meekou
            builder.Services.AddScoped<IMathService, MathService>();
            builder.Services.AddScoped<IPuppeteerService, PuppeteerService>();
            builder.Services.AddScoped<ITextService, TextService>();
            builder.Services.AddScoped<IConvertService, ConvertService>();
            #endregion
            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ResponseWrapperFilter>();
                options.Filters.Add<ResponseExceptionFilter>();
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(MeekouConsts.ApiVersion, new OpenApiInfo
                {
                    Version = MeekouConsts.ApiVersion,
                    Title = "Meekou Share Connector",
                    Description = "Provide common functions which help to improve PowerApps & Automate development process.",
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
                options.OperationFilter<ResponseSwaggerOperationFilter>();
            });
            //builder.Services.Configure<SwaggerOptions>(c => c.SerializeAsV2 = true);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}
