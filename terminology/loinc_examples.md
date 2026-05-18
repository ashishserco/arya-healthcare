# LOINC — Common Codes Reference

LOINC (Logical Observation Identifiers Names and Codes) is the standard vocabulary for laboratory tests, vital signs, and clinical observations. LOINC is the **standard** OMOP vocabulary for the `Measurement` domain.

Every LOINC code has six axes: **Component** (what), **Property** (mass/concentration/etc.), **Time**, **System** (specimen type), **Scale** (quantitative/ordinal/nominal), and **Method**.

## Vital signs

| LOINC | Display | Unit (UCUM) |
|---|---|---|
| `85354-9` | Blood pressure panel | — |
| `8480-6` | Systolic blood pressure | `mm[Hg]` |
| `8462-4` | Diastolic blood pressure | `mm[Hg]` |
| `8867-4` | Heart rate | `/min` |
| `9279-1` | Respiratory rate | `/min` |
| `8310-5` | Body temperature | `Cel` |
| `2708-6` | Oxygen saturation in arterial blood | `%` |
| `29463-7` | Body weight | `kg` |
| `8302-2` | Body height | `cm` |
| `39156-5` | Body mass index | `kg/m2` |

## Chemistry — common labs

| LOINC | Display | Unit |
|---|---|---|
| `2339-0` | Glucose, blood | `mg/dL` |
| `4548-4` | Hemoglobin A1c | `%` |
| `2160-0` | Creatinine, serum | `mg/dL` |
| `3094-0` | Urea nitrogen, blood (BUN) | `mg/dL` |
| `2951-2` | Sodium, serum | `mmol/L` |
| `2823-3` | Potassium, serum | `mmol/L` |
| `2075-0` | Chloride, serum | `mmol/L` |
| `2069-3` | Bicarbonate (CO2), serum | `mmol/L` |

## Lipid panel

| LOINC | Display | Unit |
|---|---|---|
| `2093-3` | Cholesterol, total | `mg/dL` |
| `2085-9` | HDL cholesterol | `mg/dL` |
| `13457-7` | LDL cholesterol (calculated) | `mg/dL` |
| `2571-8` | Triglycerides | `mg/dL` |

## Hematology — CBC

| LOINC | Display | Unit |
|---|---|---|
| `718-7` | Hemoglobin, blood | `g/dL` |
| `4544-3` | Hematocrit, blood | `%` |
| `6690-2` | Leukocytes, blood | `10*3/uL` |
| `777-3` | Platelets, blood | `10*3/uL` |

## Liver function

| LOINC | Display | Unit |
|---|---|---|
| `1742-6` | ALT | `U/L` |
| `1920-8` | AST | `U/L` |
| `1751-7` | Albumin | `g/dL` |
| `1968-7` | Bilirubin, total | `mg/dL` |

## How LOINC appears in FHIR Observation

```json
{
  "resourceType": "Observation",
  "status": "final",
  "code": {
    "coding": [{
      "system": "http://loinc.org",
      "code": "4548-4",
      "display": "Hemoglobin A1c/Hemoglobin.total in Blood"
    }]
  },
  "valueQuantity": {
    "value": 7.2,
    "unit": "%",
    "system": "http://unitsofmeasure.org",
    "code": "%"
  }
}
```

## How LOINC appears in HL7 v2 ORU^R01

```
OBX|1|NM|4548-4^Hemoglobin A1c^LN||7.2|%|4.0-5.6|H|||F
```

OBX segment fields: `set-id | value-type | observation-id^name^coding-system | sub-id | value | units | reference-range | abnormal-flags | probability | nature | status`.
