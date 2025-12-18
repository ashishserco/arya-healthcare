import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-health-records',
  imports: [CommonModule],
  templateUrl: './health-records.html',
  styleUrl: './health-records.css',
})
export class HealthRecordsComponent {
  records = [
    { type: 'Prescription', date: 'Oct 24, 2024', doctor: 'Dr. Sarah Smith', specialty: 'Cardiology', items: ['Atorvastatin 10mg', 'Aspirin 75mg'] },
    { type: 'Lab Report', date: 'Oct 12, 2024', doctor: 'Lab Corp', specialty: 'Blood Panel', items: ['CBC', 'Lipid Profile', 'HbA1c'] },
    { type: 'Vaccination', date: 'Sep 10, 2024', doctor: 'City Clinic', specialty: 'Immunization', items: ['Influenza Shot'] }
  ];
}
