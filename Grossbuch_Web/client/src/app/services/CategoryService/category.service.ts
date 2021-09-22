import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subscription, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Category } from '../../classes/category';

import { UserTokenStorage } from 'src/app/classes/user-token-storage';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'JWT ' + UserTokenStorage.getInstanse().getUserToken()
  })
};

@Injectable({ providedIn: 'root' })
export class CategoryService {

  categoryForChange: Category = null;
  baseUrl = "http://15520.pythonanywhere.com/categories/";

  public createdCategory: EventEmitter<any> = new EventEmitter();
  public updatedCategory: EventEmitter<any> = new EventEmitter();

  public changingCategoryForChange: EventEmitter<any> = new EventEmitter();
  
  constructor(private http: HttpClient) {
    if (/localhost/.test(document.location.host)) {
      this.baseUrl = "http://127.0.0.1:8000/categories/";
    }
  }

  changeCategoryForChange(purp: Category) {
    if(purp == null) { this.changingCategoryForChange.emit(new Category(-1, "", 0)); }
    else { this.changingCategoryForChange.emit(purp); }
  }

  createCategory(newOp: Category) {
    this.createdCategory.emit(newOp);
  }

  updateCategory(newOp: Category) {
    this.updatedCategory.emit(newOp);
  }

  getCategories(): Observable<any> {
    let url = this.baseUrl + "?format=json";
    return this.http.get(url, httpOptions)
      .pipe(
        catchError(this.handleError('getHeroes', []))
      );
  }

  postCategory(category: Category): Observable<Category> {
    return this.http.post<Category>(this.baseUrl, category, httpOptions)
      .pipe(
        catchError(this.handleError('addHero', category))
      );
  }

  putCategory(category: Category): Observable<Category> {
    let url = this.baseUrl + category.id + "/";
    return this.http.put<Category>(url, category, httpOptions)
      .pipe(
        catchError(this.handleError('addHero', category))
      );
  }

  deleteCategory(id: number) {
    var url = this.baseUrl + id + "/";
    return this.http.delete<Category>(url, httpOptions);
  }

  private handleError<T>(category = 'category', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
