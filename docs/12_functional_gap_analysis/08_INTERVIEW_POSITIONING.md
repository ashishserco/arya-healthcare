# Interview Positioning

## 1. The Narrative
"I didn't just build a CRUD app. I acted as an architect to analyze the healthcare landscape, identified gaps in patient engagement vs. pure transaction, and implemented a modern, microservice-based solution to fill those gaps safely."

## 2. Key Talking Points

### "Why did you add a Guidance Service?"
- **Answer**: "I noticed that standard booking systems assume patients *know* what they need. Real healthcare data shows patients need *guidance*. I decoupled the logic into a separate service to allow for complex triage rules without polluting the booking domain."

### "How did you handle Security with AI?"
- **Answer**: "I was very conscious of HIPAA/GDPR. I ensured we used a zero-retention LLM endpoint and implemented a PII redaction layer before the prompt. The AI is used for *navigation*, not medical diagnosis."

### "How does this compare to real enterprise systems?"
- **Answer**: "It mirrors the 'Digital Front Door' strategy used by systems like Mayo Clinic. It moves from reactive care (sick -> book) to proactive care (symptom check -> education -> book)."

## 3. Showing "Seniority"
- Mention **Trade-offs**: "I chose eventual consistency for the Content search index because article updates don't need to be instant."
- Mention **Failure Modes**: "If the AI service is down, the UI gracefully degrades to a standard keyword search."
