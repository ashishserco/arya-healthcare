-- =====================================================================
-- Sample seed data for OMOP vocabularies
-- Real-world deployments use OHDSI Athena vocabulary downloads:
--   https://athena.ohdsi.org/
-- =====================================================================

SET search_path TO omop_cdm;

-- Vocabularies
INSERT INTO vocabulary (vocabulary_id, vocabulary_name, vocabulary_reference, vocabulary_version, vocabulary_concept_id) VALUES
  ('SNOMED',  'SNOMED CT (International Edition)', 'https://www.snomed.org/',           '2024-01-31', 1),
  ('LOINC',   'Logical Observation Identifiers Names and Codes', 'https://loinc.org/',  '2.77',       2),
  ('ICD10',   'International Classification of Diseases v10', 'https://icd.who.int/',    '2024',       3),
  ('ICD10CM', 'International Classification of Diseases v10 (Clinical Modification)', 'https://www.cdc.gov/nchs/icd/icd-10-cm.htm', '2024', 4),
  ('RxNorm',  'RxNorm', 'https://www.nlm.nih.gov/research/umls/rxnorm/',                 '2024-01',    5),
  ('CPT4',    'Current Procedural Terminology v4', 'https://www.ama-assn.org/',          '2024',       6),
  ('HL7v2',   'HL7 Version 2.x', 'https://www.hl7.org/',                                  '2.5',        7)
ON CONFLICT DO NOTHING;

-- Domains
INSERT INTO domain (domain_id, domain_name, domain_concept_id) VALUES
  ('Condition',   'Condition',   100),
  ('Drug',        'Drug',        101),
  ('Measurement', 'Measurement', 102),
  ('Observation', 'Observation', 103),
  ('Procedure',   'Procedure',   104),
  ('Visit',       'Visit',       105),
  ('Gender',      'Gender',      106),
  ('Race',        'Race',        107),
  ('Ethnicity',   'Ethnicity',   108)
ON CONFLICT DO NOTHING;

-- Concept classes
INSERT INTO concept_class (concept_class_id, concept_class_name, concept_class_concept_id) VALUES
  ('Clinical Finding', 'Clinical Finding', 200),
  ('Lab Test',         'Lab Test',         201),
  ('Clinical Drug',    'Clinical Drug',    202),
  ('Visit',            'Visit',            203),
  ('ICD10 code',       'ICD10 code',       204)
ON CONFLICT DO NOTHING;

-- Sample concepts demonstrating cross-vocabulary mapping
-- Hypertension is represented in SNOMED, ICD-10, and as the standard OMOP concept
INSERT INTO concept (concept_id, concept_name, domain_id, vocabulary_id, concept_class_id, standard_concept, concept_code, valid_start_date, valid_end_date) VALUES
  (320128,   'Essential hypertension',                 'Condition',   'SNOMED',  'Clinical Finding', 'S', '59621000',  '1970-01-01', '2099-12-31'),
  (44824068, 'Essential (primary) hypertension',       'Condition',   'ICD10CM', 'ICD10 code',       NULL, 'I10',       '2007-10-01', '2099-12-31'),
  (3004249,  'Systolic blood pressure',                'Measurement', 'LOINC',   'Lab Test',         'S', '8480-6',    '1970-01-01', '2099-12-31'),
  (3012888,  'Diastolic blood pressure',               'Measurement', 'LOINC',   'Lab Test',         'S', '8462-4',    '1970-01-01', '2099-12-31'),
  (3034639,  'Hemoglobin A1c/Hemoglobin.total in Blood','Measurement','LOINC',  'Lab Test',         'S', '4548-4',    '1970-01-01', '2099-12-31'),
  (1332419,  'Amlodipine 5 MG Oral Tablet',            'Drug',        'RxNorm',  'Clinical Drug',    'S', '197361',    '1970-01-01', '2099-12-31'),
  (9201,     'Inpatient Visit',                        'Visit',       'Visit',   'Visit',            'S', 'IP',        '1970-01-01', '2099-12-31'),
  (8507,     'MALE',                                   'Gender',      'Gender',  'Gender',           'S', 'M',         '1970-01-01', '2099-12-31'),
  (8532,     'FEMALE',                                 'Gender',      'Gender',  'Gender',           'S', 'F',         '1970-01-01', '2099-12-31')
ON CONFLICT DO NOTHING;

-- Cross-vocabulary mapping: ICD-10 I10 maps to SNOMED 320128 (standard)
INSERT INTO concept_relationship (concept_id_1, concept_id_2, relationship_id, valid_start_date, valid_end_date) VALUES
  (44824068, 320128, 'Maps to',     '1970-01-01', '2099-12-31'),
  (320128,   44824068, 'Mapped from','1970-01-01', '2099-12-31')
ON CONFLICT DO NOTHING;
