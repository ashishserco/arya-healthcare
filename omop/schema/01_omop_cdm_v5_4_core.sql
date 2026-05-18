-- =====================================================================
-- OMOP Common Data Model v5.4 - Core Clinical Data Tables
-- Reference: https://ohdsi.github.io/CommonDataModel/cdm54.html
-- Target: PostgreSQL 14+
-- =====================================================================

CREATE SCHEMA IF NOT EXISTS omop_cdm;
SET search_path TO omop_cdm;

-- ---------------------------------------------------------------------
-- PERSON: demographics, one row per individual
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS person (
    person_id                       BIGINT       NOT NULL PRIMARY KEY,
    gender_concept_id               INTEGER      NOT NULL,
    year_of_birth                   INTEGER      NOT NULL,
    month_of_birth                  INTEGER,
    day_of_birth                    INTEGER,
    birth_datetime                  TIMESTAMP,
    race_concept_id                 INTEGER      NOT NULL,
    ethnicity_concept_id            INTEGER      NOT NULL,
    location_id                     BIGINT,
    provider_id                     BIGINT,
    care_site_id                    BIGINT,
    person_source_value             VARCHAR(50),
    gender_source_value             VARCHAR(50),
    gender_source_concept_id        INTEGER,
    race_source_value               VARCHAR(50),
    race_source_concept_id          INTEGER,
    ethnicity_source_value          VARCHAR(50),
    ethnicity_source_concept_id     INTEGER
);

CREATE INDEX idx_person_gender ON person(gender_concept_id);
CREATE INDEX idx_person_yob    ON person(year_of_birth);

-- ---------------------------------------------------------------------
-- OBSERVATION_PERIOD: spans of time for which clinical data is reliable
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS observation_period (
    observation_period_id           BIGINT       NOT NULL PRIMARY KEY,
    person_id                       BIGINT       NOT NULL REFERENCES person(person_id),
    observation_period_start_date   DATE         NOT NULL,
    observation_period_end_date     DATE         NOT NULL,
    period_type_concept_id          INTEGER      NOT NULL
);

CREATE INDEX idx_obs_period_person ON observation_period(person_id);

-- ---------------------------------------------------------------------
-- VISIT_OCCURRENCE: encounters (inpatient, outpatient, emergency)
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS visit_occurrence (
    visit_occurrence_id             BIGINT       NOT NULL PRIMARY KEY,
    person_id                       BIGINT       NOT NULL REFERENCES person(person_id),
    visit_concept_id                INTEGER      NOT NULL,
    visit_start_date                DATE         NOT NULL,
    visit_start_datetime            TIMESTAMP,
    visit_end_date                  DATE         NOT NULL,
    visit_end_datetime              TIMESTAMP,
    visit_type_concept_id           INTEGER      NOT NULL,
    provider_id                     BIGINT,
    care_site_id                    BIGINT,
    visit_source_value              VARCHAR(50),
    visit_source_concept_id         INTEGER,
    admitted_from_concept_id        INTEGER,
    admitted_from_source_value      VARCHAR(50),
    discharged_to_concept_id        INTEGER,
    discharged_to_source_value      VARCHAR(50),
    preceding_visit_occurrence_id   BIGINT
);

CREATE INDEX idx_visit_person      ON visit_occurrence(person_id);
CREATE INDEX idx_visit_concept     ON visit_occurrence(visit_concept_id);
CREATE INDEX idx_visit_start_date  ON visit_occurrence(visit_start_date);

-- ---------------------------------------------------------------------
-- CONDITION_OCCURRENCE: diagnoses, problems
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS condition_occurrence (
    condition_occurrence_id         BIGINT       NOT NULL PRIMARY KEY,
    person_id                       BIGINT       NOT NULL REFERENCES person(person_id),
    condition_concept_id            INTEGER      NOT NULL,
    condition_start_date            DATE         NOT NULL,
    condition_start_datetime        TIMESTAMP,
    condition_end_date              DATE,
    condition_end_datetime          TIMESTAMP,
    condition_type_concept_id       INTEGER      NOT NULL,
    condition_status_concept_id     INTEGER,
    stop_reason                     VARCHAR(20),
    provider_id                     BIGINT,
    visit_occurrence_id             BIGINT       REFERENCES visit_occurrence(visit_occurrence_id),
    visit_detail_id                 BIGINT,
    condition_source_value          VARCHAR(50),
    condition_source_concept_id     INTEGER,
    condition_status_source_value   VARCHAR(50)
);

CREATE INDEX idx_cond_person   ON condition_occurrence(person_id);
CREATE INDEX idx_cond_concept  ON condition_occurrence(condition_concept_id);
CREATE INDEX idx_cond_visit    ON condition_occurrence(visit_occurrence_id);
CREATE INDEX idx_cond_start    ON condition_occurrence(condition_start_date);

-- ---------------------------------------------------------------------
-- DRUG_EXPOSURE: medications administered or prescribed
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS drug_exposure (
    drug_exposure_id                BIGINT       NOT NULL PRIMARY KEY,
    person_id                       BIGINT       NOT NULL REFERENCES person(person_id),
    drug_concept_id                 INTEGER      NOT NULL,
    drug_exposure_start_date        DATE         NOT NULL,
    drug_exposure_start_datetime    TIMESTAMP,
    drug_exposure_end_date          DATE         NOT NULL,
    drug_exposure_end_datetime      TIMESTAMP,
    verbatim_end_date               DATE,
    drug_type_concept_id            INTEGER      NOT NULL,
    stop_reason                     VARCHAR(20),
    refills                         INTEGER,
    quantity                        NUMERIC,
    days_supply                     INTEGER,
    sig                             TEXT,
    route_concept_id                INTEGER,
    lot_number                      VARCHAR(50),
    provider_id                     BIGINT,
    visit_occurrence_id             BIGINT       REFERENCES visit_occurrence(visit_occurrence_id),
    visit_detail_id                 BIGINT,
    drug_source_value               VARCHAR(50),
    drug_source_concept_id          INTEGER,
    route_source_value              VARCHAR(50),
    dose_unit_source_value          VARCHAR(50)
);

CREATE INDEX idx_drug_person   ON drug_exposure(person_id);
CREATE INDEX idx_drug_concept  ON drug_exposure(drug_concept_id);
CREATE INDEX idx_drug_visit    ON drug_exposure(visit_occurrence_id);

-- ---------------------------------------------------------------------
-- PROCEDURE_OCCURRENCE: procedures performed
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS procedure_occurrence (
    procedure_occurrence_id         BIGINT       NOT NULL PRIMARY KEY,
    person_id                       BIGINT       NOT NULL REFERENCES person(person_id),
    procedure_concept_id            INTEGER      NOT NULL,
    procedure_date                  DATE         NOT NULL,
    procedure_datetime              TIMESTAMP,
    procedure_end_date              DATE,
    procedure_end_datetime          TIMESTAMP,
    procedure_type_concept_id       INTEGER      NOT NULL,
    modifier_concept_id             INTEGER,
    quantity                        INTEGER,
    provider_id                     BIGINT,
    visit_occurrence_id             BIGINT       REFERENCES visit_occurrence(visit_occurrence_id),
    visit_detail_id                 BIGINT,
    procedure_source_value          VARCHAR(50),
    procedure_source_concept_id     INTEGER,
    modifier_source_value           VARCHAR(50)
);

CREATE INDEX idx_proc_person   ON procedure_occurrence(person_id);
CREATE INDEX idx_proc_concept  ON procedure_occurrence(procedure_concept_id);

-- ---------------------------------------------------------------------
-- MEASUREMENT: lab values, vital signs, quantitative observations
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS measurement (
    measurement_id                  BIGINT       NOT NULL PRIMARY KEY,
    person_id                       BIGINT       NOT NULL REFERENCES person(person_id),
    measurement_concept_id          INTEGER      NOT NULL,
    measurement_date                DATE         NOT NULL,
    measurement_datetime            TIMESTAMP,
    measurement_time                VARCHAR(10),
    measurement_type_concept_id     INTEGER      NOT NULL,
    operator_concept_id             INTEGER,
    value_as_number                 NUMERIC,
    value_as_concept_id             INTEGER,
    unit_concept_id                 INTEGER,
    range_low                       NUMERIC,
    range_high                      NUMERIC,
    provider_id                     BIGINT,
    visit_occurrence_id             BIGINT       REFERENCES visit_occurrence(visit_occurrence_id),
    visit_detail_id                 BIGINT,
    measurement_source_value        VARCHAR(50),
    measurement_source_concept_id   INTEGER,
    unit_source_value               VARCHAR(50),
    unit_source_concept_id          INTEGER,
    value_source_value              VARCHAR(50),
    measurement_event_id            BIGINT,
    meas_event_field_concept_id     INTEGER
);

CREATE INDEX idx_meas_person   ON measurement(person_id);
CREATE INDEX idx_meas_concept  ON measurement(measurement_concept_id);
CREATE INDEX idx_meas_visit    ON measurement(visit_occurrence_id);
CREATE INDEX idx_meas_date     ON measurement(measurement_date);

-- ---------------------------------------------------------------------
-- OBSERVATION: non-measurement clinical facts
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS observation (
    observation_id                  BIGINT       NOT NULL PRIMARY KEY,
    person_id                       BIGINT       NOT NULL REFERENCES person(person_id),
    observation_concept_id          INTEGER      NOT NULL,
    observation_date                DATE         NOT NULL,
    observation_datetime            TIMESTAMP,
    observation_type_concept_id     INTEGER      NOT NULL,
    value_as_number                 NUMERIC,
    value_as_string                 VARCHAR(60),
    value_as_concept_id             INTEGER,
    qualifier_concept_id            INTEGER,
    unit_concept_id                 INTEGER,
    provider_id                     BIGINT,
    visit_occurrence_id             BIGINT       REFERENCES visit_occurrence(visit_occurrence_id),
    visit_detail_id                 BIGINT,
    observation_source_value        VARCHAR(50),
    observation_source_concept_id   INTEGER,
    unit_source_value               VARCHAR(50),
    qualifier_source_value          VARCHAR(50),
    value_source_value              VARCHAR(50),
    observation_event_id            BIGINT,
    obs_event_field_concept_id      INTEGER
);

CREATE INDEX idx_obs_person   ON observation(person_id);
CREATE INDEX idx_obs_concept  ON observation(observation_concept_id);

-- ---------------------------------------------------------------------
-- DEATH
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS death (
    person_id                       BIGINT       NOT NULL PRIMARY KEY REFERENCES person(person_id),
    death_date                      DATE         NOT NULL,
    death_datetime                  TIMESTAMP,
    death_type_concept_id           INTEGER,
    cause_concept_id                INTEGER,
    cause_source_value              VARCHAR(50),
    cause_source_concept_id         INTEGER
);
