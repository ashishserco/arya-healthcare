# Security and Compliance Impact

## 1. Overview
Introducing "Patient Engagement" features (Assessments, AI Chat) increases the surface area for PHI (Protected Health Information) leakage. This document defines the controls to mitigate these risks.

## 2. Key Risks & Mitigations

### A. Anonymous Triage/Guidance
- **Risk**: A user enters sensitive symptoms in a public (unauthenticated) form, and it gets logged in cleartext WAF logs or application logs.
- **Mitigation (Architecture)**:
  - Do NOT log request bodies for `POST /triage`.
  - Use ephemeral processing: Evaluate logic -> Return Recommendation -> Discard Data.
  - If data must be saved, convert "Guest" session to "Authenticated" session immediately.

### B. AI Agent Interactions
- **Risk**: User types PHI into the chat; LLM provider retains it for training.
- **Mitigation (Contractual)**: Use Azure OpenAI "Enterprise" subscription (Zero Data Retention policy).
- **Mitigation (Technical)**: PII Redaction Middleware *before* sending to LLM.

### C. Content Personalization
- **Risk**: "Recommended for You: Diabetes Management" displayed on a shared family computer screen.
- **Mitigation**: "Privacy Mode" toggle. No specific condition titles in email subject lines (e.g., Use "You have a new health message" instead of "Your HIV test results").

## 3. Compliance Mapping

| Regulatory Standard | Requirement | Implementation |
| :--- | :--- | :--- |
| **HIPAA** | Minimum Necessary Access | Services only request data needed for their specific function. |
| **GDPR** | Right to be Forgotten | "Delete Account" must cascade deletes to `TriageSubmissions` and `ChatLogs`. |
| **WCAG 2.1** | Accessibility | All new UI components (Triage forms) must support screen readers and keyboard nav. |

## 4. Audit Strategy
- **Audit Logs**: Every *write* action (Submit Form, Book Appointment) generates a structured audit log entry: `Who`, `What`, `When`, `IP`.
- **Read Logs**: Accessing sensitive records (e.g., Mental Health History) is also logged ("Break Glass" scenarios).
