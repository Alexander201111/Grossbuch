import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subscription, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Currency } from '../../classes/currency';

import { UserTokenStorage } from 'src/app/classes/user-token-storage';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'JWT ' + UserTokenStorage.getInstanse().getUserToken()
  })
};

@Injectable({ providedIn: 'root' })
export class CurrencyService {

  currencyForChange: Currency = null;
  baseUrl = "http://15520.pythonanywhere.com/currencies/";

  constructor(private http: HttpClient) {
    if (/localhost/.test(document.location.host)) {
      this.baseUrl = "http://127.0.0.1:8000/currencies/";
    }
  }

  getCurrencies(): Observable<any> {
    let url = this.baseUrl + "?format=json";
    return this.http.get(url, httpOptions)
      .pipe(
        catchError(this.handleError('getCurrency', []))
      );
  }

  postCurrency(currency: Currency): Observable<Currency> {
    return this.http.post<Currency>(this.baseUrl, currency, httpOptions)
      .pipe(
        catchError(this.handleError('addHero', currency))
      );
  }

  putCurrency(currency: Currency): Observable<Currency> {
    let url = this.baseUrl + currency.id + "/";
    return this.http.put<Currency>(url, currency, httpOptions)
      .pipe(
        catchError(this.handleError('addHero', currency))
      );
  }

  deleteCurrency(id: number) {
    var url = this.baseUrl + id + "/";
    return this.http.delete<Currency>(url, httpOptions);
  }

  private handleError<T>(currency = 'currency', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
