import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-symptom-checker',
  imports: [CommonModule],
  templateUrl: './symptom-checker.html',
  styleUrl: './symptom-checker.css',
})
export class SymptomCheckerComponent {
  step = 1;
  answers: any = {};
  result: string | null = null;
  severity: 'low' | 'medium' | 'high' = 'low';

  nextStep(answer?: string) {
    if (this.step === 1 && answer) this.answers['symptom'] = answer;
    if (this.step === 2 && answer) this.answers['duration'] = answer;
    if (this.step === 3 && answer) this.answers['severity'] = answer;

    this.step++;

    if (this.step > 3) {
      this.calculateResult();
    }
  }

  calculateResult() {
    // Mock logic
    const s = this.answers['severity'];
    if (s === 'High') {
      this.result = 'Please visit an Urgent Care center immediately.';
      this.severity = 'high';
    } else if (s === 'Medium') {
      this.result = 'We recommend scheduling a video consultation today.';
      this.severity = 'medium';
    } else {
      this.result = 'Self-care at home recommended. Monitor symptoms.';
      this.severity = 'low';
    }
  }

  restart() {
    this.step = 1;
    this.answers = {};
    this.result = null;
  }
}
