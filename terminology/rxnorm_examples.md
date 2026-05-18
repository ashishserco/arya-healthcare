# RxNorm — Common Codes Reference

RxNorm is a normalized clinical drug nomenclature maintained by the US National Library of Medicine. It is the **standard** OMOP vocabulary for the `Drug` domain.

RxNorm concept term types:
- **IN** (Ingredient)
- **SCD** (Semantic Clinical Drug) — ingredient + strength + dose form
- **SBD** (Semantic Branded Drug) — brand + ingredient + strength + dose form
- **BN** (Brand Name)
- **GPCK / BPCK** (Generic / Branded Pack)

## Cardiovascular

| RxNorm RxCUI | Display | TTY |
|---|---|---|
| `197361` | Amlodipine 5 MG Oral Tablet | SCD |
| `1191` | Aspirin | IN |
| `1116632` | Aspirin 81 MG Oral Tablet | SCD |
| `200033` | Atorvastatin 10 MG Oral Tablet | SCD |
| `83367` | Atorvastatin | IN |
| `860975` | Metoprolol Tartrate 25 MG Oral Tablet | SCD |
| `198039` | Losartan 50 MG Oral Tablet | SCD |

## Diabetes

| RxNorm RxCUI | Display | TTY |
|---|---|---|
| `860975` | Metformin 500 MG Oral Tablet | SCD |
| `6809` | Metformin | IN |
| `5856` | Insulin Human | IN |
| `253182` | Insulin Glargine 100 UNT/ML Injectable Solution | SCD |

## Respiratory

| RxNorm RxCUI | Display | TTY |
|---|---|---|
| `435` | Albuterol | IN |
| `329498` | Albuterol 0.09 MG/ACTUAT Metered Dose Inhaler | SCD |
| `746763` | Fluticasone Propionate 0.044 MG/ACTUAT Metered Dose Inhaler | SCD |

## Pain / Anti-inflammatory

| RxNorm RxCUI | Display | TTY |
|---|---|---|
| `161` | Acetaminophen | IN |
| `198440` | Acetaminophen 500 MG Oral Tablet | SCD |
| `5640` | Ibuprofen | IN |
| `310965` | Ibuprofen 200 MG Oral Tablet | SCD |

## Anticoagulants

| RxNorm RxCUI | Display | TTY |
|---|---|---|
| `11289` | Warfarin | IN |
| `855288` | Warfarin Sodium 5 MG Oral Tablet | SCD |
| `1037045` | Apixaban 5 MG Oral Tablet | SCD |
| `1599538` | Rivaroxaban 20 MG Oral Tablet | SCD |

## How RxNorm appears in FHIR MedicationRequest

```json
{
  "resourceType": "MedicationRequest",
  "status": "active",
  "intent": "order",
  "medicationCodeableConcept": {
    "coding": [{
      "system": "http://www.nlm.nih.gov/research/umls/rxnorm",
      "code": "197361",
      "display": "Amlodipine 5 MG Oral Tablet"
    }]
  },
  "subject": { "reference": "Patient/MRN12345" },
  "dosageInstruction": [{
    "text": "1 tablet PO daily",
    "timing": { "repeat": { "frequency": 1, "period": 1, "periodUnit": "d" } }
  }]
}
```

## How RxNorm appears in HL7 v2 ORM^O01

```
OBR|1|ORD002||197361^Amlodipine 5 MG Oral Tablet^RXNORM
```

## ETL: RxNorm → OMOP drug_exposure

RxNorm is itself standard in OMOP, so no cross-vocabulary mapping is required:

```sql
INSERT INTO drug_exposure (
    drug_exposure_id,
    person_id,
    drug_concept_id,            -- RxNorm OMOP concept_id for 197361
    drug_exposure_start_date,
    drug_type_concept_id,
    drug_source_value,          -- '197361'
    quantity,
    days_supply
) VALUES (
    nextval('drug_exp_seq'),
    :person_id,
    1332419,
    :start_date,
    38000177,                   -- Prescription written
    '197361',
    30,
    30
);
```
