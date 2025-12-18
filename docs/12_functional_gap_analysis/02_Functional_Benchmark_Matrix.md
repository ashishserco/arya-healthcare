# Functional Benchmark Matrix

## 1. Feature Description
A comparative analysis of the current platform against industry-standard healthcare capabilities. This matrix identifies gaps to prioritize feature development.

## 2. Why It Exists
To provide a clear, executive-level view of platform maturity and roadmap priorities.

## 3. Comparison Matrix

| Feature Category | Specific Capability | Common in Industry | Present in Our Platform | Gap | Severity | Action |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Patient Education** | Searchable Health Library | Yes | No | **Yes** | High | Create `ContentService`; Integrate CMS |
| | Personalized Recommendations | Yes | No | **Yes** | Medium | AI-driven suggestions based on profile |
| **Mental Health** | Triage/Assessment Forms | Yes | No | **Yes** | Critical | Add `GuidanceService` for assessments |
| | Crisis Resource Access | Yes | No | **Yes** | Critical | Add static resources & immediate help UI |
| | Provider Matching | Yes | Partial | Partial | Medium | Enhance Search with "Matches" logic |
| **Appointments** | Real-time Availability | Yes | Yes | No | - | Optimize existing `AppointmentService` |
| | Multi-provider/Team Booking | Yes | No | **Yes** | Low | Future phase |
| **Provider Search** | Rich Profiles (Bio, Video) | Yes | Basic | Partial | Medium | Expand `ProviderService` data model |
| | Map-based Search | Yes | No | **Yes** | Medium | Add Geolocation to Frontend |
| **Guidance** | Symptom Checker | Yes | No | **Yes** | High | Integrate AI Agent or Rule Engine |
| | Care Pathway Routing | Yes | No | **Yes** | High | Direct to Telehealth/In-person based on triage |
| **Support** | Live Chat/Bot | Yes | No | **Yes** | High | Integrate AI Agent for L1 Support |
| | Secure Messaging | Yes | No | **Yes** | High | Add `CommunicationService` |

## 4. Gap Analysis Summary
The current platform provides a solid foundational transactional layer (Auth, Booking) but lacks the **engagement** and **guidance** layers common in modern patient portals. The lack of Mental Health specific workflows and Educational content are the most critical gaps given current healthcare trends.

## 5. Security & Compliance
Adding content and guidance features generally carries lower risk than transactional features, provided that **no PHI is collected anonymously** or that anonymous flows transition securely to authenticated flows before collecting sensitive data.

## 6. Observability
New metrics required:
- **Search Success Rate**: % of searches leading to a click.
- **Triage Completion Rate**: % of started assessments completed.
- **Content Engagement**: Usage of health library.
