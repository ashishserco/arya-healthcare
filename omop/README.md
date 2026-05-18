# OMOP Common Data Model — FullHealth Implementation

This folder contains the OMOP CDM v5.4 data model definitions, vocabulary seed data, ETL mapping notes, and reference analytical queries used by the FullHealth platform for population-health and clinical research workloads.

## What is OMOP?

The **Observational Medical Outcomes Partnership (OMOP) Common Data Model** is maintained by [OHDSI](https://www.ohdsi.org/) (Observational Health Data Sciences and Informatics). It standardizes the structure of observational healthcare data so that the same analytical query — written once — can run across hospitals, countries, and EHR vendors.

Reference: <https://ohdsi.github.io/CommonDataModel/cdm54.html>

## Why we use it

- **Cross-institution analytics:** the same cohort definition runs against any OMOP-compliant data warehouse.
- **Standard vocabularies:** every coded clinical fact resolves to an OMOP `concept_id`, mapped from the source vocabulary (SNOMED, LOINC, ICD-10, RxNorm).
- **Research tooling:** ATLAS for cohort definition, HADES for advanced analytics, ACHILLES for data quality.
- **Regulatory acceptance:** OMOP is the de-facto standard for FDA, EMA, and PCORI research collaborations.

## Folder layout

```
omop/
├── schema/
│   ├── 01_omop_cdm_v5_4_core.sql        ← clinical event tables (PERSON, VISIT_OCCURRENCE, ...)
│   └── 02_omop_cdm_v5_4_vocabulary.sql  ← VOCABULARY, CONCEPT, CONCEPT_RELATIONSHIP, CONCEPT_ANCESTOR
├── seed/
│   └── sample_concepts.sql              ← minimal cross-vocabulary concept seed for local dev
├── queries/
│   └── common_analytics.sql             ← reference cohort and outcomes queries
└── etl/
    └── fhir_to_omop_mapping.md          ← FHIR resource → OMOP table mapping
```

## Core tables

| Table | Purpose | Primary code system |
|---|---|---|
| `person` | Demographics, one row per individual | — |
| `observation_period` | Windows of reliable data per person | — |
| `visit_occurrence` | Encounters (inpatient, outpatient, ED) | OMOP visit concepts |
| `condition_occurrence` | Diagnoses, problems | SNOMED CT (standard) ← ICD-10 (source) |
| `drug_exposure` | Medications administered or prescribed | RxNorm |
| `procedure_occurrence` | Procedures performed | SNOMED CT ← CPT, HCPCS |
| `measurement` | Lab values, vital signs | LOINC |
| `observation` | Non-measurement clinical facts | mixed |
| `death` | Death record | — |
| `concept` | Master vocabulary | all standards |
| `concept_relationship` | Cross-vocabulary mappings (`Maps to`, `Mapped from`) | — |
| `concept_ancestor` | Hierarchical roll-up for "all descendants of X" | — |

## The `concept_id` pattern

Every coded clinical column in OMOP is an integer foreign key into `concept`. A row in `condition_occurrence` like:

```
condition_concept_id = 320128
condition_source_value = 'I10'
condition_source_concept_id = 44824068
```

means:

- The **standard OMOP concept** is `320128` — SNOMED `59621000` "Essential hypertension"
- The **source code** was `I10` from ICD-10-CM (concept_id `44824068`)
- These are linked in `concept_relationship` via `Maps to` / `Mapped from`

This is what enables cross-vocabulary analytics with one query.

## Running locally

```bash
# 1. Provision PostgreSQL 14+
createdb fullhealth_omop

# 2. Apply schema
psql -d fullhealth_omop -f schema/01_omop_cdm_v5_4_core.sql
psql -d fullhealth_omop -f schema/02_omop_cdm_v5_4_vocabulary.sql

# 3. Load minimal seed (for production, download from OHDSI Athena instead)
psql -d fullhealth_omop -f seed/sample_concepts.sql

# 4. Run sample analytics
psql -d fullhealth_omop -f queries/common_analytics.sql
```

## Production vocabulary

For real deployments, OHDSI [Athena](https://athena.ohdsi.org/) provides the complete standardized vocabulary download (~ 100 GB). The seed in `seed/sample_concepts.sql` is intentionally minimal for local development.

## ETL strategy

Source data lands in HL7 v2 (legacy hospital integration) and FHIR R4 (modern APIs). We map both streams into OMOP via the ETL described in [`etl/fhir_to_omop_mapping.md`](etl/fhir_to_omop_mapping.md). At a high level:

```
HL7 v2 ADT/ORU/ORM  ─┐
                     ├──► FHIR resources ──► OMOP staging ──► OMOP CDM ──► ATLAS / Superset
FHIR R4 (REST)      ─┘
```
