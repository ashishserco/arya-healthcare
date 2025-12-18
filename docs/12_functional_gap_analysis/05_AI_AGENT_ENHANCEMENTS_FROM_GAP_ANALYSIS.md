# AI Agent Enhancements from Gap Analysis

## 1. Role of AI in Filling Gaps
The Gap Analysis identified critical needs in **Guidance**, **Education**, and **Support**. The "AI Agent" is not just a chatbot; it is an orchestration layer that connects patients to these capabilities dynamically.

## 2. Specific AI Capabilities

### A. Health Education Q&A
- **Gap**: Patients can't find relevant articles easily.
- **AI Solution**: RAG (Retrieval-Augmented Generation) pipeline over the `ContentService` database.
- **Workflow**:
  1. Patient asks "What are symptoms of mild flu?"
  2. Agent searches vector index of health articles.
  3. Agent summarizes top 3 articles and provides links.
- **Safety**: System prompt restricts answers to *only* provided context (grounded).

### B. Mental Health Soft-Triage
- **Gap**: Determining if a patient needs immediate help vs. routine care.
- **AI Solution**: Sentiment Analysis & Keyword Spotting.
- **Workflow**:
  1. Patient input: "I'm feeling really hopless."
  2. AI detects "Critical" sentiment.
  3. AI bypasses standard flow -> Immediate "Crisis Resource" card shown + optional human handoff.

### C. Appointment Scheduling Assistant
- **Gap**: Complex search filters (insurance + location + specialty).
- **AI Solution**: Natural Language to SQL/Search Query.
- **Workflow**:
  1. Patient: "Find me a cardiologist near downtown who takes BlueCross."
  2. AI extracts entities: {Specialty: Cardiologist, Location: Downtown, Insurance: BCBS}.
  3. Calls `ProviderService` API with structured query.

### D. Administrative Support
- **Gap**: Call center overload for "Reset password" or "Where is the clinic?"
- **AI Solution**: FAQ Auto-responder.

## 3. Integration Architecture
- **Service**: Azure OpenAI (GPT-4o).
- **Orchestrator**: Semantic Kernel or LangChain.
- **Context Window**: Injected with simplified patient profile (Age, Gender - *No Name/MRN*).

## 4. Guardrails & Safety
- **System Prompt**: "You are a helpful healthcare assistant. You DO NOT provide medical diagnoses. If a user asks for medical advice, direct them to the 'Get Care' feature."
- **PHI Scrubbing**: All user input passed through a PII scrubber before reaching the LLM.
