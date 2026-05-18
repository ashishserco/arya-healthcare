using FluentAssertions;
using FullHealth.Interop.Hl7v2;
using Xunit;

namespace FullHealth.Interop.Tests;

public class Hl7v2ParserTests
{
    private readonly Hl7MessageParser _parser = new();

    private const string SampleAdtA01 =
        "MSH|^~\\&|EPIC|HOSP1|FULLHEALTH|FULLHEALTH|20240115103000||ADT^A01|MSG00001|P|2.5\r" +
        "EVN|A01|20240115103000\r" +
        "PID|1||MRN12345^^^HOSP^MR||DOE^JOHN^A||19700101|M\r" +
        "PV1|1|I|ICU^101^1|||||||SUR||||||1|||VIS00001";

    private const string SampleOruR01 =
        "MSH|^~\\&|LAB|HOSP1|FULLHEALTH|FULLHEALTH|20240115110500||ORU^R01|MSG00002|P|2.5\r" +
        "PID|1||MRN12345^^^HOSP^MR||DOE^JOHN^A||19700101|M\r" +
        "OBR|1|ORD001|RES001|85354-9^Blood pressure panel^LN\r" +
        "OBX|1|NM|8480-6^Systolic blood pressure^LN||120|mmHg|90-140|N|||F\r" +
        "OBX|2|NM|8462-4^Diastolic blood pressure^LN||80|mmHg|60-90|N|||F";

    [Fact]
    public void Parses_AdtA01_Extracts_Patient_Mrn()
    {
        var summary = _parser.ParseAdtA01(SampleAdtA01);

        summary.PatientMrn.Should().Be("MRN12345");
        summary.PatientFamilyName.Should().Be("DOE");
        summary.PatientGivenName.Should().Be("JOHN");
        summary.EventType.Should().Be("A01");
        summary.VisitClass.Should().Be("I");
    }

    [Fact]
    public void Parses_OruR01_Extracts_Two_Components()
    {
        var summary = _parser.ParseOruR01(SampleOruR01);

        summary.PatientMrn.Should().Be("MRN12345");
        summary.Results.Should().HaveCountGreaterThanOrEqualTo(2);
        summary.Results[0].ObservationIdentifier.Should().Be("8480-6");
        summary.Results[1].ObservationIdentifier.Should().Be("8462-4");
    }

    [Fact]
    public void Builds_Standard_Ack_With_Original_Control_Id()
    {
        var ack = _parser.BuildAck("MSG00001");

        ack.Should().Contain("MSA|AA|MSG00001");
        ack.Should().StartWith("MSH|");
    }
}
