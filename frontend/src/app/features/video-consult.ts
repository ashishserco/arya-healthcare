import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-video-consult',
  imports: [CommonModule],
  templateUrl: './video-consult.html',
  styleUrl: './video-consult.css',
})
export class VideoConsultComponent {
  step: 'select' | 'waiting' | 'call' = 'select';
  selectedSpecialty = '';

  specialties = [
    { name: 'General Physician', icon: '🩺', wait: '5 mins' },
    { name: 'Dermatology', icon: '🧴', wait: '15 mins' },
    { name: 'Pediatrics', icon: '👶', wait: '10 mins' },
    { name: 'Psychiatry', icon: '🧠', wait: '20 mins' },
    { name: 'Gynaecology', icon: '👩‍⚕️', wait: '12 mins' },
    { name: 'Dietitian', icon: '🥗', wait: '8 mins' }
  ];

  startConsult(specialty: string) {
    this.selectedSpecialty = specialty;
    this.step = 'waiting';
    // Simulate connection after 3 seconds
    setTimeout(() => {
      this.step = 'call';
    }, 3000);
  }

  endCall() {
    this.step = 'select';
    this.selectedSpecialty = '';
  }
}
