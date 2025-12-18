# Observability and Quality Controls

## 1. Strategy
We apply a "Symptom-Based" monitoring approach (Golden Signals) to all new services, supplemented by business-specific metrics for the new functional capabilities.

## 2. Metrics by Feature

### A. Triage & Guidance
- **Availability**: % of successful form loads.
- **Latency**: Time to load the questionnaire (Target < 200ms).
- **Business**:
  - `triage_started_total`: Counter.
  - `triage_completed_total`: Counter.
  - `crisis_trigger_total`: Counter (Alert immediately).

### B. Content Serving
- **Cache Hit Ratio**: Effectiveness of CDN/Redis for articles.
- **Search Latency**: Time to return search results.
- **Business**:
  - `article_views_total`: By Category.
  - `search_queries_total`: With "Zero Results" flag.

### C. AI Agent
- **Token Usage**: Cost monitoring.
- **Throttling**: Azure OpenAI 429 errors.
- **Quality**: User feedback (Thumbs Up/Down) on answers.

## 3. Alerts

| Alert Name | Condition | Severity | Channel |
| :--- | :--- | :--- | :--- |
| **HighCrisisRate** | > 10 crisis triggers / min | P1 | OpsGenie + Security |
| **SearchFailure** | > 50% searches fail | P2 | Slack |
| **AIQuotaExceeded** | 429 Errors > 5% | P3 | Email |

## 4. Distributed Tracing
- **Trace ID**: Propagated from Frontend -> API Gateway -> Guidance Service -> AI Agent.
- **Standard**: OpenTelemetry (OTEL).

## 5. Quality Controls
- **Unit Tests**: Logic for Triage routing (Jest/xUnit).
- **Integration Tests**: Full flow: Search -> Triage -> Match Provider.
- **Synthetics**: 5-minute heartbeat check on "Public Landing Page".
