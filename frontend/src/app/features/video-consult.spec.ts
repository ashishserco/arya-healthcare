import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VideoConsult } from './video-consult';

describe('VideoConsult', () => {
  let component: VideoConsult;
  let fixture: ComponentFixture<VideoConsult>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VideoConsult]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VideoConsult);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
