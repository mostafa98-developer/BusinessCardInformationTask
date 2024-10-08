import { Component } from '@angular/core';
import { BusinessCard } from '../../models/business-card.model';
import { BusinessCardService } from '../../services/BusinessCardService.service';
import { MatDialog } from '@angular/material/dialog';
import { BusinessCardFilter } from '../../models/business-card.filter.model';
import { ImportBusinessCardsFromFileComponent } from '../import-business-cards-from-file/import-business-cards-from-file.component';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-business-card-information-list',
  templateUrl: './business-card-information-list.component.html',
  styleUrl: './business-card-information-list.component.css'
})
export class BusinessCardInformationListComponent {

  displayedColumns: string[] = ['name', 'gender', 'dateOfBirth', 'email', 'phone', 'address', 'actions'];
  filter: BusinessCardFilter = new BusinessCardFilter();
  genderOptions = ['Male', 'Female'];
  dataSource = new MatTableDataSource<BusinessCard>([]);

  constructor(private snackBar: MatSnackBar,private businessCardService: BusinessCardService,private dialog: MatDialog) {}


  ngOnInit(): void {
    this.loadBusinessCards();
  }

  loadBusinessCards(): void {
    this.getAllCards()
  }

  Reset() {
    this.filter = {};
    this.loadBusinessCards();
  }
  importFromFile() {
    const dialogRef = this.dialog.open(ImportBusinessCardsFromFileComponent, {
      width: '600px',
      panelClass: 'custom-dialog-container',
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadBusinessCards();
      }
    });
  }

  addNew() {
    throw new Error('Method not implemented.');
  }


  getAllCards() {
    this.businessCardService.getAllBusinessCards(this.filter).subscribe(cards => {
      this.dataSource.data = [...cards];
    });
  }

  updateCard(card: BusinessCard) {
    this.businessCardService.updateBusinessCard(card).subscribe(response => {
      console.log('Card updated:', response);
    });
  }

  deleteCard(cardId: number) {
    this.businessCardService.deleteBusinessCard(cardId).subscribe(response => {
      if(response){
        this.snackBar.open('Card deleted', 'Close', {
          duration: 5000,
        });
        this.loadBusinessCards()
      }
    });
  }

  exportToXml() {
    this.businessCardService.exportToXml().subscribe(blob => {
      this.downloadFile(blob, 'business_cards.xml'); // Specify the filename
    }, error => {
      console.error('Error exporting to XML:', error);
    });
  }

  exportToCsv() {
    this.businessCardService.exportToCsv().subscribe(blob => {
      this.downloadFile(blob, 'business_cards.csv'); // Specify the filename
    }, error => {
      console.error('Error exporting to CSV:', error);
    });
  }

  private downloadFile(blob: Blob, filename: string) {
    const url = window.URL.createObjectURL(blob); // Create a URL for the Blob
    const a = document.createElement('a'); // Create an anchor element
    a.href = url; // Set the href to the Blob URL
    a.download = filename; // Set the download attribute with the desired filename
    document.body.appendChild(a); // Append the anchor to the body
    a.click(); // Programmatically click the anchor to trigger the download
    document.body.removeChild(a); // Remove the anchor from the document
    window.URL.revokeObjectURL(url); // Free up memory by revoking the Blob URL
  }
}
