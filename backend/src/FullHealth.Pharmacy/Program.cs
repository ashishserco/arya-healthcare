using FullHealth.Pharmacy.Data;
using FullHealth.Pharmacy.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB Context
builder.Services.AddDbContext<PharmacyDbContext>(options =>
    options.UseInMemoryDatabase("PharmacyDb"));

var app = builder.Build();

// Seed Data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PharmacyDbContext>();
    context.Database.EnsureCreated();
    
    if (!context.Products.Any())
    {
        context.Products.AddRange(
            new Product { Name = "Paracetamol 500mg", Category = "Medicines", Price = 20, DiscountPrice = 18, ImageUrl = "https://via.placeholder.com/150", Description = "Fever and pain relief." },
            new Product { Name = "Vitamin C", Category = "Supplements", Price = 300, DiscountPrice = 250, ImageUrl = "https://via.placeholder.com/150", Description = "Immunity booster." },
            new Product { Name = "Digital Thermometer", Category = "Devices", Price = 500, DiscountPrice = 400, ImageUrl = "https://via.placeholder.com/150", Description = "Accurate temp reading." },
            new Product { Name = "Baby Shampoo", Category = "Baby Care", Price = 250, DiscountPrice = 220, ImageUrl = "https://via.placeholder.com/150", Description = "Gentle care for babies." }
        );
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
