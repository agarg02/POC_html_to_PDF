
using JSON_To_PDF.Repository.Interfaces;
using JSON_To_PDF.Repository.Services;
using JSON_To_PDF.Validators.Interface;
using JSON_To_PDF.Validators.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using RazorLight;
using System.Globalization;
using Microsoft.Extensions.Localization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register custom services.
builder.Services.AddScoped<IHtmlToPdfRepository, HtmlToPdfRepository>();
builder.Services.AddScoped<IMortgageRepository, MortgageRepository>();
builder.Services.AddScoped<IMortgageValidation, MortgageValidation>();

builder.Services.AddRazorPages();
builder.Services.AddScoped<IRazorLightEngine>(provider =>
{
    return new RazorLightEngineBuilder()
        .UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory())) // Adjust the path to your Views folder
        .UseMemoryCachingProvider()
        .Build();
});

// for localization

var supportedCultures = new List<CultureInfo>
{
    new CultureInfo("en-US"),
    new CultureInfo("es-ES")
};

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc()
        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
        .AddDataAnnotationsLocalization();

// for localization


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
