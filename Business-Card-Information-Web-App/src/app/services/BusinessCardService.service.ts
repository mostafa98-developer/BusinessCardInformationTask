import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, Observable, of } from 'rxjs';
import { BusinessCard } from '../models/business-card.model';
import { environment } from '../../environments/environment';
import { BusinessCardFilter } from '../models/business-card.filter.model';
import { toHttpParams } from './helper/toHttpParams.helper';
import { ServiceResult } from '../models/common/serviceResult.common';
import { ResultCode } from '../shared/Enums/ResultCode.enums';
import { Error } from '../models/common/error.common';

@Injectable({
  providedIn: 'root'
})
export class BusinessCardService {
  private apiUrl = `${environment.apiUrl}/api/BusinessCard`;


  constructor(private http: HttpClient) {}

  createBusinessCard(card: BusinessCard): Observable<ServiceResult<BusinessCard>> {
    return this.http.post<BusinessCard>(this.apiUrl, card).pipe(
      map((response) => new ServiceResult<BusinessCard>(ResultCode.Ok, response)),
      catchError((error) => this.handleError<BusinessCard>(error))
    );
  }

  getAllBusinessCards(filter?: BusinessCardFilter): Observable<ServiceResult<BusinessCard[]>> {
    const params = toHttpParams(filter); // Use the dynamic converter
    return this.http.get<BusinessCard[]>(this.apiUrl, { params }).pipe(
      map((response) => new ServiceResult<BusinessCard[]>(ResultCode.Ok, response)),
      catchError((error) => this.handleError<BusinessCard[]>(error))
    );
  }

  getCardById(cardId: number): Observable<ServiceResult<BusinessCard>> {
    return this.http.get<BusinessCard>(`${this.apiUrl}/${cardId}`).pipe(
      map((response) => new ServiceResult<BusinessCard>(ResultCode.Ok, response)),
      catchError((error) => this.handleError<BusinessCard>(error))
    );
  }

  updateBusinessCard(card: BusinessCard): Observable<ServiceResult<BusinessCard>> {
    return this.http.put<BusinessCard>(this.apiUrl, card).pipe(
      map((response) => new ServiceResult<BusinessCard>(ResultCode.Ok, response)),
      catchError((error) => this.handleError<BusinessCard>(error))
    );
  }

  deleteBusinessCard(cardId: number): Observable<ServiceResult<BusinessCard>> {
    return this.http.delete<BusinessCard>(`${this.apiUrl}/${cardId}`).pipe(
      map((response) => new ServiceResult<BusinessCard>(ResultCode.Ok, response)),
      catchError((error) => this.handleError<BusinessCard>(error))
    );
  }

  importBulk(cards: BusinessCard[]): Observable<ServiceResult<BusinessCard[]>> {
    return this.http.post<BusinessCard[]>(`${this.apiUrl}/bulk`, cards).pipe(
      map((response) => new ServiceResult<BusinessCard[]>(ResultCode.Ok, response)),
      catchError((error) => this.handleError<BusinessCard[]>(error))
    );
  }

  importBusinessCards(file: File): Observable<ServiceResult<any>> {
    const formData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post<any>(`${this.apiUrl}/import`, formData).pipe(
      map((response) => new ServiceResult<any>(ResultCode.Ok, response)),
      catchError((error) => this.handleError<any>(error))
    );
  }

  exportToXml(): Observable<ServiceResult<Blob>> {
    return this.http.get(`${this.apiUrl}/export/xml`, { responseType: 'blob' }).pipe(
      map((response) => new ServiceResult<Blob>(ResultCode.Ok, response)),
      catchError((error) => this.handleError<Blob>(error))
    );
  }

  exportToCsv(): Observable<ServiceResult<Blob>> {
    return this.http.get(`${this.apiUrl}/export/csv`, { responseType: 'blob' }).pipe(
      map((response) => new ServiceResult<Blob>(ResultCode.Ok, response)),
      catchError((error) => this.handleError<Blob>(error))
    );
  }

  private handleError<T>(error: any): Observable<ServiceResult<T>> {
    const result = new ServiceResult<T>(ResultCode.Ok); // Use appropriate ResultCode based on error
    let message = 'An unknown error occurred';

    // Check if error has a specific structure
    if (error.error) {
      if (typeof error.error === 'string') {
        message = error.error; // Assume it's a string error message
      } else if (error.error.message) {
        message = error.error.message; // For more structured error response
      } else {
        message = 'Unexpected server response';
      }
      result.addError(new Error('ServerError', message));
    } else {
      message = error.message || 'Unknown error occurred';
      result.addError(new Error('ServerError', message));
    }

    return of(result);
  }
}
