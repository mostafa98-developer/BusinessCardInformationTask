import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { BusinessCard } from '../../models/business-card.model';
import { BusinessCardService } from '../../services/BusinessCardService.service';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-import-business-cards-from-file',
  templateUrl: './import-business-cards-from-file.component.html',
  styleUrl: './import-business-cards-from-file.component.css'
})
export class ImportBusinessCardsFromFileComponent {

  displayedColumns: string[] = ['name', 'gender', 'dateOfBirth', 'email', 'phone', 'address'];
  dataSource = new MatTableDataSource<BusinessCard>([]);
  selectedFile: File | null = null;
  importError: string | null = null;

  constructor(private snackBar: MatSnackBar,private businessCardService: BusinessCardService,public dialogRef: MatDialogRef<ImportBusinessCardsFromFileComponent>) {}

  onFileChange(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
      this.importError = null; // Reset error message
    }
  }

  importBusinessCards() {
    if (!this.selectedFile) {
      this.importError = 'Please select a file to import.';
      return;
    }

    if(['text/csv','text/xml'].includes(this.selectedFile.type) ){
      this.businessCardService.importBusinessCards(this.selectedFile).subscribe((result) => {
        if(!result.hasErrors && result.data) {
          this.dataSource.data = [...result.data];
        }
      });
    } else {
      this.businessCardService.QRCodeReader(this.selectedFile).subscribe((result) => {
        if(!result.hasErrors && result.data) {
          this.dataSource.data = [result.data];
        }
      });
    }


  }

  save() {
    this.businessCardService.importBulk(this.dataSource.data).subscribe(result => {
      if(result.isSucceed){
        this.dialogRef.close();
      }
    })

  }
}
