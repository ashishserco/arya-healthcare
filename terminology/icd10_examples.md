# ICD-10 / ICD-10-CM — Common Codes Reference

ICD-10 is the WHO classification of diseases used worldwide for diagnoses and cause-of-death reporting. ICD-10-CM is the United States clinical modification used by hospitals for billing and statistical reporting.

In OMOP, ICD-10 codes are a **source** vocabulary — at ETL time they are mapped to standard SNOMED concepts via `concept_relationship`.

## Cardiovascular (I00-I99)

| ICD-10 | Display | Maps to SNOMED |
|---|---|---|
| `I10` | Essential (primary) hypertension | `59621000` Essential hypertension |
| `I11.0` | Hypertensive heart disease with heart failure | |
| `I21.9` | Acute myocardial infarction, unspecified | `22298006` |
| `I25.10` | Atherosclerotic heart disease of native coronary artery without angina | |
| `I50.9` | Heart failure, unspecified | `84114007` |
| `I48.91` | Atrial fibrillation, unspecified | `49436004` |

## Endocrine (E00-E89)

| ICD-10 | Display | Maps to SNOMED |
|---|---|---|
| `E11.9` | Type 2 diabetes mellitus without complications | `44054006` |
| `E11.65` | Type 2 diabetes mellitus with hyperglycemia | |
| `E10.9` | Type 1 diabetes mellitus without complications | `46635009` |
| `E78.5` | Hyperlipidemia, unspecified | `55822004` |
| `E03.9` | Hypothyroidism, unspecified | `40930008` |

## Respiratory (J00-J99)

| ICD-10 | Display | Maps to SNOMED |
|---|---|---|
| `J45.909` | Unspecified asthma, uncomplicated | `195967001` |
| `J44.9` | COPD, unspecified | `13645005` |
| `J18.9` | Pneumonia, unspecified organism | `233604007` |
| `U07.1` | COVID-19 | `840539006` |

## Mental health (F01-F99)

| ICD-10 | Display |
|---|---|
| `F32.9` | Major depressive disorder, single episode, unspecified |
| `F41.1` | Generalized anxiety disorder |
| `F33.9` | Major depressive disorder, recurrent, unspecified |

## Injury (S00-T88)

| ICD-10 | Display |
|---|---|
| `S72.001A` | Fracture of unspecified part of neck of right femur, initial encounter |
| `S06.0X9A` | Concussion with loss of consciousness of unspecified duration, initial encounter |

## How ICD-10 appears in HL7 v2 DG1 segment

```
DG1|1||I10^Essential hypertension^I10|||W
```

DG1 fields: `set-id | type | diagnosis-code^description^coding-system | description | date | type`.

## ETL: ICD-10 → standard SNOMED

The OMOP pattern always preserves both source and standard:

```sql
INSERT INTO condition_occurrence (
    condition_occurrence_id,
    person_id,
    condition_concept_id,         -- SNOMED standard (320128)
    condition_start_date,
    condition_type_concept_id,
    condition_source_value,       -- 'I10' (original)
    condition_source_concept_id   -- ICD-10-CM concept (44824068)
) VALUES (
    nextval('cond_occ_seq'),
    :person_id,
    320128,
    :start_date,
    32817,                        -- EHR
    'I10',
    44824068
);
```
