import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
import { UserService } from 'src/app/services/UserService/user.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private router: Router, private userService: UserService) { }

  ngOnInit() {
  }

  logout() {
    this.userService.logout();
  }

  pageMain() {
    this.router.navigate(['']);
  }

  pageAllOperation() {
    this.router.navigate(['allOperations']);
  }

}
