import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router'

import { LoginModel } from '../../classes/login-model';
import { UserTokenStorage } from 'src/app/classes/user-token-storage';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private httpOptions: any;
  baseUrl = "http://15520.pythonanywhere.com/";

  public loginEvent = new EventEmitter();
  public logoutEvent = new EventEmitter();

  currentUser: LoginModel = null;;

  //public token: string;
  //public token_expires: Date;
  //public username: string;

  public errors: any = [];

  constructor(private http: HttpClient, private router: Router) {
    this.httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };

    if (/localhost/.test(document.location.host)) {
      this.baseUrl = "http://127.0.0.1:8000/";
    }
  }

  public getCurrentUser() {
    if (this.currentUser == null) {
      this.currentUser = new LoginModel();
      let token_decoded = JSON.parse(window.atob(UserTokenStorage.getInstanse().getUserToken().split(/\./)[1]));
      console.log(token_decoded);
      this.currentUser.userId = token_decoded.user_id;
      this.currentUser.username = token_decoded.username;
    }
    return this.currentUser;
  }

  public login() {
    this.loginEvent.emit();
  }

  public loginToServer(user: LoginModel) {
    this.currentUser = new LoginModel();
    this.http.post(this.baseUrl + 'api-token-auth/', JSON.stringify(user), this.httpOptions).subscribe(
      data => {
        this.currentUser.username = user.username;
        this.currentUser.token = data['token'];
        this.currentUser.receiveAnswerFromServer = true;
        UserTokenStorage.getInstanse().setUserToken(data['token']);

        if (this.currentUser.receiveAnswerFromServer == true) {
          this.router.navigate(['']);
        }
      },
      err => {
        this.errors = err['error'];
      }
    );
  }

  public refreshToken() {
    this.http.post(this.baseUrl + 'api-token-refresh/', JSON.stringify({ token: this.currentUser.token }), this.httpOptions).subscribe(
      data => {
        this.currentUser.token = data['token'];
      },
      err => {
        this.errors = err['error'];
      }
    );
  }

  public logout() {
    /* this.logoutEvent.emit(); */
    UserTokenStorage.getInstanse().clearStorage();
    this.router.navigate(['login']);
    location.reload();
  }

  /* private updateUser(user: LoginModel, token) {
    this.currentUser.username = user.username;
    this.currentUser.token = token;
 
    // decode the token to read the username and expiration timestamp
    const token_parts = token.split(/\./);
    const token_decoded = JSON.parse(window.atob(token_parts[1]));
    this.token_expires = new Date(token_decoded.exp * 1000);
    this.username = token_decoded.username;
  } */
}
