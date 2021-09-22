import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'

import { LoginModel } from '../../classes/login-model';

import { UserService } from '../../services/UserService/user.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  logUser: LoginModel = new LoginModel();
  logEventSubscription: Subscription;
  
  showErrorMsg: boolean = false;
  
  constructor(private userService: UserService, private router: Router) {
    this.logUser.username = "admin";
    this.logUser.password = "admin";
    
    this.logEventSubscription = this.userService.loginEvent.subscribe(() => {
      this.userService.loginToServer(this.logUser)
    });
  }

  ngOnInit() {
  }

  login() {
    this.userService.login();
  }
 
  refreshToken() {
    this.userService.refreshToken();
  }
 
  logout() {
    this.userService.logout();
  }

}
