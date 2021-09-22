import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subscription, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Purpose } from '../../classes/purpose';

import { UserTokenStorage } from 'src/app/classes/user-token-storage';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'JWT ' + UserTokenStorage.getInstanse().getUserToken()
  })
};

@Injectable({
  providedIn: 'root'
})
export class PurposeService {

  private purposeForChange: Purpose = null;
  baseUrl = "http://15520.pythonanywhere.com/purposes/";

  public createdPurpose: EventEmitter<any> = new EventEmitter();
  public updatedPurpose: EventEmitter<any> = new EventEmitter();

  public changingPurposeForChange: EventEmitter<any> = new EventEmitter();

  constructor(private http: HttpClient) {
    if (/localhost/.test(document.location.host)) {
      this.baseUrl = "http://127.0.0.1:8000/purposes/";
    }
  }

  changePurposeForChange(purp: Purpose) {
    if(purp == null) { this.changingPurposeForChange.emit(new Purpose(-1, "")); }
    else { this.changingPurposeForChange.emit(purp); }
  }

  createPurpose(newOp: Purpose) {
    this.createdPurpose.emit(newOp);
  }

  updatePurpose(newOp: Purpose) {
    this.updatedPurpose.emit(newOp);
  }

  getPurposes(): Observable<any> {
    let url = this.baseUrl + "?format=json";
    return this.http.get(url, httpOptions)
      .pipe(
        catchError(this.handleError('getHeroes', []))
    );
  }

  postPurpose(purpose: Purpose): Observable<Purpose> {
    return this.http.post<Purpose>(this.baseUrl, purpose, httpOptions)
    .pipe(
      catchError(this.handleError('addHero', purpose))
    );
  }

  putPurpose(purpose: Purpose): Observable<Purpose> {
    let url = this.baseUrl + purpose.id + "/";
    return this.http.put<Purpose>(url, purpose, httpOptions)
    .pipe(
      catchError(this.handleError('addHero', purpose))
    );
  }

  deletePurpose(id: number) {
    var url = this.baseUrl + id + "/";
    return this.http.delete<Purpose>(url, httpOptions);
  }

  private handleError<T> (purpose = 'purpose', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
