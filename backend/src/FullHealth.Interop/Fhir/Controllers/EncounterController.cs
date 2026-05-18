using Hl7.Fhir.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace FullHealth.Interop.Fhir.Controllers;

/// <summary>
/// FHIR R4 Encounter resource - inpatient stays, outpatient visits, emergency.
/// </summary>
[ApiController]
[Route("fhir/Encounter")]
[Produces("application/fhir+json")]
public class EncounterController : ControllerBase
{
    private readonly FhirJsonSerializer _serializer;

    public EncounterController(FhirJsonSerializer serializer)
    {
        _serializer = serializer;
    }

    [HttpGet("{id}")]
    public IActionResult Read(string id)
    {
        var enc = FhirSampleData.SampleInpatientEncounter();
        enc.Id = id;
        return Content(_serializer.SerializeToString(enc), "application/fhir+json");
    }

    [HttpGet]
    public IActionResult Search([FromQuery] string? patient, [FromQuery] string? status)
    {
        var enc = FhirSampleData.SampleInpatientEncounter(patient ?? "MRN12345");
        return Content(_serializer.SerializeToString(enc), "application/fhir+json");
    }
}
