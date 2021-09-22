import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
import { AccountService } from 'src/app/services/AccountService/account.service'
import { Account } from 'src/app/classes/account';

@Component({
  selector: 'app-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.css']
})
export class AccountListComponent implements OnInit {

  accounts = [];

  constructor(private accountService: AccountService,
    private router: Router) { }

  ngOnInit() {
    this.accountService.createdAccount.subscribe((result) => {
      this.accounts.push(result);
    });

    this.accountService.updatedAccount.subscribe((result) => {
      for(let i=0; i<this.accounts.length; i++) {
        if(this.accounts[i].id == result.id) {
          this.accounts[i] = result;
        }
      }
    });

    this.getAccounts();
  }

  getAccounts(): void {
    this.accountService.getAccounts()
    .subscribe(prodResp => 
      this.accounts = prodResp.results
    );
  }
}
