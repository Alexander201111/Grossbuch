import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subscription, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Operation } from '../../classes/operation';

import { UserTokenStorage } from 'src/app/classes/user-token-storage';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'JWT ' + UserTokenStorage.getInstanse().getUserToken()
  })
};

@Injectable({ providedIn: 'root' })
export class OperationService {

  baseUrl = "http://15520.pythonanywhere.com/operations/";

  public operationForChange: Operation = null;

  public createdOperation: EventEmitter<any> = new EventEmitter();
  public updatedOperation: EventEmitter<any> = new EventEmitter();

  constructor(private http: HttpClient) {
    if (/localhost/.test(document.location.host)) {
      this.baseUrl = "http://127.0.0.1:8000/operations/";
    }
  }

  createOperation(newOp: Operation) {
    this.createdOperation.emit(newOp);
  }

  updateOperation(newOp: Operation) {
    this.updatedOperation.emit(newOp);
  }

  //#region Server
  getOperations(): Observable<any> {
    let url = this.baseUrl + "?format=json";
    return this.http.get(url, httpOptions)
      .pipe(
        catchError(this.handleError('getHeroes', []))
    );
  }

  postOperation(operation: Operation): Observable<Operation> {
    console.log("token=", UserTokenStorage.getInstanse().getUserToken());
    return this.http.post<Operation>(this.baseUrl, JSON.stringify(operation), httpOptions)
    .pipe(
      catchError(this.handleError('addHero', operation))
    );
  }

  putOperation(operation: Operation): Observable<Operation> {
    let url = this.baseUrl + operation.id + "/";
    return this.http.put<Operation>(url, operation, httpOptions)
    .pipe(
      catchError(this.handleError('addHero', operation))
    );
  }

  deleteOperation(id: number) {
    var url = this.baseUrl + id + "/";
    return this.http.delete<Operation>(url, httpOptions);
  }
  //#endregion

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }


}
