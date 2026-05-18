using Hl7.Fhir.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "FullHealth Interop API (FHIR R4 + HL7 v2)", Version = "v1" });
});

builder.Services.AddSingleton<FhirJsonSerializer>();
builder.Services.AddSingleton<FhirJsonParser>();
builder.Services.AddSingleton<FullHealth.Interop.Hl7v2.Hl7MessageParser>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.MapGet("/", () => "FullHealth Interop API - FHIR R4 endpoints under /fhir, HL7 v2 ingest under /hl7v2");

app.Run();
