using Lego_Inventory.Data;
using Lego_Inventory.Models;
using Microsoft.AspNetCore.Builder;
using MongoDB.Driver;
using DotNetEnv;
using Microsoft.AspNetCore.DataProtection;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

// Add this line before building the app
builder.Services.AddScoped<ILegoRepository, LegoRepository>();

// Retrieve the CosmosDB connection string from the environment variables
var connectionString = Environment.GetEnvironmentVariable("COSMOSDB_CONNECTIONSTRING");


if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("❌ ERROR: CosmosDB connection string is missing!");
    throw new Exception("Missing CosmosDB connection string.");
}
else
{
    Console.WriteLine($"✅ CosmosDB Connection String: {connectionString}");
}

// Register the MongoDB client with the connection string
builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(connectionString));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
// Set the default route for the application
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
