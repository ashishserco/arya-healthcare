import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';

@Component({
  selector: 'app-doctor-profile',
  imports: [CommonModule, RouterModule],
  templateUrl: './doctor-profile.html',
  styleUrl: './doctor-profile.css',
})
export class DoctorProfileComponent implements OnInit {
  doctor: any;

  // Mock Database
  private doctors = [
    {
      id: 1,
      name: 'Dr. Sarah Smith',
      specialty: 'Cardiology',
      qualification: 'MD, FACC',
      experience: 15,
      location: 'Downtown Clinic',
      rating: 4.9,
      image: 'https://img.freepik.com/free-photo/pleased-young-female-doctor-wearing-medical-robe-stethoscope-around-neck-standing-with-closed-posture_409827-254.jpg',
      about: 'Dr. Sarah Smith is a renowned Cardiologist with over 15 years of experience in treating complex heart conditions. She specializes in Interventional Cardiology and Heart Failure management.',
      availability: ['Mon', 'Wed', 'Fri'],
      reviews: [
        { user: 'John Doe', rating: 5, comment: 'Excellent doctor, very patient.' },
        { user: 'Jane Smith', rating: 4.5, comment: 'Great experience but wait time was long.' }
      ]
    },
    {
      id: 2,
      name: 'Dr. James Wilson',
      specialty: 'Dermatology',
      qualification: 'MBBS, DDVL',
      experience: 8,
      location: 'Westside Center',
      rating: 4.8,
      image: 'https://img.freepik.com/free-photo/doctor-offering-medical-advice_23-2147796535.jpg',
      about: 'Dr. James Wilson is an expert in cosmetic and clinical dermatology, helping patients achieve healthy, glowing skin.',
      availability: ['Tue', 'Thu'],
      reviews: [
        { user: 'Alice', rating: 5, comment: 'Cleared my acne in 2 weeks!' }
      ]
    },
    // Fallback for others
    {
      id: 3,
      name: 'Dr. Emily Chen',
      specialty: 'Pediatrics',
      qualification: 'MD (Pediatrics)',
      experience: 12,
      location: 'North Hills',
      rating: 5.0,
      image: 'https://img.freepik.com/free-photo/smiling-asian-medical-expert_1262-1832.jpg',
      about: 'Dr. Emily Chen loves kids and provides comprehensive care from newborns to adolescents.',
      availability: ['Mon', 'Tue', 'Fri'],
      reviews: []
    },
    {
      id: 4,
      name: 'Dr. Michael Brown',
      specialty: 'Orthopedics',
      qualification: 'MS (Ortho)',
      experience: 20,
      location: 'Downtown Clinic',
      rating: 4.7,
      image: 'https://img.freepik.com/free-photo/portrait-successful-mid-adult-doctor-with-crossed-arms_1262-12865.jpg',
      about: 'Dr. Michael Brown specializes in sports injuries and joint replacement surgeries with a high success rate.',
      availability: ['Wed', 'Thu'],
      reviews: []
    }
  ];

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.doctor = this.doctors.find(d => d.id === id) || this.doctors[0];
  }
}
