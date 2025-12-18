# New Microservices Introduced

## 1. Overview
To support the new capabilities (Education, Guidance, Richer Search) without bloating the existing `PatientService` or `AppointmentService`, we are introducing specialized microservices.

## 2. New Services

### A. Guidance Service (`guidance-service`)
- **Responsibility**: Manages triage flows, questionnaires (forms), and "next best action" logic.
- **Why New?**: Separation of concerns. Booking logic shouldn't know about clinical questionnaires.
- **Data Ownership**: `FormDefinitions`, `FormSubmissions` (Transient or Persisted).
- **APIs**:
  - `GET /forms/{id}` (Public/Protected)
  - `POST /forms/{id}/submit` (Protected)
  - `POST /triage/evaluate` (Public - Anonymous Triage)

### B. Content Service (`content-service`)
- **Responsibility**: Manages health education content metadata and serving.
- **Why New?**: Content lifecycles are different from patient data. Allows independent scaling for high-read traffic.
- **Data Ownership**: `Articles`, `Categories`, `Tags`.
- **APIs**:
  - `GET /articles` (Search)
  - `GET /articles/{id}`
  - `POST /admin/articles` (Internal)

### C. Notification/Communication Service (`notification-service`)
- **Responsibility**: Centralized handling of emails, SMS, and secure platform messages.
- **Why New?**: To abstract away 3rd party providers (SendGrid, Twilio) from core business logic.
- **Data Ownership**: `MessageTemplates`, `NotificationLogs`.
- **APIs**:
  - `POST /send` (Internal)
  - `GET /messages` (Patient Inbox)

## 3. Updates to Existing Services

### Appointment Service
- **Change**: Add `ReferralId` or `TriageId` to booking request to link booking to a prior guidance flow.

### Provider Service
- **Change**: Extend `Provider` entity with `Bio`, `VideoUrl`, `Languages`, `Specialties`.
- **Change**: Add `Search` optimization (likely requires Read Replica or rudimentary search index).

## 4. Security Boundaries
- **Guidance Service**: High sensitivity if storing answers. Anonymous flows must utilize ephemeral storage or strictly tokenized sessions.
- **Content Service**: Low sensitivity (mostly public data).
- **Notification Service**: High sensitivity (PII/PHI in messages). Strict auditing required.

## 5. Observability
- **New Service Dashboards**: Standard RED metrics (Rate, Errors, Duration).
- **Business Logic Metrics**: "Form Abandonment Rate" in Guidance Service.
