using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace FullHealth.Interop.Fhir.Controllers;

/// <summary>
/// FHIR R4 Patient resource endpoints.
/// Implements core RESTful operations per https://hl7.org/fhir/R4/patient.html
/// </summary>
[ApiController]
[Route("fhir/Patient")]
[Produces("application/fhir+json")]
public class PatientController : ControllerBase
{
    private readonly FhirJsonSerializer _serializer;

    public PatientController(FhirJsonSerializer serializer)
    {
        _serializer = serializer;
    }

    [HttpGet("{id}")]
    public IActionResult Read(string id)
    {
        var patient = FhirSampleData.SamplePatient(id);
        return Content(_serializer.SerializeToString(patient), "application/fhir+json");
    }

    [HttpGet]
    public IActionResult Search([FromQuery] string? identifier, [FromQuery] string? family)
    {
        var bundle = new Bundle
        {
            Type = Bundle.BundleType.Searchset,
            Timestamp = DateTimeOffset.UtcNow,
            Entry = new List<Bundle.EntryComponent>
            {
                new()
                {
                    FullUrl = $"Patient/MRN12345",
                    Resource = FhirSampleData.SamplePatient()
                }
            }
        };
        bundle.Total = bundle.Entry.Count;
        return Content(_serializer.SerializeToString(bundle), "application/fhir+json");
    }

    /// <summary>
    /// $everything operation per FHIR R4 - returns all resources related to a patient.
    /// </summary>
    [HttpGet("{id}/$everything")]
    public IActionResult Everything(string id)
    {
        var bundle = new Bundle
        {
            Type = Bundle.BundleType.Searchset,
            Timestamp = DateTimeOffset.UtcNow,
            Entry = new List<Bundle.EntryComponent>
            {
                new() { Resource = FhirSampleData.SamplePatient(id) },
                new() { Resource = FhirSampleData.SampleInpatientEncounter(id) },
                new() { Resource = FhirSampleData.SampleHypertension(id) },
                new() { Resource = FhirSampleData.SampleBloodPressure(id) },
                new() { Resource = FhirSampleData.SampleAmlodipine(id) }
            }
        };
        bundle.Total = bundle.Entry.Count;
        return Content(_serializer.SerializeToString(bundle), "application/fhir+json");
    }
}
