import { Component } from '@angular/core';
import { BusinessCard } from '../../models/business-card.model';
import { BusinessCardService } from '../../services/BusinessCardService.service';
import { MatDialog } from '@angular/material/dialog';
import { BusinessCardFilter } from '../../models/business-card.filter.model';
import { ImportBusinessCardsFromFileComponent } from '../import-business-cards-from-file/import-business-cards-from-file.component';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { NotificationService } from '../../services/helper/notification.service';

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

  BusinessCards: BusinessCard[] = [];

  constructor(private snackBar: MatSnackBar,
    private businessCardService: BusinessCardService,
    private dialog: MatDialog,
    private router: Router,
    private notificationService: NotificationService) {}


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
    this.router.navigateByUrl('pages/business-bard-information-manage');
  }


  getAllCards() {
    this.businessCardService.getAllBusinessCards(this.filter).subscribe(result => {
      if(!result.hasErrors && result.data) {
        this.dataSource.data = [...result.data];
      } else {
        this.notificationService.showError(result.errors.map( e => e.extraMessage + '\n').toString());
      }

    });
  }

  updateCard(card: BusinessCard) {
    this.router.navigateByUrl('pages/business-bard-information-manage/'+card.id);
  }

  deleteCard(cardId: number) {
    this.notificationService.confirm('Are you sure you want to delete this item?')
      .subscribe(result => {
        if (result) {
          this.businessCardService.deleteBusinessCard(cardId).subscribe(response => {
            if(!response.hasErrors){
              this.notificationService.showSuccess('Card deleted successfully!');
              this.loadBusinessCards()
            } else {
              this.notificationService.showError(response.errors.map( e => e.extraMessage + '\n').toString());
            }
          });
        } else {
          this.notificationService.showError('Something went wrong!');
        }
      });


  }

  exportToXml() {
    this.businessCardService.exportToXml().subscribe(result => {
      if(!result.hasErrors && result.data) {
        this.downloadFile(result.data, 'business_cards.xml'); // Specify the filename
      } else {
        this.notificationService.showError(result.errors.map( e => e.extraMessage + '\n').toString());
      }
    });
  }

  exportToCsv() {
    this.businessCardService.exportToCsv().subscribe(result => {
      if(!result.hasErrors && result.data) {
        this.downloadFile(result.data, 'business_cards.csv'); // Specify the filename
      } else {
        this.notificationService.showError(result.errors.map( e => e.extraMessage + '\n').toString());
      }
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
