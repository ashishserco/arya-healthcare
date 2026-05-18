# Interoperability Standards — FullHealth Platform

This document describes how the FullHealth platform implements modern healthcare interoperability across the four pillars hiring teams typically evaluate:

1. **HL7 v2** — legacy hospital integration (still 80%+ of real-world EHR exchange in 2025)
2. **FHIR R4** — modern RESTful resource APIs
3. **OMOP CDM v5.4** — research-grade common data model
4. **Clinical terminologies** — SNOMED CT, LOINC, ICD-10, RxNorm

## Component map

```
                ┌──────────────────────────────────────────────┐
                │      FullHealth.Interop (ASP.NET Core 8)     │
                │                                              │
  HL7 v2 MLLP ─►│  /hl7v2/adt    /hl7v2/oru    /hl7v2/orm      │
                │     │              │              │          │
                │     ▼              ▼              ▼          │
                │  NHapi PipeParser (HL7 v2.5)                 │
                │     │                                        │
                │     ▼                                        │
                │  ┌────────────────────────────────────────┐  │
                │  │ Domain events: PatientAdmitted,        │  │
                │  │ LabResultReceived, MedicationOrdered   │  │
                │  └────────────────────────────────────────┘  │
                │     │                                        │
                │     ▼                                        │
  FHIR R4 REST ─►  /fhir/Patient   /fhir/Observation           │
                │  /fhir/Encounter /fhir/Condition             │
                │  /fhir/MedicationRequest                     │
                │  /fhir/Patient/{id}/$everything              │
                │     │                                        │
                │     ▼                                        │
                │  Firely .NET SDK (Hl7.Fhir.R4)               │
                │                                              │
                └──────────────┬───────────────────────────────┘
                               │
                               ▼
              ┌──────────────────────────────────┐
              │   PostgreSQL (Bronze + Silver)   │
              │   Raw JSONB / typed staging      │
              └──────────────┬───────────────────┘
                             │
                             ▼ ETL (FHIR → OMOP)
              ┌──────────────────────────────────┐
              │      OMOP CDM v5.4 (Gold)        │
              │   person, visit_occurrence,      │
              │   condition_occurrence,          │
              │   drug_exposure, measurement     │
              └──────────────┬───────────────────┘
                             │
                             ▼
              ┌──────────────────────────────────┐
              │  ATLAS / HADES / Superset        │
              │  Cohort definition, analytics    │
              └──────────────────────────────────┘
```

## Endpoint inventory

| Path | Method | Standard | Notes |
|------|--------|----------|-------|
| `/fhir/Patient/{id}` | GET | FHIR R4 | Read |
| `/fhir/Patient` | GET | FHIR R4 | Search by identifier, family |
| `/fhir/Patient/{id}/$everything` | GET | FHIR R4 | Operation: all resources for a patient |
| `/fhir/Observation/{id}` | GET | FHIR R4 | LOINC-coded observation read |
| `/fhir/Observation` | GET | FHIR R4 | Search by patient + code |
| `/fhir/Encounter/{id}` | GET | FHIR R4 | Read |
| `/fhir/Encounter` | GET | FHIR R4 | Search by patient + status |
| `/fhir/Condition/{id}` | GET | FHIR R4 | SNOMED + ICD-10 dual-coded |
| `/fhir/Condition` | GET | FHIR R4 | Search |
| `/fhir/MedicationRequest/{id}` | GET | FHIR R4 | RxNorm coded |
| `/fhir/MedicationRequest` | GET | FHIR R4 | Search |
| `/hl7v2/adt` | POST | HL7 v2.5 | Accepts ADT^A01; returns MSA ACK |
| `/hl7v2/oru` | POST | HL7 v2.5 | Accepts ORU^R01; returns MSA ACK |

## Code-system handling

Every coded clinical value carries the source `system` URI and code. At ETL time the source code is mapped to a standard OMOP `concept_id`:

```
Source (HL7 v2 DG1 "I10")
        │
        ▼
FHIR R4 Condition with coding[icd-10] + coding[snomed]
        │
        ▼
OMOP condition_occurrence:
  - condition_source_value         = 'I10'
  - condition_source_concept_id    = 44824068   (ICD-10 concept)
  - condition_concept_id           = 320128     (SNOMED standard)
```

See [`omop/etl/fhir_to_omop_mapping.md`](../omop/etl/fhir_to_omop_mapping.md) for the full per-resource mapping table.

## Security & compliance

- **PHI in transit:** TLS 1.2+ enforced; mTLS planned for service-to-service
- **PHI at rest:** PostgreSQL TDE; field-level encryption for direct identifiers
- **Audit:** every read or write of PHI is logged with subject, actor, timestamp, and request correlation id
- **Auth:** OAuth 2.0 + JWT bearer; per-resource RBAC; clinician role + patient-context scoping
- **Logs:** structured logs scrubbed of PHI before egress; no patient identifiers in log messages

## Standards versions in use

| Standard | Version | Reason |
|----------|---------|--------|
| HL7 v2 | 2.5 | Widest hospital deployment; NHapi support |
| FHIR | R4 (4.0.1) | Production stable; broad EHR support (Epic, Cerner) |
| OMOP CDM | v5.4 | Current stable; ATLAS and HADES compatible |
| SNOMED CT | International 2024-01-31 | |
| LOINC | 2.77 | |
| ICD-10-CM | 2024 | |
| RxNorm | 2024-01 monthly | |

## What is intentionally NOT yet implemented

This is a reference platform — the following items are roadmap, not production:

- [ ] OAuth 2.0 SMART-on-FHIR launch flow (planned)
- [ ] Bulk Data Access API ($export) for population-level FHIR exports
- [ ] FHIR Subscriptions (R4) for real-time event push
- [ ] HL7 v2 over MLLP TCP listener (currently HTTP-only for ease of testing)
- [ ] Full Athena vocabulary load (currently minimal seed only)
- [ ] CONDITION_ERA and DRUG_ERA derivation jobs
- [ ] ACHILLES data-quality dashboard

## References

- [HL7 FHIR R4 specification](https://hl7.org/fhir/R4/)
- [HL7 v2.5 specification](https://www.hl7.org/implement/standards/product_brief.cfm?product_id=185)
- [OMOP CDM v5.4](https://ohdsi.github.io/CommonDataModel/cdm54.html)
- [OHDSI Athena](https://athena.ohdsi.org/) — standardized vocabularies
- [Firely .NET FHIR SDK](https://docs.fire.ly/projects/Firely-NET-SDK/)
- [NHapi for HL7 v2](https://github.com/nHapiNET/nHapi)
