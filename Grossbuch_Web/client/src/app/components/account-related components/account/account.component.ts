import { Component, OnInit, Input } from '@angular/core';
import { Account } from 'src/app/classes/account';
import { AccountService } from 'src/app/services/AccountService/account.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})

export class AccountComponent implements OnInit {

  @Input() account: Account;

  addSum: number;

  constructor(private accountService: AccountService) { }

  ngOnInit() {
  }

  changeAccount() {
    this.accountService.changeAccountForChange(this.account);
  }

  addToAccount() {
    this.accountService.changeAccountForAdd(this.account);
  }

}
