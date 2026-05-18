using Hl7.Fhir.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace FullHealth.Interop.Fhir.Controllers;

/// <summary>
/// FHIR R4 Condition resource - diagnoses, problems.
/// Codes dual-coded with SNOMED CT (primary) and ICD-10 (secondary) per common HealthTech pattern.
/// </summary>
[ApiController]
[Route("fhir/Condition")]
[Produces("application/fhir+json")]
public class ConditionController : ControllerBase
{
    private readonly FhirJsonSerializer _serializer;

    public ConditionController(FhirJsonSerializer serializer)
    {
        _serializer = serializer;
    }

    [HttpGet("{id}")]
    public IActionResult Read(string id)
    {
        var cond = FhirSampleData.SampleHypertension();
        cond.Id = id;
        return Content(_serializer.SerializeToString(cond), "application/fhir+json");
    }

    [HttpGet]
    public IActionResult Search([FromQuery] string? patient, [FromQuery] string? code)
    {
        var cond = FhirSampleData.SampleHypertension(patient ?? "MRN12345");
        return Content(_serializer.SerializeToString(cond), "application/fhir+json");
    }
}
