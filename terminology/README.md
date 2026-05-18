# Clinical Terminology Reference

This folder documents the clinical vocabularies the FullHealth platform consumes and the rules we apply when ingesting, persisting, and querying coded clinical data.

| Vocabulary | Purpose | Authority | OMOP `vocabulary_id` |
|------------|---------|-----------|----------------------|
| **SNOMED CT** | Clinical findings, diagnoses, procedures | [SNOMED International](https://www.snomed.org/) | `SNOMED` |
| **LOINC** | Laboratory tests, vital signs, observations | [Regenstrief Institute](https://loinc.org/) | `LOINC` |
| **ICD-10 / ICD-10-CM** | Diagnoses (billing, epidemiology) | WHO / CDC NCHS | `ICD10` / `ICD10CM` |
| **RxNorm** | Medications, ingredients, dose forms | US NLM | `RxNorm` |
| **CPT-4** | Procedures (US billing) | American Medical Association | `CPT4` |
| **HCPCS** | Procedures, supplies, DME (US) | CMS | `HCPCS` |
| **UCUM** | Units of measure | Regenstrief | `UCUM` |

## When each vocabulary is used

### SNOMED CT — clinical findings (the spine)

- **What:** the most comprehensive clinical terminology, 350,000+ concepts, hierarchical with IS-A relationships.
- **Used in:** FHIR `Condition.code`, `Procedure.code`, `Observation.code` (some); OMOP `condition_concept_id`, `procedure_concept_id`.
- **Example:** `38341003 | Hypertensive disorder, systemic arterial`
- **Practical rule:** SNOMED is the OMOP standard for `Condition` and `Procedure` domains. ICD-10 source codes are mapped to SNOMED via `concept_relationship`.

### LOINC — labs and observations

- **What:** 95,000+ codes uniquely identifying lab tests and clinical observations.
- **Used in:** FHIR `Observation.code` (vital signs, lab results, panels); OMOP `measurement_concept_id`.
- **Example:** `4548-4 | Hemoglobin A1c/Hemoglobin.total in Blood`
- **Practical rule:** every numeric clinical fact in OMOP `measurement` should resolve to a LOINC concept.

### ICD-10 / ICD-10-CM — diagnoses (mostly billing)

- **What:** WHO classification used worldwide for mortality and morbidity. ICD-10-CM is the US clinical modification.
- **Used in:** HL7 v2 `DG1` segments; FHIR `Condition.code.coding[icd-10]`; insurance billing.
- **Example:** `I10 | Essential (primary) hypertension`
- **Practical rule:** ICD-10 is a *source* code in OMOP; it is mapped to standard SNOMED at ETL time.

### RxNorm — medications

- **What:** US normalized vocabulary for medications, ingredients, dose forms, strengths.
- **Used in:** FHIR `MedicationRequest.medicationCodeableConcept`; OMOP `drug_concept_id`.
- **Example:** `197361 | Amlodipine 5 MG Oral Tablet`
- **Practical rule:** RxNorm is the OMOP standard for `Drug` domain. NDC source codes map to RxNorm.

## How we resolve codes at ingest

```
Source code (e.g. ICD-10 "I10")
        │
        ▼
Look up in concept WHERE vocabulary_id = 'ICD10CM' AND concept_code = 'I10'
        │
        ▼
If standard_concept = 'S' → use it
Else find concept_relationship 'Maps to' → standard concept (SNOMED)
        │
        ▼
Persist BOTH:
  - source_concept_id = ICD-10 concept (44824068)
  - concept_id        = standard SNOMED concept (320128)
```

## File index

- `snomed_examples.md` — common SNOMED CT codes by domain
- `loinc_examples.md` — common LOINC codes by panel
- `icd10_examples.md` — common ICD-10-CM codes
- `rxnorm_examples.md` — common medication RxNorm codes
- `code_systems.json` — code system URIs used in FHIR resources

## Production vocabulary download

For real deployments, download the full standardized vocabulary bundle from [OHDSI Athena](https://athena.ohdsi.org/) and load via the OMOP `concept`, `concept_relationship`, and `concept_ancestor` tables. The samples here are minimal — sized for local development and demo purposes only.

## Licensing

- **SNOMED CT:** member-country license required for commercial use; free in member territories.
- **LOINC:** free worldwide under the LOINC License.
- **ICD-10:** free for use.
- **ICD-10-CM:** US public domain.
- **RxNorm:** free for use.
- **CPT-4:** licensed from the AMA; commercial use requires fee.

This project consumes terminologies in a manner compatible with each licence; production deployments must obtain appropriate licences for any in-scope vocabularies.
