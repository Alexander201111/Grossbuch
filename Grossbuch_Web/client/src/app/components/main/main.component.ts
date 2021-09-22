import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  isAuthorized: boolean = false;

  myControl = new FormControl();
  options: string[] = ['One', 'Two', 'Three'];
  
  constructor(private router: Router) {}

  ngOnInit() {
    
  }

  addOperation() {
    this.router.navigate(['newoperation']);
  }

}
