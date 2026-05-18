using Microsoft.AspNetCore.Mvc;

namespace FullHealth.Interop.Hl7v2.Controllers;

/// <summary>
/// HL7 v2 message ingest endpoint.
/// In production this would be a TCP MLLP listener; HTTP endpoint included
/// here for simple testing and demonstration.
/// </summary>
[ApiController]
[Route("hl7v2")]
public class Hl7IngestController : ControllerBase
{
    private readonly Hl7MessageParser _parser;
    private readonly ILogger<Hl7IngestController> _logger;

    public Hl7IngestController(Hl7MessageParser parser, ILogger<Hl7IngestController> logger)
    {
        _parser = parser;
        _logger = logger;
    }

    /// <summary>
    /// Accepts an ADT^A01 (Admit Patient) message and returns an ACK.
    /// </summary>
    [HttpPost("adt")]
    [Consumes("text/plain")]
    public async Task<IActionResult> IngestAdt()
    {
        using var reader = new StreamReader(Request.Body);
        var raw = await reader.ReadToEndAsync();

        try
        {
            var summary = _parser.ParseAdtA01(raw);
            _logger.LogInformation("ADT received for MRN {Mrn} event {Event}", summary.PatientMrn, summary.EventType);

            // TODO: persist to staging table; map to FHIR Patient + Encounter; emit domain event.

            var ack = _parser.BuildAck(summary.MessageControlId ?? "UNKNOWN");
            return Content(ack, "application/hl7-v2");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ADT parse failed");
            return BadRequest(new { error = "Malformed HL7 message", detail = ex.Message });
        }
    }

    /// <summary>
    /// Accepts an ORU^R01 (Observation Result) message.
    /// </summary>
    [HttpPost("oru")]
    [Consumes("text/plain")]
    public async Task<IActionResult> IngestOru()
    {
        using var reader = new StreamReader(Request.Body);
        var raw = await reader.ReadToEndAsync();

        try
        {
            var summary = _parser.ParseOruR01(raw);
            _logger.LogInformation("ORU received for MRN {Mrn} with {Count} results", summary.PatientMrn, summary.Results.Count);

            // TODO: map LOINC-coded results to FHIR Observations; persist; trigger clinical decision rules.

            var ack = _parser.BuildAck(summary.MessageControlId ?? "UNKNOWN");
            return Content(ack, "application/hl7-v2");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ORU parse failed");
            return BadRequest(new { error = "Malformed HL7 message", detail = ex.Message });
        }
    }
}
