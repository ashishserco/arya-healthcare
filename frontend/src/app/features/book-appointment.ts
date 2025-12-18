import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-book-appointment',
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './book-appointment.html',
  styleUrl: './book-appointment.css',
})
export class BookAppointmentComponent implements OnInit {
  doctorId: number = 0;
  selectedDate: string = '';
  selectedSlot: string = '';
  patientName: string = '';
  reason: string = '';

  slots = ['09:00 AM', '10:00 AM', '11:30 AM', '02:00 PM', '04:00 PM'];

  // Mock Doctor Data (Should come from service)
  doctorName = 'Dr. Sarah Smith';
  specialty = 'Cardiology';

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.doctorId = Number(this.route.snapshot.paramMap.get('id')); // Fix: Use paramMap instead of accessing params directly if possible, or cast correctly.
    // Ideally fetch doctor details here
  }

  confirmBooking() {
    alert(`Appointment Confirmed with ${this.doctorName} on ${this.selectedDate} at ${this.selectedSlot} for ${this.patientName}.`);
  }
}
