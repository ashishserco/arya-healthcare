import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InternationalPatients } from './international-patients';

describe('InternationalPatients', () => {
  let component: InternationalPatients;
  let fixture: ComponentFixture<InternationalPatients>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InternationalPatients]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InternationalPatients);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
