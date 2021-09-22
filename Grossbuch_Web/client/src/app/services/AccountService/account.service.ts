import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subscription, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Account } from '../../classes/account';

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
export class AccountService {

  accountForChange: Account = null;
  baseUrl = "http://15520.pythonanywhere.com/accounts/";

  public createdAccount: EventEmitter<any> = new EventEmitter();
  public updatedAccount: EventEmitter<any> = new EventEmitter();

  public changingAccountForChange: EventEmitter<any> = new EventEmitter();
  public changingAccountForAdd: EventEmitter<any> = new EventEmitter();

  constructor(private http: HttpClient) {
    if (/localhost/.test(document.location.host)) {
      this.baseUrl = "http://127.0.0.1:8000/accounts/";
    }
  }

  changeAccountForChange(purp: Account) {
    if(purp == null) { this.changingAccountForChange.emit(new Account(-1, "", 0)); }
    else { this.changingAccountForChange.emit(purp); }
  }

  changeAccountForAdd(purp: Account) {
    if(purp == null) { this.changingAccountForAdd.emit(new Account(-1, "", 0)); }
    else { this.changingAccountForAdd.emit(purp); }
  }

  createAccount(newOp: Account) {
    this.createdAccount.emit(newOp);
  }

  updateAccount(newOp: Account) {
    this.updatedAccount.emit(newOp);
  }

  getAccounts(): Observable<any> {
    let url = this.baseUrl + "?format=json";
    return this.http.get(url, httpOptions)
      .pipe(
        catchError(this.handleError('getHeroes', []))
    );
  }

  postAccount(account: Account): Observable<Account> {
    return this.http.post<Account>(this.baseUrl, account, httpOptions)
    .pipe(
      catchError(this.handleError('addHero', account))
    );
  }

  putAccount(account: Account): Observable<Account> {
    let url = this.baseUrl + account.id + "/";
    return this.http.put<Account>(url, account, httpOptions)
    .pipe(
      catchError(this.handleError('addHero', account))
    );
  }

  deleteAccount(id: number) {
    var url = this.baseUrl + id + "/";
    return this.http.delete<Account>(url, httpOptions);
  }

  private handleError<T> (account = 'account', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
