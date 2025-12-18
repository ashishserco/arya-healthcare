import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-pharmacy',
  imports: [CommonModule],
  templateUrl: './pharmacy.html',
  styleUrl: './pharmacy.css',
})
export class PharmacyComponent {
  categories = [
    { name: 'Diabetes Care', icon: '🩸' },
    { name: 'Cardiac Care', icon: '❤️' },
    { name: 'Stomach Care', icon: '🦠' },
    { name: 'Eye Care', icon: '👁️' }
  ];

  products = [
    { name: 'Paracetamol 650mg', category: 'Fever', price: 30, discount: '10% OFF', image: 'https://via.placeholder.com/150?text=Paracetamol' },
    { name: 'Vitamin C Generic', category: 'Supplements', price: 150, discount: '15% OFF', image: 'https://via.placeholder.com/150?text=Vitamin+C' },
    { name: 'Diabetes Care Kit', category: 'Diabetes', price: 999, discount: '20% OFF', image: 'https://via.placeholder.com/150?text=Diabetes+Kit' },
    { name: 'Calcium Sandoz', category: 'Supplements', price: 350, discount: '', image: 'https://via.placeholder.com/150?text=Calcium' },
    { name: 'Dettol Antiseptic', category: 'First Aid', price: 120, discount: '5% OFF', image: 'https://via.placeholder.com/150?text=Dettol' },
    { name: 'N95 Mask (Pack of 5)', category: 'Protection', price: 400, discount: '50% OFF', image: 'https://via.placeholder.com/150?text=N95+Mask' }
  ];
}
