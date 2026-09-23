import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FinesSearchComponent } from './fines-search-component';

describe('FinesSearchComponent', () => {
  let component: FinesSearchComponent;
  let fixture: ComponentFixture<FinesSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FinesSearchComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(FinesSearchComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
