import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthLibrary } from './health-library';

describe('HealthLibrary', () => {
  let component: HealthLibrary;
  let fixture: ComponentFixture<HealthLibrary>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HealthLibrary]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HealthLibrary);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
