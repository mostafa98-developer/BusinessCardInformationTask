import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BusinessCardInformationManageComponent } from './business-card-information-manage.component';

describe('BusinessCardInformationManageComponent', () => {
  let component: BusinessCardInformationManageComponent;
  let fixture: ComponentFixture<BusinessCardInformationManageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BusinessCardInformationManageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BusinessCardInformationManageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
