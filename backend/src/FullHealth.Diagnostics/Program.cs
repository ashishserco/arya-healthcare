using FullHealth.Diagnostics.Data;
using FullHealth.Diagnostics.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB Context
builder.Services.AddDbContext<DiagnosticsDbContext>(options =>
    options.UseInMemoryDatabase("DiagnosticsDb"));

var app = builder.Build();

// Seed Data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DiagnosticsDbContext>();
    context.Database.EnsureCreated();
    
    if (!context.LabPackages.Any())
    {
        context.LabPackages.AddRange(
            new LabPackage { Name = "Complete Health Checkup", Category = "Popular", Price = 2500, DiscountPrice = 1999, TestsIncluded = 85, Description = "Comprehensive health screening" },
            new LabPackage { Name = "Diabetes Screening", Category = "Condition Based", Price = 800, DiscountPrice = 599, TestsIncluded = 12, Description = "HbA1c, Fasting Sugar, PP Sugar" },
            new LabPackage { Name = "Women Wellness", Category = "Women Health", Price = 3500, DiscountPrice = 2799, TestsIncluded = 65, Description = "Hormones, Thyroid, Vitamin D" },
            new LabPackage { Name = "Heart Health", Category = "Condition Based", Price = 1500, DiscountPrice = 1199, TestsIncluded = 25, Description = "Lipid Profile, ECG, Echo" }
        );
        
        context.LabTests.AddRange(
            new LabTest { Name = "Complete Blood Count (CBC)", Price = 300, Description = "Hemoglobin, WBC, Platelets", FastingRequired = false },
            new LabTest { Name = "Lipid Profile", Price = 500, Description = "Cholesterol, Triglycerides, HDL, LDL", FastingRequired = true },
            new LabTest { Name = "Thyroid Profile", Price = 600, Description = "T3, T4, TSH", FastingRequired = false },
            new LabTest { Name = "Vitamin D", Price = 800, Description = "25-OH Vitamin D", FastingRequired = false }
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
