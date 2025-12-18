import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-international-patients',
  imports: [CommonModule, RouterModule],
  templateUrl: './international-patients.html',
  styleUrl: './international-patients.css',
})
export class InternationalPatientsComponent {
  services = [
    { title: 'Visa Assistance', icon: '🛂', desc: 'We provide invitation letters and support for medical visas.' },
    { title: 'Airport Transfers', icon: '✈️', desc: 'Complimentary pick-up and drop-off services for patients.' },
    { title: 'Language Interpretation', icon: '🗣️', desc: 'Dedicated interpreters for Arabic, French, Russian, and more.' },
    { title: 'Accommodation', icon: '🏨', desc: 'Partner hotels and guest houses near the hospital.' },
    { title: 'Currency Exchange', icon: '💱', desc: 'Hassle-free currency exchange services within the premises.' },
    { title: 'Sim Card Assistance', icon: '📱', desc: 'Local SIM cards provided upon arrival for easy connectivity.' }
  ];

  steps = [
    { num: '01', title: 'Get in Touch', desc: 'Send us your medical reports via email or WhatsApp.' },
    { num: '02', title: 'Treatment Plan', desc: 'Receive a detailed treatment plan and cost estimate within 24 hours.' },
    { num: '03', title: 'Travel Arrangements', desc: 'We assist with visa, tickets, and accommodation booking.' },
    { num: '04', title: 'Arrival & Treatment', desc: 'Airport pickup and admission to the hospital for treatment.' },
    { num: '05', title: 'Recovery & Fly Back', desc: 'Post-discharge follow-up and safe return to your home country.' }
  ];

  testimonials = [
    { name: 'Ahmed Al-Fayed', country: 'UAE', quote: 'The care I received was world-class. The interpreters made everything so easy.', image: 'https://via.placeholder.com/60' },
    { name: 'Sarah Jenkins', country: 'UK', quote: 'Fraction of the cost compared to home, but better facilities. Highly recommended.', image: 'https://via.placeholder.com/60' },
    { name: 'Dmitry Volkov', country: 'Russia', quote: 'Professional doctors and excellent hospitality. Thank you Arya Health.', image: 'https://via.placeholder.com/60' }
  ];
}
