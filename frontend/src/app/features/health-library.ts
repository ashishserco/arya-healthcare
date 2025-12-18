import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

interface Article {
  id: number;
  title: string;
  category: string;
  excerpt: string;
  readTime: string;
}

@Component({
  selector: 'app-health-library',
  imports: [CommonModule],
  templateUrl: './health-library.html',
  styleUrl: './health-library.css',
})
export class HealthLibraryComponent {
  categories = ['Healthy Living', 'Mental Health', 'Conditions', 'Pregnancy', 'Senior Care'];

  featuredArticles: Article[] = [
    { id: 1, title: 'Managing Stress in Modern Life', category: 'Mental Health', excerpt: 'Practical tips for reducing daily stress and improving mindfulness.', readTime: '5 min read' },
    { id: 2, title: 'Understanding Heart Health', category: 'Conditions', excerpt: 'Key indicators of heart health and how to maintain cardiovascular fitness.', readTime: '7 min read' },
    { id: 3, title: 'Nutrition for Families', category: 'Healthy Living', excerpt: 'Balanced meal planning for busy families on a budget.', readTime: '4 min read' },
    { id: 4, title: 'The Importance of Sleep', category: 'Healthy Living', excerpt: 'Why sleep matters and how to get better quality rest.', readTime: '6 min read' },
  ];
}
