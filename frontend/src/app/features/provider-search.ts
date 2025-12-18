import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

interface Doctor {
  id: number;
  name: string;
  specialty: string;
  qualification: string;
  experience: number;
  location: string;
  rating: number;
  image: string;
  availability: string;
  insuranceAccepted: boolean;
  expertise: string[];
  packages: string[];
}

@Component({
  selector: 'app-provider-search',
  imports: [CommonModule, RouterModule],
  templateUrl: './provider-search.html',
  styleUrl: './provider-search.css',
})
export class ProviderSearchComponent {
  doctors: Doctor[] = [
    {
      id: 1,
      name: 'Dr. Sarah Smith',
      specialty: 'Cardiology',
      qualification: 'MD, FACC',
      experience: 15,
      location: 'Downtown Clinic',
      rating: 4.9,
      image: 'https://img.freepik.com/free-photo/pleased-young-female-doctor-wearing-medical-robe-stethoscope-around-neck-standing-with-closed-posture_409827-254.jpg',
      availability: 'Today',
      insuranceAccepted: true,
      expertise: ['Interventional Cardiology', 'Heart Failure', 'Hypertension'],
      packages: ['Comprehensive Heart Check', 'Lipid Profile Manager']
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
      availability: 'Tomorrow',
      insuranceAccepted: true,
      expertise: ['Cosmetic Dermatology', 'Acne Treatment', 'Laser Therapy'],
      packages: ['Skin Glow Package', 'Allergy Screen']
    },
    {
      id: 3,
      name: 'Dr. Emily Chen',
      specialty: 'Pediatrics',
      qualification: 'MD (Pediatrics)',
      experience: 12,
      location: 'North Hills',
      rating: 5.0,
      image: 'https://img.freepik.com/free-photo/smiling-asian-medical-expert_1262-1832.jpg',
      availability: 'Today',
      insuranceAccepted: true,
      expertise: ['Newborn Care', 'Vaccination', 'Child Nutrition'],
      packages: ['Well Baby Checkup', 'Immunity Booster']
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
      availability: 'Next Week',
      insuranceAccepted: true,
      expertise: ['Joint Replacement', 'Sports Injuries', 'Spine Surgery'],
      packages: ['Bone Health Check', 'Arthritis Screening']
    },
  ];
}
