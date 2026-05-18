-- =====================================================================
-- Common OMOP analytical queries
-- These illustrate the kind of cohort and outcome queries that ATLAS
-- and HADES generate against the OMOP CDM.
-- =====================================================================

SET search_path TO omop_cdm;

-- ---------------------------------------------------------------------
-- Q1: Count of patients with a hypertension diagnosis (incl. all descendants)
-- Uses concept_ancestor to roll up - the OMOP standard pattern.
-- ---------------------------------------------------------------------
SELECT COUNT(DISTINCT co.person_id) AS hypertension_patient_count
FROM condition_occurrence co
JOIN concept_ancestor ca
  ON ca.descendant_concept_id = co.condition_concept_id
WHERE ca.ancestor_concept_id = 320128;   -- SNOMED Essential hypertension

-- ---------------------------------------------------------------------
-- Q2: Mean systolic blood pressure by gender for adults 40-65
-- ---------------------------------------------------------------------
SELECT
    c.concept_name                 AS gender,
    COUNT(DISTINCT m.person_id)    AS patient_count,
    AVG(m.value_as_number)::NUMERIC(10,2) AS mean_systolic_mmhg,
    STDDEV(m.value_as_number)::NUMERIC(10,2) AS stddev_systolic_mmhg
FROM measurement m
JOIN person p
  ON p.person_id = m.person_id
JOIN concept c
  ON c.concept_id = p.gender_concept_id
WHERE m.measurement_concept_id = 3004249    -- LOINC Systolic BP
  AND EXTRACT(YEAR FROM AGE(m.measurement_date, MAKE_DATE(p.year_of_birth, 1, 1))) BETWEEN 40 AND 65
GROUP BY c.concept_name
ORDER BY gender;

-- ---------------------------------------------------------------------
-- Q3: New users of Amlodipine in 2024 (incident exposure cohort)
-- ---------------------------------------------------------------------
WITH first_exposure AS (
    SELECT
        person_id,
        MIN(drug_exposure_start_date) AS first_amlodipine_date
    FROM drug_exposure
    WHERE drug_concept_id = 1332419           -- RxNorm Amlodipine 5 MG
    GROUP BY person_id
)
SELECT
    person_id,
    first_amlodipine_date
FROM first_exposure
WHERE first_amlodipine_date >= DATE '2024-01-01'
  AND first_amlodipine_date <  DATE '2025-01-01'
ORDER BY first_amlodipine_date;

-- ---------------------------------------------------------------------
-- Q4: Patients with hypertension AND elevated HbA1c (>= 6.5) within 90 days
-- Co-occurrence / cohort intersection pattern.
-- ---------------------------------------------------------------------
SELECT DISTINCT co.person_id
FROM condition_occurrence co
JOIN measurement m
  ON m.person_id = co.person_id
WHERE co.condition_concept_id = 320128                      -- Hypertension
  AND m.measurement_concept_id = 3034639                    -- HbA1c
  AND m.value_as_number >= 6.5
  AND m.measurement_date BETWEEN co.condition_start_date
                            AND co.condition_start_date + INTERVAL '90 days';

-- ---------------------------------------------------------------------
-- Q5: Inpatient length-of-stay distribution by diagnosis
-- ---------------------------------------------------------------------
SELECT
    c.concept_name                                              AS diagnosis,
    COUNT(*)                                                    AS visit_count,
    AVG(vo.visit_end_date - vo.visit_start_date)::NUMERIC(10,1) AS mean_los_days,
    PERCENTILE_CONT(0.5) WITHIN GROUP (ORDER BY vo.visit_end_date - vo.visit_start_date) AS median_los_days
FROM visit_occurrence vo
JOIN condition_occurrence co
  ON co.visit_occurrence_id = vo.visit_occurrence_id
JOIN concept c
  ON c.concept_id = co.condition_concept_id
WHERE vo.visit_concept_id = 9201   -- Inpatient Visit
GROUP BY c.concept_name
ORDER BY visit_count DESC
LIMIT 20;
