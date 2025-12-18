# Missing Features and Enhancements

## 1. Feature Description
This document details the specific capabilities identified as "Missing" or "Partial" in the Benchmark Matrix. It provides the rationale and high-level requirements for closing these gaps.

## 2. Identified Gaps

### A. Integrated Content Management (Patient Education)
- **Feature**: A system to serve, manage, and search health articles and videos.
- **Why Needed**: Patients trust providers who educate them. Reduces "Dr. Google" anxiety.
- **Inspired By**: Mayo Clinic, WebMD.
- **Implementation**: Introduce a headless CMS or a new `ContentService` that stores article metadata and links to blob storage.

### B. Mental Health Triage & Guidance
- **Feature**: Questionnaire-based routing for mental health needs.
- **Why Needed**: Mental health access is a crisis; generic booking doesn't capture urgency or specific need type.
- **Inspired By**: Unmind, CALM, Hertility Health.
- **Implementation**: A "Get Help" flow that asks 3-5 screening questions before showing providers.

### C. Provider Rich Profiles
- **Feature**: Expanded provider data model including bio, languages, special interest tags, and video url.
- **Why Needed**: Patients choose people, not just "slots".
- **Inspired By**: Zocdoc, Hospital systems.
- **Implementation**: Schema update on `ProviderService` (PostgreSQL).

### D. Anonymous/Guest Flows
- **Feature**: Ability to search content and providers *without* logging in first.
- **Why Needed**: Login walls cause high drop-off for new patients exploring care options.
- **Inspired By**: Most e-commerce and modern health startups.
- **Implementation**: Public endpoints in API Gateway for Read-Only operations.

### E. AI-Driven Support (Guidance)
- **Feature**: An LLM-backed agent to answer FAQ and assist with navigation.
- **Why Needed**: 24/7 availability for non-clinical questions.
- **Inspired By**: Chatbots on insurance portals.
- **Implementation**: `AI Agent` integration using Azure OpenAI.

## 3. Implementation Strategy (Safe & Generic)
We will implement these using standard patterns (Microservices + API Gateway).
- **No Proprietary Algorithms**: Logic will be configuration-driven or simple decision trees.
- **No Real Medical Advice**: "Symptom Checkers" will act as *navigational* aids (e.g., "Go to ER"), not diagnostic tools.
- **Disclaimer**: All new user-facing flows will include clear medical disclaimers.

## 4. Security
- **Public vs Private**: Explicitly define what data is public (Provider bios) vs private (Patient Triage answers).
- **Rate Limiting**: Protect public endpoints (search) from scraping.
