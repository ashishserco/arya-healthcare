using NHapi.Base.Parser;
using NHapi.Model.V25.Message;

namespace FullHealth.Interop.Hl7v2;

/// <summary>
/// Wraps NHapi pipe parser for HL7 v2.5 messages.
/// Supports ADT (admission/discharge/transfer), ORM (orders), ORU (results).
/// </summary>
public class Hl7MessageParser
{
    private readonly PipeParser _parser = new();

    public AdtSummary ParseAdtA01(string pipeMessage)
    {
        var msg = _parser.Parse(pipeMessage) as ADT_A01
            ?? throw new InvalidOperationException("Not an ADT^A01 message");

        return new AdtSummary
        {
            MessageControlId = msg.MSH.MessageControlID.Value,
            SendingFacility = msg.MSH.SendingFacility.NamespaceID.Value,
            EventType = msg.EVN.EventTypeCode.Value,
            EventDateTime = msg.EVN.RecordedDateTime.Time.Value,
            PatientMrn = msg.PID.GetPatientIdentifierList(0).IDNumber.Value,
            PatientFamilyName = msg.PID.GetPatientName(0).FamilyName.Surname.Value,
            PatientGivenName = msg.PID.GetPatientName(0).GivenName.Value,
            PatientGender = msg.PID.AdministrativeSex.Value,
            PatientDob = msg.PID.DateTimeOfBirth.Time.Value,
            VisitClass = msg.PV1.PatientClass.Value,
            AssignedLocation = msg.PV1.AssignedPatientLocation.PointOfCare.Value,
            VisitNumber = msg.PV1.VisitNumber.IDNumber.Value
        };
    }

    public OruSummary ParseOruR01(string pipeMessage)
    {
        var msg = _parser.Parse(pipeMessage) as ORU_R01
            ?? throw new InvalidOperationException("Not an ORU^R01 message");

        var resultSummaries = new List<OruResult>();
        var patientResultCount = msg.PATIENT_RESULTRepetitionsUsed;

        for (var p = 0; p < patientResultCount; p++)
        {
            var pr = msg.GetPATIENT_RESULT(p);
            var orderCount = pr.ORDER_OBSERVATIONRepetitionsUsed;

            for (var o = 0; o < orderCount; o++)
            {
                var order = pr.GetORDER_OBSERVATION(o);
                var obsCount = order.OBSERVATIONRepetitionsUsed;

                for (var i = 0; i < obsCount; i++)
                {
                    var obx = order.GetOBSERVATION(i).OBX;
                    resultSummaries.Add(new OruResult
                    {
                        ObservationIdentifier = obx.ObservationIdentifier.Identifier.Value,
                        ObservationText = obx.ObservationIdentifier.Text.Value,
                        CodingSystem = obx.ObservationIdentifier.NameOfCodingSystem.Value,
                        Value = obx.GetObservationValue(0)?.Data.ToString(),
                        Units = obx.Units.Identifier.Value,
                        Status = obx.ObservationResultStatus.Value
                    });
                }
            }
        }

        return new OruSummary
        {
            MessageControlId = msg.MSH.MessageControlID.Value,
            PatientMrn = msg.GetPATIENT_RESULT(0).PATIENT.PID.GetPatientIdentifierList(0).IDNumber.Value,
            Results = resultSummaries
        };
    }

    /// <summary>
    /// Build standard HL7 v2 ACK in response to a received message.
    /// </summary>
    public string BuildAck(string originalMessageControlId, string sendingApp = "FULLHEALTH", string receivingApp = "EHR")
    {
        var now = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        return $"MSH|^~\\&|{sendingApp}|FULLHEALTH|{receivingApp}|EHR|{now}||ACK|{Guid.NewGuid():N}|P|2.5\r" +
               $"MSA|AA|{originalMessageControlId}|Message accepted";
    }
}

public class AdtSummary
{
    public string? MessageControlId { get; set; }
    public string? SendingFacility { get; set; }
    public string? EventType { get; set; }
    public string? EventDateTime { get; set; }
    public string? PatientMrn { get; set; }
    public string? PatientFamilyName { get; set; }
    public string? PatientGivenName { get; set; }
    public string? PatientGender { get; set; }
    public string? PatientDob { get; set; }
    public string? VisitClass { get; set; }
    public string? AssignedLocation { get; set; }
    public string? VisitNumber { get; set; }
}

public class OruSummary
{
    public string? MessageControlId { get; set; }
    public string? PatientMrn { get; set; }
    public List<OruResult> Results { get; set; } = new();
}

public class OruResult
{
    public string? ObservationIdentifier { get; set; }
    public string? ObservationText { get; set; }
    public string? CodingSystem { get; set; }
    public string? Value { get; set; }
    public string? Units { get; set; }
    public string? Status { get; set; }
}
