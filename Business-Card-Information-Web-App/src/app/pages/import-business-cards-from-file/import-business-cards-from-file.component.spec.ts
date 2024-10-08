import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportBusinessCardsFromFileComponent } from './import-business-cards-from-file.component';

describe('ImportBusinessCardsFromFileComponent', () => {
  let component: ImportBusinessCardsFromFileComponent;
  let fixture: ComponentFixture<ImportBusinessCardsFromFileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ImportBusinessCardsFromFileComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ImportBusinessCardsFromFileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
