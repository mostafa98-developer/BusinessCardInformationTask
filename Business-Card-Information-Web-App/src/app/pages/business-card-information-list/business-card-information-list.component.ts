import { Component } from '@angular/core';
import { BusinessCard } from '../../models/business-card.model';
import { BusinessCardService } from '../../services/BusinessCardService.service';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-business-card-information-list',
  templateUrl: './business-card-information-list.component.html',
  styleUrl: './business-card-information-list.component.css'
})
export class BusinessCardInformationListComponent {
  displayedColumns: string[] = ['name', 'gender', 'dateOfBirth', 'email', 'phone', 'address', 'actions'];
  dataSource: BusinessCard[] = [];
  constructor(private businessCardService: BusinessCardService,private dialog: MatDialog) {}


  ngOnInit(): void {
    this.loadBusinessCards();
  }

  loadBusinessCards(): void {
    this.getAllCards()
  }



  getAllCards() {
    this.businessCardService.getAllBusinessCards().subscribe(cards => {
      this.dataSource = cards;
    });
  }

  updateCard(card: BusinessCard) {
    this.businessCardService.updateBusinessCard(card).subscribe(response => {
      console.log('Card updated:', response);
    });
  }

  deleteCard(cardId: number) {
    this.businessCardService.deleteBusinessCard(cardId).subscribe(response => {
      console.log('Card deleted:', response);
    });
  }

  importBulk(cards: BusinessCard[]) {
    this.businessCardService.importBulk(cards).subscribe(response => {
      console.log('Bulk import result:', response);
    });
  }

  importFile(file: File) {
    this.businessCardService.importBusinessCards(file).subscribe(response => {
      console.log('File import result:', response);
    });
  }

  exportToXml() {
    this.businessCardService.exportToXml().subscribe(blob => {
    });
  }

  exportToCsv() {
    this.businessCardService.exportToCsv().subscribe(blob => {
    });
  }
}
