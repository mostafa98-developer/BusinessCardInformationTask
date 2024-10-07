import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BusinessCard } from '../models/business-card.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BusinessCardService {
  private apiUrl = `${environment.apiUrl}/api/BusinessCard`;


  constructor(private http: HttpClient) {}

  createBusinessCard(card: BusinessCard): Observable<BusinessCard> {
    return this.http.post<BusinessCard>(this.apiUrl, card);
  }

  getAllBusinessCards(filter?: any): Observable<BusinessCard[]> {
    return this.http.get<BusinessCard[]>(this.apiUrl, { params: filter });
  }

  updateBusinessCard(card: BusinessCard): Observable<BusinessCard> {
    return this.http.put<BusinessCard>(this.apiUrl, card);
  }

  deleteBusinessCard(cardId: number): Observable<BusinessCard> {
    return this.http.delete<BusinessCard>(`${this.apiUrl}/${cardId}`);
  }

  importBulk(cards: BusinessCard[]): Observable<BusinessCard[]> {
    return this.http.post<BusinessCard[]>(`${this.apiUrl}/bulk`, cards);
  }

  importBusinessCards(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post<any>(`${this.apiUrl}/import`, formData);
  }

  exportToXml(): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/export/xml`, { responseType: 'blob' });
  }

  exportToCsv(): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/export/csv`, { responseType: 'blob' });
  }
}
