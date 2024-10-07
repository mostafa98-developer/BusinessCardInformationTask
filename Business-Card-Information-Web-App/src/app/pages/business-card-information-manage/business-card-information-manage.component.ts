import { Component } from '@angular/core';
import { BusinessCard } from '../../models/business-card.model';
import { BusinessCardService } from '../../services/BusinessCardService.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-business-card-information-manage',
  templateUrl: './business-card-information-manage.component.html',
  styleUrl: './business-card-information-manage.component.css'
})
export class BusinessCardInformationManageComponent {


  businessCardForm!: FormGroup;
  businessCard!: BusinessCard;

  constructor(private fb: FormBuilder,private businessCardService: BusinessCardService) {
    this.businessCardForm = this.fb.group({
      name: ['', Validators.required],
      gender: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      address: ['', Validators.required],
      photoBase64: ['']  // Optional, can be added later
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.businessCardForm.valid) {
      this.businessCard = this.businessCardForm.value;
    }
  }

  save(): void {
    this.businessCardService.createBusinessCard(this.businessCard).subscribe(response => {
      console.log('Card created:', response);
    });
  }
}
