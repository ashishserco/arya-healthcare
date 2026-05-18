-- =====================================================================
-- OMOP CDM v5.4 - Standardized Vocabularies
-- The concept_id integer key is the heart of OMOP: every coded fact in
-- clinical tables references a CONCEPT row, which carries the source
-- vocabulary (SNOMED, LOINC, ICD-10, RxNorm, etc.) and standard mapping.
-- =====================================================================

SET search_path TO omop_cdm;

-- ---------------------------------------------------------------------
-- VOCABULARY: registry of supported vocabularies
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS vocabulary (
    vocabulary_id           VARCHAR(20)  NOT NULL PRIMARY KEY,
    vocabulary_name         VARCHAR(255) NOT NULL,
    vocabulary_reference    VARCHAR(255),
    vocabulary_version      VARCHAR(255),
    vocabulary_concept_id   INTEGER      NOT NULL
);

-- ---------------------------------------------------------------------
-- DOMAIN: clinical domain (Condition, Drug, Measurement, etc.)
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS domain (
    domain_id          VARCHAR(20)  NOT NULL PRIMARY KEY,
    domain_name        VARCHAR(255) NOT NULL,
    domain_concept_id  INTEGER      NOT NULL
);

-- ---------------------------------------------------------------------
-- CONCEPT_CLASS: classification within a vocabulary
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS concept_class (
    concept_class_id          VARCHAR(20)  NOT NULL PRIMARY KEY,
    concept_class_name        VARCHAR(255) NOT NULL,
    concept_class_concept_id  INTEGER      NOT NULL
);

-- ---------------------------------------------------------------------
-- CONCEPT: the master vocabulary table
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS concept (
    concept_id          INTEGER      NOT NULL PRIMARY KEY,
    concept_name        VARCHAR(255) NOT NULL,
    domain_id           VARCHAR(20)  NOT NULL REFERENCES domain(domain_id),
    vocabulary_id       VARCHAR(20)  NOT NULL REFERENCES vocabulary(vocabulary_id),
    concept_class_id    VARCHAR(20)  NOT NULL REFERENCES concept_class(concept_class_id),
    standard_concept    VARCHAR(1),
    concept_code        VARCHAR(50)  NOT NULL,
    valid_start_date    DATE         NOT NULL,
    valid_end_date      DATE         NOT NULL,
    invalid_reason      VARCHAR(1)
);

CREATE INDEX idx_concept_code      ON concept(concept_code);
CREATE INDEX idx_concept_vocab     ON concept(vocabulary_id);
CREATE INDEX idx_concept_domain    ON concept(domain_id);
CREATE INDEX idx_concept_standard  ON concept(standard_concept);

-- ---------------------------------------------------------------------
-- CONCEPT_RELATIONSHIP: relationships between concepts (mapping, IS-A, etc.)
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS concept_relationship (
    concept_id_1        INTEGER      NOT NULL REFERENCES concept(concept_id),
    concept_id_2        INTEGER      NOT NULL REFERENCES concept(concept_id),
    relationship_id     VARCHAR(20)  NOT NULL,
    valid_start_date    DATE         NOT NULL,
    valid_end_date      DATE         NOT NULL,
    invalid_reason      VARCHAR(1),
    PRIMARY KEY (concept_id_1, concept_id_2, relationship_id)
);

CREATE INDEX idx_concept_rel_1   ON concept_relationship(concept_id_1);
CREATE INDEX idx_concept_rel_2   ON concept_relationship(concept_id_2);

-- ---------------------------------------------------------------------
-- CONCEPT_ANCESTOR: hierarchical roll-up for "all descendants of X"
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS concept_ancestor (
    ancestor_concept_id        INTEGER NOT NULL REFERENCES concept(concept_id),
    descendant_concept_id      INTEGER NOT NULL REFERENCES concept(concept_id),
    min_levels_of_separation   INTEGER NOT NULL,
    max_levels_of_separation   INTEGER NOT NULL,
    PRIMARY KEY (ancestor_concept_id, descendant_concept_id)
);

CREATE INDEX idx_anc_desc ON concept_ancestor(descendant_concept_id);
