import { Routes } from '@angular/router';
import { LayoutComponent } from './core/layout';
import { LandingComponent } from './features/landing';
import { DashboardComponent } from './features/dashboard';
import { ProviderSearchComponent } from './features/provider-search';
import { HealthLibraryComponent } from './features/health-library';
import { SymptomCheckerComponent } from './features/symptom-checker';
import { SignupComponent } from './features/signup';
import { VideoConsultComponent } from './features/video-consult';
import { HealthRecordsComponent } from './features/dashboard/health-records';
import { PharmacyComponent } from './features/pharmacy';
import { DiagnosticsComponent } from './features/diagnostics';
import { DoctorProfileComponent } from './features/doctor-profile';
import { BookAppointmentComponent } from './features/book-appointment';
import { InternationalPatientsComponent } from './features/international-patients';
import { SpecialtiesComponent } from './features/specialties';
import { SpecialtyDetailComponent } from './features/specialty-detail';

export const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: LandingComponent },
      { path: 'find-doc', component: ProviderSearchComponent },
      { path: 'library', component: HealthLibraryComponent },
      { path: 'symptom-checker', component: SymptomCheckerComponent },
      { path: 'video-consult', component: VideoConsultComponent },
      { path: 'pharmacy', component: PharmacyComponent },
      { path: 'diagnostics', component: DiagnosticsComponent },
      { path: 'doctor-profile/:id', component: DoctorProfileComponent },
      { path: 'book-appointment/:id', component: BookAppointmentComponent },
      { path: 'international-patients', component: InternationalPatientsComponent },
      { path: 'centres-of-excellence', component: SpecialtiesComponent },
      { path: 'centre/:id', component: SpecialtyDetailComponent },
      { path: 'signup', component: SignupComponent },
    ]
  },
  { path: 'dashboard', component: DashboardComponent },
  { path: '**', redirectTo: '' }
];
