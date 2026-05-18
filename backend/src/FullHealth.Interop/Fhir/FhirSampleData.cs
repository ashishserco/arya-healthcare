using Hl7.Fhir.Model;

namespace FullHealth.Interop.Fhir;

/// <summary>
/// In-memory sample FHIR R4 resources used by controllers for demonstration
/// of resource modelling and search semantics. Real deployments would source
/// from a FHIR-compliant data store.
/// </summary>
public static class FhirSampleData
{
    public static Patient SamplePatient(string id = "MRN12345") => new()
    {
        Id = id,
        Identifier = new List<Identifier>
        {
            new("http://fullhealth.example.org/mrn", id)
        },
        Active = true,
        Name = new List<HumanName>
        {
            new() { Family = "Doe", Given = new[] { "John", "A" }, Use = HumanName.NameUse.Official }
        },
        Gender = AdministrativeGender.Male,
        BirthDate = "1970-01-01",
        Address = new List<Address>
        {
            new() { Line = new[] { "123 Main St" }, City = "Pune", State = "MH", PostalCode = "411001", Country = "IN" }
        },
        Telecom = new List<ContactPoint>
        {
            new(ContactPoint.ContactPointSystem.Phone, "+91-9999999999", ContactPoint.ContactPointUse.Mobile)
        }
    };

    public static Observation SampleBloodPressure(string patientId = "MRN12345") => new()
    {
        Id = "bp-001",
        Status = ObservationStatus.Final,
        Category = new List<CodeableConcept>
        {
            new("http://terminology.hl7.org/CodeSystem/observation-category", "vital-signs", "Vital Signs")
        },
        Code = new CodeableConcept("http://loinc.org", "85354-9", "Blood pressure panel"),
        Subject = new ResourceReference($"Patient/{patientId}"),
        Effective = new FhirDateTime(DateTimeOffset.UtcNow),
        Component = new List<Observation.ComponentComponent>
        {
            new()
            {
                Code = new CodeableConcept("http://loinc.org", "8480-6", "Systolic blood pressure"),
                Value = new Quantity(120, "mmHg", "http://unitsofmeasure.org")
            },
            new()
            {
                Code = new CodeableConcept("http://loinc.org", "8462-4", "Diastolic blood pressure"),
                Value = new Quantity(80, "mmHg", "http://unitsofmeasure.org")
            }
        }
    };

    public static Encounter SampleInpatientEncounter(string patientId = "MRN12345") => new()
    {
        Id = "enc-001",
        Status = Encounter.EncounterStatus.InProgress,
        Class = new Coding("http://terminology.hl7.org/CodeSystem/v3-ActCode", "IMP", "inpatient encounter"),
        Subject = new ResourceReference($"Patient/{patientId}"),
        Period = new Period { Start = DateTimeOffset.UtcNow.AddDays(-2).ToString("o") },
        ReasonCode = new List<CodeableConcept>
        {
            new("http://snomed.info/sct", "38341003", "Hypertensive disorder, systemic arterial")
        }
    };

    public static Condition SampleHypertension(string patientId = "MRN12345") => new()
    {
        Id = "cond-001",
        ClinicalStatus = new CodeableConcept("http://terminology.hl7.org/CodeSystem/condition-clinical", "active", "Active"),
        VerificationStatus = new CodeableConcept("http://terminology.hl7.org/CodeSystem/condition-ver-status", "confirmed", "Confirmed"),
        Code = new CodeableConcept
        {
            Coding = new List<Coding>
            {
                new("http://snomed.info/sct", "38341003", "Hypertensive disorder, systemic arterial"),
                new("http://hl7.org/fhir/sid/icd-10", "I10", "Essential (primary) hypertension")
            },
            Text = "Essential hypertension"
        },
        Subject = new ResourceReference($"Patient/{patientId}"),
        RecordedDate = DateTimeOffset.UtcNow.ToString("o")
    };

    public static MedicationRequest SampleAmlodipine(string patientId = "MRN12345") => new()
    {
        Id = "medreq-001",
        Status = MedicationRequest.MedicationrequestStatus.Active,
        Intent = MedicationRequest.MedicationRequestIntent.Order,
        Medication = new CodeableConcept
        {
            Coding = new List<Coding>
            {
                new("http://www.nlm.nih.gov/research/umls/rxnorm", "197361", "Amlodipine 5 MG Oral Tablet")
            },
            Text = "Amlodipine 5 mg PO daily"
        },
        Subject = new ResourceReference($"Patient/{patientId}"),
        AuthoredOn = DateTimeOffset.UtcNow.ToString("o"),
        DosageInstruction = new List<Dosage>
        {
            new()
            {
                Text = "1 tablet by mouth once daily",
                Timing = new Timing
                {
                    Repeat = new Timing.RepeatComponent
                    {
                        Frequency = 1,
                        Period = 1,
                        PeriodUnit = Timing.UnitsOfTime.D
                    }
                }
            }
        }
    };
}
