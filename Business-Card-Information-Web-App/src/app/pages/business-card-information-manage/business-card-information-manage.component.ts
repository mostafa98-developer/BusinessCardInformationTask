import { Component } from '@angular/core';
import { BusinessCard } from '../../models/business-card.model';
import { BusinessCardService } from '../../services/BusinessCardService.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NotificationService } from '../../services/helper/notification.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ServiceResult } from '../../models/common/serviceResult.common';

@Component({
  selector: 'app-business-card-information-manage',
  templateUrl: './business-card-information-manage.component.html',
  styleUrl: './business-card-information-manage.component.css'
})
export class BusinessCardInformationManageComponent {


  businessCardForm: FormGroup;
  businessCard: BusinessCard = new BusinessCard();
  personalPhotoBase64: string | null = null
  id: any;

  constructor(private fb: FormBuilder,private businessCardService: BusinessCardService,
    private notificationService: NotificationService,private router: Router,private route: ActivatedRoute) {
    this.businessCardForm = this.fb.group({
      name: ['', Validators.required],
      gender: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      address: ['', Validators.required],
      photoBase64: ['']  // Optional, can be added later
    });

    this.route.params.subscribe(params => {
      if (params['id']) {
        this.id = params['id']
        this.businessCardService.getCardById(this.id ).subscribe({
          next: (result: ServiceResult<BusinessCard>) => {
            if (!result.hasErrors && result.data) {
              this.businessCardForm.patchValue(result.data);
            }
          },
          error: () => {
            this.notificationService.showError('Error occurred while fetching the business card.');
          },
        });
      }
    });
  }

  ngOnInit(): void {

  }

  onSubmit(): void {
    if (this.businessCardForm.valid) {
      this.businessCard = this.businessCardForm.value;
    }
  }

  save(): void {
    if (this.id > 0) {
      this.update();
    } else {
      this.add();
    }

  }

  private add() {
    this.businessCardService.createBusinessCard(this.businessCard).subscribe(response => {
      if (!response.hasErrors) {
        this.notificationService.showSuccess('Saved successfully!');
        this.router.navigateByUrl('pages/business-bard-information-list');
      }
    });
  }

  private update() {
    this.businessCard.id = this.id;
    this.businessCardService.updateBusinessCard(this.businessCard).subscribe(response => {
      if (!response.hasErrors) {
        this.notificationService.showSuccess('Saved successfully!');
        this.router.navigateByUrl('pages/business-bard-information-list');
      }
    });
  }

  onPhotoUploaded(base64: string): void {
    this.businessCardForm.get('photoBase64')?.patchValue(base64);
  }
}
