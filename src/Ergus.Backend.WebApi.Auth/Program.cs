using Ergus.Backend.WebApi.Auth.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerConfigServices();

builder.Services.AddGeneralConfigServices(builder.Configuration);

builder.Services.AddDependencyInjectionConfigServices(builder.Configuration);

builder.Services.AddAuthorizationConfigServices(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfigure();
}

app.UseGeneralConfigure();

app.UseAuthorizationConfigure();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
