import { Component, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-photo-upload',
  templateUrl: './photo-upload.component.html',
  styleUrls: ['./photo-upload.component.css']
})
export class PhotoUploadComponent {
  @Output() photoUploaded = new EventEmitter<string>();
  @Input('photoBase64Input') photoBase64Input = '';// Emit the Base64 string to parent
  fileError: string | null = null; // To store any error messages related to the file input

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length > 0) {
      const file: File = input.files[0];

      if (this.isValidFileType(file)) {
        this.convertFileToBase64(file);
      } else {
        this.fileError = 'Invalid file type. Please upload an image file.';
      }
    }
  }

  private isValidFileType(file: File): boolean {
    return file.type.startsWith('image/');
  }

  private convertFileToBase64(file: File): void {
    const reader = new FileReader();

    reader.onload = () => {
      const base64Image = reader.result as string;
      this.photoBase64Input = base64Image;
      this.photoUploaded.emit(base64Image); // Emit the base64 string to the parent
      this.fileError = null; // Clear any previous error messages
    };

    reader.onerror = () => {
      this.fileError = 'Error reading file. Please try again.';
    };

    reader.readAsDataURL(file); // This method reads the file and converts it to base64
  }
}
