import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BusinessCardInformationListComponent } from './business-card-information-list.component';

describe('BusinessCardInformationListComponent', () => {
  let component: BusinessCardInformationListComponent;
  let fixture: ComponentFixture<BusinessCardInformationListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BusinessCardInformationListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BusinessCardInformationListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
