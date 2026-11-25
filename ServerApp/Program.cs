using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Copilot suggestion:
// Added CORS support to allow the Blazor client
// to access the API from a different origin.
builder.Services.AddCors();

builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

var cache = app.Services.GetRequiredService<IMemoryCache>();

app.MapGet("/api/productlist", () =>
{
    // Copilot-inspired optimization:
    // Cache product data to reduce unnecessary
    // object creation and database/API calls.

    return cache.GetOrCreate("products", entry =>
    {
        entry.AbsoluteExpirationRelativeToNow =
            TimeSpan.FromMinutes(5);

        return new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 1200.50m,
                Stock = 25,
                Category = new Category
                {
                    Id = 101,
                    Name = "Electronics"
                }
            },
            new Product
            {
                Id = 2,
                Name = "Headphones",
                Price = 50.00m,
                Stock = 100,
                Category = new Category
                {
                    Id = 102,
                    Name = "Accessories"
                }
            }
        };
    });
});

app.Run();