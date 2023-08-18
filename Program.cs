
using JSON_To_PDF.Repository.Interfaces;
using JSON_To_PDF.Repository.Services;
using RazorLight;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register custom services.
builder.Services.AddScoped<IHtmlToPdfRepository, HtmlToPdfRepository>();

builder.Services.AddRazorPages();
builder.Services.AddScoped<IRazorLightEngine>(provider =>
{
    return new RazorLightEngineBuilder()
        .UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory())) // Adjust the path to your Views folder
        .UseMemoryCachingProvider()
        .Build();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
