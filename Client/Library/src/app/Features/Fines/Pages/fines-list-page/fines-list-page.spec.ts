import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FinesListPage } from './fines-list-page';

describe('FinesListPage', () => {
  let component: FinesListPage;
  let fixture: ComponentFixture<FinesListPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FinesListPage],
    }).compileComponents();

    fixture = TestBed.createComponent(FinesListPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
