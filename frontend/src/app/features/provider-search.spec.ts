import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProviderSearch } from './provider-search';

describe('ProviderSearch', () => {
  let component: ProviderSearch;
  let fixture: ComponentFixture<ProviderSearch>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProviderSearch]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProviderSearch);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
