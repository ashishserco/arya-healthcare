import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-diagnostics',
  imports: [CommonModule],
  templateUrl: './diagnostics.html',
  styleUrl: './diagnostics.css',
})
export class DiagnosticsComponent {
  categories = [
    {
      name: 'Popular Packages',
      open: true,
      items: ['Full Body Checkup', 'Diabetes Screening', 'Vitamin Profile']
    },
    {
      name: 'Condition Based',
      open: false,
      items: ['Heart', 'Thyroid', 'Liver', 'Kidney', 'Bone Health']
    },
    {
      name: 'Women Health',
      open: false,
      items: ['PCOD Profile', 'Pregnancy', 'Mammography']
    }
  ];

  selectedCategory = 'Full Body Checkup';

  toggleCategory(cat: any) {
    cat.open = !cat.open;
  }

  packages = [
    { name: 'Comprehensive Full Body Checkup', tests: 85, price: 1499, oldPrice: 2999, discount: '50% OFF', includes: ['Thyroid', 'Liver', 'Kidney', 'CBC', 'Sugar'], icon: '🛡️' },
    { name: 'Diabetes Screening Pro', tests: 45, price: 699, oldPrice: 1200, discount: '40% OFF', includes: ['HbA1c', 'Fasting Sugar', 'Urine Routine'], icon: '🩸' },
    { name: 'Advanced Heart Care', tests: 55, price: 1999, oldPrice: 3500, discount: '43% OFF', includes: ['Lipid Profile', 'ECG', 'Cardiac Risk Markers'], icon: '❤️' },
    { name: 'Vitamin Deficiency Check', tests: 10, price: 899, oldPrice: 1500, discount: '40% OFF', includes: ['Vitamin D', 'Vitamin B12', 'Calcium', 'Iron'], icon: '💊' },
    { name: 'Thyroid Profile Total', tests: 3, price: 399, oldPrice: 800, discount: '50% OFF', includes: ['T3', 'T4', 'TSH'], icon: '🦋' }
  ];

  labTests = [
    { name: 'CBC (Complete Blood Count)', price: 299 },
    { name: 'Lipid Profile', price: 599 },
    { name: 'HbA1c (Glycosylated Hemoglobin)', price: 450 },
    { name: 'TSH (Thyroid Stimulating Hormone)', price: 199 },
    { name: 'Liver Function Test (LFT)', price: 799 },
    { name: 'Kidney Function Test (KFT)', price: 899 },
    { name: 'Vitamin D Total', price: 999 },
    { name: 'Vitamin B12', price: 899 }
  ];
}
