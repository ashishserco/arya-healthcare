import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-specialties',
  imports: [CommonModule, RouterModule],
  templateUrl: './specialties.html',
  styleUrl: './specialties.css',
})
export class SpecialtiesComponent {
  departments = [
    { id: 'cardiology', name: 'Cardiac Sciences', icon: '❤️', desc: 'Comprehensive heart care including angioplasty, bypass surgery & rehabilitation.', image: 'https://via.placeholder.com/400x200?text=Cardiology' },
    { id: 'neurology', name: 'Neurosciences', icon: '🧠', desc: 'Advanced care for brain and spine disorders with robotic neurosurgery.', image: 'https://via.placeholder.com/400x200?text=Neurology' },
    { id: 'orthopedics', name: 'Orthopaedics', icon: '🦴', desc: 'Joint replacement, trauma care, and sports medicine centres.', image: 'https://via.placeholder.com/400x200?text=Orthopedics' },
    { id: 'oncology', name: 'Oncology', icon: '🎗️', desc: 'Integrated cancer care with medical, surgical, and radiation oncology.', image: 'https://via.placeholder.com/400x200?text=Oncology' },
    { id: 'gastro', name: 'Gastro Sciences', icon: '🩺', desc: 'Treatment for liver, pancreas, and digestive tract disorders.', image: 'https://via.placeholder.com/400x200?text=Gastroenterology' },
    { id: 'transplant', name: 'Organ Transplant', icon: '🏥', desc: 'State-of-the-art liver, kidney, and heart transplant programmes.', image: 'https://via.placeholder.com/400x200?text=Transplants' },
  ];
}
