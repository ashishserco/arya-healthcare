using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace FullHealth.Interop.Fhir.Controllers;

/// <summary>
/// FHIR R4 Observation resource - lab results, vital signs, clinical observations.
/// Codes use LOINC system per https://hl7.org/fhir/R4/observation.html
/// </summary>
[ApiController]
[Route("fhir/Observation")]
[Produces("application/fhir+json")]
public class ObservationController : ControllerBase
{
    private readonly FhirJsonSerializer _serializer;

    public ObservationController(FhirJsonSerializer serializer)
    {
        _serializer = serializer;
    }

    [HttpGet("{id}")]
    public IActionResult Read(string id)
    {
        var obs = FhirSampleData.SampleBloodPressure();
        obs.Id = id;
        return Content(_serializer.SerializeToString(obs), "application/fhir+json");
    }

    /// <summary>
    /// Search by patient and LOINC code.
    /// Example: GET /fhir/Observation?patient=MRN12345&amp;code=http://loinc.org|85354-9
    /// </summary>
    [HttpGet]
    public IActionResult Search([FromQuery] string? patient, [FromQuery] string? code, [FromQuery] string? date)
    {
        var bundle = new Bundle
        {
            Type = Bundle.BundleType.Searchset,
            Timestamp = DateTimeOffset.UtcNow,
            Entry = new List<Bundle.EntryComponent>
            {
                new() { Resource = FhirSampleData.SampleBloodPressure(patient ?? "MRN12345") }
            }
        };
        bundle.Total = bundle.Entry.Count;
        return Content(_serializer.SerializeToString(bundle), "application/fhir+json");
    }
}
