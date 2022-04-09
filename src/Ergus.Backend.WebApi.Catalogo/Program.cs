using Ergus.Backend.WebApi.Catalogo.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerConfigServices();

builder.Services.AddGeneralConfigServices(builder.Configuration);

builder.Services.AddDependencyInjectionConfigServices(builder.Configuration);

builder.Services.AddAuthorizationConfigServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerConfigure();

app.UseGeneralConfigure();

app.UseAuthorizationConfigure();

app.UseValidatorConfigigure();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();