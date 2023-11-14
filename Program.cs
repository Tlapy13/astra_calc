using astra_calc.Services.Interfaces;
using astra_calc.Services;
using Serilog;
using Database;
using Database.Interfaces;
using Calculator_Logic.Interfaces;
using Calculator_Logic;

var builder = WebApplication.CreateBuilder(args);

// Serilog konfigurace
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DI

//In case of DB should be used instead of cache:
//builder.Services.AddScoped<ICalculationDataAccess, CalculationDataAccess>(serviceProvider =>
//    new CalculationDataAccess(builder.Configuration.GetConnectionString("CalculationDb")));
//builder.Services.AddScoped<ICalculationHistoryService, CalculationHistoryService>();
builder.Services.AddScoped<ICalculationHistoryService, CalculationCacheService>();
builder.Services.AddScoped<ICalculate, Calculate>();
builder.Services.AddScoped<ICalculatorService, CalculatorService>();
builder.Services.AddSingleton<IErrorHandlingService, ErrorHandlingService>();

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Calculator}/{action=Index}/{id?}");

app.Run();
