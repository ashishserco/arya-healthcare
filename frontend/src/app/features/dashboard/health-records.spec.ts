import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthRecords } from './health-records';

describe('HealthRecords', () => {
  let component: HealthRecords;
  let fixture: ComponentFixture<HealthRecords>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HealthRecords]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HealthRecords);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
