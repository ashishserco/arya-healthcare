# SNOMED CT — Common Codes Reference

SNOMED CT is the international clinical terminology used for diagnoses, problems, findings, and procedures. SNOMED is the **standard** vocabulary in OMOP for the `Condition` and `Procedure` domains.

## Hypertension cluster (cardiovascular)

| SNOMED code | Display | Notes |
|---|---|---|
| `38341003` | Hypertensive disorder, systemic arterial | Parent concept |
| `59621000` | Essential hypertension | Standard OMOP `Condition` concept |
| `46113002` | Pre-eclampsia | Pregnancy-related |
| `194783001` | Secondary hypertension | |

## Diabetes cluster (endocrine)

| SNOMED code | Display | Maps from ICD-10 |
|---|---|---|
| `73211009` | Diabetes mellitus | E10-E14 |
| `44054006` | Type 2 diabetes mellitus | E11 |
| `46635009` | Type 1 diabetes mellitus | E10 |
| `4855003` | Diabetic retinopathy | E11.3 |

## Respiratory

| SNOMED code | Display |
|---|---|
| `195967001` | Asthma |
| `13645005` | Chronic obstructive pulmonary disease |
| `233604007` | Pneumonia |
| `840539006` | Disease caused by SARS-CoV-2 |

## Procedures

| SNOMED code | Display |
|---|---|
| `35884008` | Coronary artery bypass grafting |
| `387713003` | Surgical procedure |
| `108253007` | Laboratory procedure |
| `78318003` | Magnetic resonance imaging |

## Clinical findings (signs / symptoms)

| SNOMED code | Display |
|---|---|
| `25064002` | Headache |
| `49727002` | Cough |
| `386661006` | Fever |
| `267036007` | Dyspnea |

## Hierarchical roll-up

A key SNOMED feature is the IS-A hierarchy, exposed in OMOP via `concept_ancestor`. Querying "all patients with any form of diabetes" is a single join:

```sql
SELECT DISTINCT co.person_id
FROM condition_occurrence co
JOIN concept_ancestor ca
  ON ca.descendant_concept_id = co.condition_concept_id
WHERE ca.ancestor_concept_id = 201820;   -- SNOMED Diabetes mellitus
```

This returns Type 1, Type 2, gestational, secondary, and any descendant condition without enumerating each code.
