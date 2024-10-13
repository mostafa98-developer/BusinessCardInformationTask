// confirmation-dialog.service.ts
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class FileService  {

  constructor() {}

  downloadFile(blob: Blob, filename: string) {
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
