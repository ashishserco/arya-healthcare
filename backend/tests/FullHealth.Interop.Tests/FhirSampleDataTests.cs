using FluentAssertions;
using FullHealth.Interop.Fhir;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Xunit;

namespace FullHealth.Interop.Tests;

public class FhirSampleDataTests
{
    private readonly FhirJsonSerializer _serializer = new(new SerializerSettings { Pretty = true });

    [Fact]
    public void SamplePatient_Has_Mrn_Identifier()
    {
        var p = FhirSampleData.SamplePatient("MRN99999");

        p.Id.Should().Be("MRN99999");
        p.Identifier.Should().ContainSingle()
            .Which.Value.Should().Be("MRN99999");
        p.Gender.Should().Be(AdministrativeGender.Male);
    }

    [Fact]
    public void SampleBloodPressure_Uses_LOINC_Panel_Code()
    {
        var obs = FhirSampleData.SampleBloodPressure();

        obs.Code.Coding.Should().ContainSingle()
            .Which.System.Should().Be("http://loinc.org");
        obs.Code.Coding[0].Code.Should().Be("85354-9");
        obs.Component.Should().HaveCount(2);
    }

    [Fact]
    public void SampleHypertension_Dual_Coded_Snomed_And_Icd10()
    {
        var cond = FhirSampleData.SampleHypertension();

        cond.Code.Coding.Should().Contain(c => c.System == "http://snomed.info/sct" && c.Code == "38341003");
        cond.Code.Coding.Should().Contain(c => c.System == "http://hl7.org/fhir/sid/icd-10" && c.Code == "I10");
    }

    [Fact]
    public void SampleMedicationRequest_Uses_RxNorm()
    {
        var mr = FhirSampleData.SampleAmlodipine();
        var med = mr.Medication as CodeableConcept;

        med.Should().NotBeNull();
        med!.Coding.Should().ContainSingle()
            .Which.System.Should().Be("http://www.nlm.nih.gov/research/umls/rxnorm");
        med.Coding[0].Code.Should().Be("197361");
    }

    [Fact]
    public void Patient_Serializes_To_Valid_Fhir_Json()
    {
        var p = FhirSampleData.SamplePatient();
        var json = _serializer.SerializeToString(p);

        json.Should().Contain("\"resourceType\": \"Patient\"");
        json.Should().Contain("\"id\": \"MRN12345\"");
    }
}
