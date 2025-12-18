import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';

@Component({
  selector: 'app-specialty-detail',
  imports: [CommonModule, RouterModule],
  templateUrl: './specialty-detail.html',
  styleUrl: './specialty-detail.css',
})
export class SpecialtyDetailComponent implements OnInit {
  specialtyId: string | null = '';

  // Mock Data (In reality, fetch by ID)
  data: any = {
    name: 'Cardiac Sciences',
    subtitle: 'Advanced Heart Care',
    desc: 'Our Centre of Excellence in Cardiac Sciences offers comprehensive heart care, ranging from prevention to diagnosis and treatment of cardiovascular diseases. We are equipped with state-of-the-art Cath Labs and dedicated Cardiac ICUs.',
    image: 'https://via.placeholder.com/1200x400?text=Cardiac+Centre',
    procedures: [
      'Coronary Artery Bypass Graft (CABG)',
      'Angioplasty & Stenting',
      'Valve Replacement Surgery',
      'Heart Transplant',
      'Pacemaker Implantation'
    ],
    doctors: [
      { name: 'Dr. Sarah Smith', qual: 'MD, DM (Cardiology)', exp: '15 Years', img: 'https://via.placeholder.com/60' },
      { name: 'Dr. James Doe', qual: 'MCh (CTVS)', exp: '20 Years', img: 'https://via.placeholder.com/60' }
    ]
  };

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.specialtyId = params.get('id');
      // In a real app, switch (this.specialtyId) to load different data
      if (this.specialtyId === 'neurology') {
        this.data = {
          name: 'Neurosciences',
          subtitle: 'Brain & Spine Care',
          desc: 'Our Neurology department provides cutting-edge treatment for stroke, epilepsy, and spinal disorders.',
          image: 'https://via.placeholder.com/1200x400?text=Neuro+Centre',
          procedures: ['Brain Tumour Surgery', 'Spine Surgery', 'Stroke Management'],
          doctors: [{ name: 'Dr. Emily White', qual: 'MCh (Neuro)', exp: '12 Years', img: 'https://via.placeholder.com/60' }]
        };
      }
    });
  }
}
