/*  _____ _______         _                      _
 * |_   _|__   __|       | |                    | |
 *   | |    | |_ __   ___| |___      _____  _ __| | __  ___ ____
 *   | |    | | '_ \ / _ \ __\ \ /\ / / _ \| '__| |/ / / __|_  /
 *  _| |_   | | | | |  __/ |_ \ V  V / (_) | |  |   < | (__ / /
 * |_____|  |_|_| |_|\___|\__| \_/\_/ \___/|_|  |_|\_(_)___/___|
 *
 *                      ___ ___ ___
 *                     | . |  _| . |  LICENCE
 *                     |  _|_| |___|
 *                     |_|
 *
 *    REKVALIFIKAČNÍ KURZY  <>  PROGRAMOVÁNÍ  <>  IT KARIÉRA
 *
 * Tento zdrojový kód je součástí profesionálních IT kurzů na
 * WWW.ITNETWORK.CZ
 *
 * Kód spadá pod licenci PRO obsahu a vznikl díky podpoře
 * našich členů. Je určen pouze pro osobní užití a nesmí být šířen.
 * Více informací na http://www.itnetwork.cz/licence
 */

using Invoices.Api;
using Invoices.Api.Interfaces;
using Invoices.Api.Managers;
using Invoices.Data;
using Invoices.Data.Interfaces;
using Invoices.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the connection string from the app's configuration
var connectionString = builder.Configuration.GetConnectionString("LocalInvoicesConnection");

// Register the DbContext with SQL Server connection and enable Lazy Loading proxies
builder.Services.AddDbContext<InvoicesDbContext>(options =>
    options.UseSqlServer(connectionString)    // Configures SQL Server as the database provider
        .UseLazyLoadingProxies()              // Enables lazy loading of related entities
        .ConfigureWarnings(x => x.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning))); // Suppress warning for lazy loading with disposed context

// Add controllers and configure JSON serialization options
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));  // Converts enum to string during JSON serialization

// Add Swagger API documentation and UI for development
builder.Services.AddEndpointsApiExplorer(); // Adds support for API explorer
builder.Services.AddSwaggerGen(options =>
    options.SwaggerDoc("invoices", new OpenApiInfo
    {
        Version = "v1",        // API version
        Title = "Invoices"     // API title
    }));

// Register repositories and managers with dependency injection
builder.Services.AddScoped<IPersonRepository, PersonRepository>();  // Registers the PersonRepository
builder.Services.AddScoped<IPersonManager, PersonManager>();       // Registers the PersonManager
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>(); // Registers the InvoiceRepository
builder.Services.AddScoped<IInvoiceManager, InvoiceManager>();    // Registers the InvoiceManager

// Add AutoMapper for object-to-object mapping
builder.Services.AddAutoMapper(typeof(AutomapperConfigurationProfile)); // Configures AutoMapper

var app = builder.Build();

// Configure middleware for development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger UI
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("invoices/swagger.json", "Invoices - v1"); // Swagger UI endpoint
    });
}

// Map a basic route for testing
app.MapGet("/", () => "Hello World!"); // Simple route that returns "Hello World!"

// Map controllers to handle API requests
app.MapControllers(); // Maps API routes to controllers

// Run the application
app.Run(); // Starts the web application
