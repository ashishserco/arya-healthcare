using FullHealth.Provider.Data;
using FullHealth.Provider.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB Context
builder.Services.AddDbContext<ProviderDbContext>(options =>
    options.UseInMemoryDatabase("ProviderDb"));

var app = builder.Build();

// Seed Data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ProviderDbContext>();
    context.Database.EnsureCreated();
    
    if (!context.Doctors.Any())
    {
        context.Doctors.AddRange(
            new Doctor { Name = "Dr. Sarah Smith", Specialty = "Cardiology", Experience = "15+ Years", ImageUrl = "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?auto=format&fit=crop&w=300&q=80", Qualification = "MD, DM", Bio = "Expert in Interventional Cardiology." },
            new Doctor { Name = "Dr. James Doe", Specialty = "Neurology", Experience = "20+ Years", ImageUrl = "https://images.unsplash.com/photo-1612349317150-141d211a2cd9?auto=format&fit=crop&w=300&q=80", Qualification = "MCh (Neuro)", Bio = "specializes in Stroke and Epilepsy." },
            new Doctor { Name = "Dr. Emily White", Specialty = "Pediatrics", Experience = "12+ Years", ImageUrl = "https://images.unsplash.com/photo-1594824476967-48c8b964273f?auto=format&fit=crop&w=300&q=80", Qualification = "MD (Paed codes)", Bio = "Kind and compassionate pediatrician." },
            new Doctor { Name = "Dr. Michael Brown", Specialty = "Orthopedics", Experience = "18+ Years", ImageUrl = "https://images.unsplash.com/photo-1622253692010-333f2da6031d?auto=format&fit=crop&w=300&q=80", Qualification = "MS (Ortho)", Bio = "Joint Replacement Specialist." }
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
