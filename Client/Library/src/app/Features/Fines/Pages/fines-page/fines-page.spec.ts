import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FinesPage } from './fines-page';

describe('FinesPage', () => {
  let component: FinesPage;
  let fixture: ComponentFixture<FinesPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FinesPage],
    }).compileComponents();

    fixture = TestBed.createComponent(FinesPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
