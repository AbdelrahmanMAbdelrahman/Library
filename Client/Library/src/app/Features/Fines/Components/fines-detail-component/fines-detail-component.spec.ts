import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FinesDetailComponent } from './fines-detail-component';

describe('FinesDetailComponent', () => {
  let component: FinesDetailComponent;
  let fixture: ComponentFixture<FinesDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FinesDetailComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(FinesDetailComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
