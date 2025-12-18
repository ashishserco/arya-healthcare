import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-landing',
  imports: [CommonModule, RouterLink],
  templateUrl: './landing.html',
  styleUrl: './landing.css',
})
export class LandingComponent {
  topDoctors = [
    { name: 'Dr. Sarah Smith', specialty: 'Cardiology', exp: '15+ Years', img: 'https://images.unsplash.com/photo-1559839734-2b71ea197ec2?auto=format&fit=crop&w=300&q=80' },
    { name: 'Dr. James Doe', specialty: 'Neuro Surgery', exp: '20+ Years', img: 'https://images.unsplash.com/photo-1612349317150-141d211a2cd9?auto=format&fit=crop&w=300&q=80' },
    { name: 'Dr. Emily White', specialty: 'Pediatrics', exp: '12+ Years', img: 'https://images.unsplash.com/photo-1594824476967-48c8b964273f?auto=format&fit=crop&w=300&q=80' },
    { name: 'Dr. Michael Brown', specialty: 'Orthopedics', exp: '18+ Years', img: 'https://images.unsplash.com/photo-1622253692010-333f2da6031d?auto=format&fit=crop&w=300&q=80' }
  ];
}
