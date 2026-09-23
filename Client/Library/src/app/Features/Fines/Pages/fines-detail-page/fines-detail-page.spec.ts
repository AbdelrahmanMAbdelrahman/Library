import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FinesDetailPage } from './fines-detail-page';

describe('FinesDetailPage', () => {
  let component: FinesDetailPage;
  let fixture: ComponentFixture<FinesDetailPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FinesDetailPage],
    }).compileComponents();

    fixture = TestBed.createComponent(FinesDetailPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
