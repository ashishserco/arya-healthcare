using Hl7.Fhir.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace FullHealth.Interop.Fhir.Controllers;

/// <summary>
/// FHIR R4 MedicationRequest resource - prescription orders.
/// Medication coded via RxNorm (US) or SNOMED CT depending on locale.
/// </summary>
[ApiController]
[Route("fhir/MedicationRequest")]
[Produces("application/fhir+json")]
public class MedicationRequestController : ControllerBase
{
    private readonly FhirJsonSerializer _serializer;

    public MedicationRequestController(FhirJsonSerializer serializer)
    {
        _serializer = serializer;
    }

    [HttpGet("{id}")]
    public IActionResult Read(string id)
    {
        var mr = FhirSampleData.SampleAmlodipine();
        mr.Id = id;
        return Content(_serializer.SerializeToString(mr), "application/fhir+json");
    }

    [HttpGet]
    public IActionResult Search([FromQuery] string? patient, [FromQuery] string? status)
    {
        var mr = FhirSampleData.SampleAmlodipine(patient ?? "MRN12345");
        return Content(_serializer.SerializeToString(mr), "application/fhir+json");
    }
}
