import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpecialtyDetail } from './specialty-detail';

describe('SpecialtyDetail', () => {
  let component: SpecialtyDetail;
  let fixture: ComponentFixture<SpecialtyDetail>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SpecialtyDetail]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SpecialtyDetail);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
