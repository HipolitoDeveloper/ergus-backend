﻿using Ergus.Backend.Core.Settings;
using Ergus.Backend.WebApi.Auth.Middlewares;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using System.Globalization;
using System.IO.Compression;

namespace Ergus.Backend.WebApi.Auth.Setup
{
    public static class GeneralConfig
    {
        public static void AddGeneralConfigServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddCors();
            services.AddControllers();

            services.Configure<AppSettings>(config.GetSection("AppSettings"));

            // Habilita a compressão do response
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
        }

        public static void UseGeneralConfigure(this IApplicationBuilder app)
        {
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // Definindo a cultura padrão: pt-BR
            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            //Middleware customizado para interceptar erros HTTP e exceptions não tratadas
            app.UseCustomExceptionHandler();

            app.UseHttpsRedirection();

            // Utiliza a compressão do response
            app.UseResponseCompression();

            app.UseRouting();
        }
    }
}
