using FluentValidation.AspNetCore;
using Ergus.Backend.Core.Settings;
using Ergus.Backend.WebApi.Catalogo.Helpers;
using Ergus.Backend.WebApi.Catalogo.Middlewares;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.ResponseCompression;
using System.Globalization;
using System.IO.Compression;

namespace Ergus.Backend.WebApi.Catalogo.Setup
{
    public static class GeneralConfig
    {
        public static void AddGeneralConfigServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddCors();
            services.AddControllers();

            services.Configure<AppSettings>(config.GetSection("AppSettings"));

            // Ajusta o tipo aceito de data pelo Postgres
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Habilita a compressão do response
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);

            services
                .AddMvc(options => { options.Filters.Add(typeof(ValidateModelStateAttribute)); })
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
                    fv.DisableDataAnnotationsValidation = true;
                });

            // Desabilita a validação padrão do dotnet
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });;
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
