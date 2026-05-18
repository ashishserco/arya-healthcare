# FHIR R4 → OMOP CDM v5.4 ETL Mapping

This document describes how FullHealth maps inbound FHIR R4 resources into the OMOP Common Data Model for downstream analytics. The mapping aligns with the [OHDSI FHIR-OMOP-on-FHIR working group](https://www.ohdsi.org/) guidance.

## High-level data flow

```
┌─────────────────┐     ┌──────────────┐     ┌──────────────┐     ┌──────────────┐
│  HL7 v2 / FHIR  │ ──► │  Raw Bronze  │ ──► │  Curated     │ ──► │  OMOP CDM    │
│  ingestion      │     │  (JSON/JSONB)│     │  Silver      │     │  Gold        │
└─────────────────┘     └──────────────┘     └──────────────┘     └──────────────┘
```

- **Bronze:** raw FHIR resources persisted as JSONB in PostgreSQL with metadata (received_at, source_system, message_id).
- **Silver:** flattened, validated, deduplicated, with terminology codes mapped to OMOP `concept_id` via `concept` and `concept_relationship`.
- **Gold:** OMOP CDM tables ready for ATLAS, HADES, Superset, or any OHDSI-compliant tool.

## Resource-by-resource mapping

### Patient → PERSON

| FHIR Patient field                  | OMOP person column                       | Notes |
|-------------------------------------|------------------------------------------|-------|
| `id`                                | `person_source_value`                    | Source MRN; `person_id` is generated |
| `gender`                            | `gender_concept_id`                      | male → 8507, female → 8532 |
| `birthDate`                         | `year_of_birth`, `month_of_birth`, `day_of_birth`, `birth_datetime` | |
| `extension[us-core-race]`           | `race_concept_id`                        | Lookup in `concept` |
| `extension[us-core-ethnicity]`      | `ethnicity_concept_id`                   | Lookup in `concept` |
| `address`                           | `location_id`                            | FK to `location` table |
| `generalPractitioner`               | `provider_id`                            | FK to `provider` table |

### Encounter → VISIT_OCCURRENCE

| FHIR Encounter field          | OMOP visit_occurrence column                  | Notes |
|-------------------------------|-----------------------------------------------|-------|
| `id`                          | `visit_source_value`                          | |
| `class.code`                  | `visit_concept_id`                            | IMP → 9201 Inpatient, AMB → 9202 Outpatient, EMER → 9203 ED |
| `subject.reference`           | `person_id`                                   | Resolved via `person_source_value` |
| `period.start`                | `visit_start_date`, `visit_start_datetime`    | |
| `period.end`                  | `visit_end_date`, `visit_end_datetime`        | |
| `hospitalization.admitSource` | `admitted_from_concept_id`                    | |
| `hospitalization.dischargeDisposition` | `discharged_to_concept_id`           | |

### Condition → CONDITION_OCCURRENCE

| FHIR Condition field      | OMOP condition_occurrence column          | Notes |
|---------------------------|-------------------------------------------|-------|
| `code.coding[snomed]`     | `condition_source_concept_id`             | If SNOMED present, prefer as source |
| `code.coding[icd-10]`     | `condition_source_value`                  | |
| (mapped)                  | `condition_concept_id`                    | Resolve via `concept_relationship` `Maps to` → standard SNOMED |
| `subject.reference`       | `person_id`                               | |
| `encounter.reference`     | `visit_occurrence_id`                     | |
| `onsetDateTime`           | `condition_start_date`                    | |
| `abatementDateTime`       | `condition_end_date`                      | |
| `clinicalStatus.coding`   | `condition_status_concept_id`             | |

### Observation → MEASUREMENT (numeric) or OBSERVATION (non-numeric)

Routing rule: if `valueQuantity` is present and `code.coding.system = http://loinc.org`, route to **MEASUREMENT**. Else route to **OBSERVATION**.

| FHIR Observation field         | OMOP measurement column                | Notes |
|--------------------------------|----------------------------------------|-------|
| `code.coding[loinc].code`      | `measurement_source_value`             | |
| (mapped)                       | `measurement_concept_id`               | LOINC → OMOP standard concept |
| `subject.reference`            | `person_id`                            | |
| `encounter.reference`          | `visit_occurrence_id`                  | |
| `effectiveDateTime`            | `measurement_date`, `measurement_datetime` | |
| `valueQuantity.value`          | `value_as_number`                      | |
| `valueQuantity.unit`           | `unit_source_value`                    | |
| (mapped)                       | `unit_concept_id`                      | UCUM → OMOP unit concept |
| `referenceRange.low`           | `range_low`                            | |
| `referenceRange.high`          | `range_high`                           | |

### MedicationRequest / MedicationAdministration → DRUG_EXPOSURE

| FHIR field                          | OMOP drug_exposure column         | Notes |
|-------------------------------------|------------------------------------|-------|
| `medicationCodeableConcept[rxnorm]` | `drug_source_value`               | |
| (mapped)                            | `drug_concept_id`                  | RxNorm → OMOP standard |
| `subject.reference`                 | `person_id`                        | |
| `encounter.reference`               | `visit_occurrence_id`              | |
| `authoredOn`                        | `drug_exposure_start_date`         | |
| `dosageInstruction.timing`          | `days_supply` (derived)            | |
| `dosageInstruction.doseAndRate`     | `quantity`                         | |
| `dosageInstruction.route`           | `route_concept_id`                 | |
| `dosageInstruction.text`            | `sig`                              | |

### Procedure → PROCEDURE_OCCURRENCE

| FHIR Procedure field         | OMOP procedure_occurrence column | Notes |
|------------------------------|------------------------------------|-------|
| `code.coding[snomed/cpt]`    | `procedure_source_concept_id` / `procedure_source_value` | |
| (mapped)                     | `procedure_concept_id`             | Standard SNOMED |
| `subject.reference`          | `person_id`                        | |
| `encounter.reference`        | `visit_occurrence_id`              | |
| `performedDateTime`          | `procedure_date`                   | |

## Code-system → vocabulary_id mapping

| FHIR `system` URI                              | OMOP `vocabulary_id` |
|-----------------------------------------------|----------------------|
| `http://snomed.info/sct`                       | `SNOMED`             |
| `http://loinc.org`                             | `LOINC`              |
| `http://hl7.org/fhir/sid/icd-10`               | `ICD10`              |
| `http://hl7.org/fhir/sid/icd-10-cm`            | `ICD10CM`            |
| `http://www.nlm.nih.gov/research/umls/rxnorm`  | `RxNorm`             |
| `http://www.ama-assn.org/go/cpt`               | `CPT4`               |
| `http://unitsofmeasure.org`                    | `UCUM`               |

## Concept resolution algorithm

For any source-coded value (e.g. ICD-10 `I10`):

1. Look up `concept_id` where `vocabulary_id = 'ICD10CM'` and `concept_code = 'I10'`.
2. If `standard_concept = 'S'`, it is already standard - use it.
3. Otherwise, find the standard mapping:
   ```sql
   SELECT c2.concept_id
   FROM concept_relationship cr
   JOIN concept c2 ON c2.concept_id = cr.concept_id_2
   WHERE cr.concept_id_1 = :source_concept_id
     AND cr.relationship_id = 'Maps to'
     AND c2.standard_concept = 'S';
   ```
4. Persist BOTH:
   - `condition_source_concept_id` = the source concept (ICD-10 `I10`)
   - `condition_concept_id` = the standard concept (SNOMED `Essential hypertension`)

This preserves provenance while enabling standard-vocabulary analytics.

## Data quality

After each ETL batch we run [ACHILLES](https://github.com/OHDSI/Achilles) data-quality checks:

- Person record count matches source
- No future-dated events
- All `*_concept_id` columns resolve in `concept`
- Death date never precedes any event for that person
- Standard concept usage rate > 95%

## Open work

- [ ] Add COST table mapping from FHIR `Claim` / `Account`
- [ ] Wire MedicationAdministration (not just Request) into drug_exposure
- [ ] Add `note` table for clinical free-text with NLP-derived concepts
- [ ] Add CONDITION_ERA derivation job (eras of continuous condition exposure)
