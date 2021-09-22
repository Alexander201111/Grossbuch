import { Component, OnInit, Input } from '@angular/core';
import { Account } from 'src/app/classes/account';
import { AccountService } from 'src/app/services/AccountService/account.service';

@Component({
  selector: 'app-add-account',
  templateUrl: './add-account.component.html',
  styleUrls: ['./add-account.component.css']
})
export class AddAccountComponent implements OnInit {
  
  @Input() newAccount: Account;

  addSum = 0;

  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.newAccount = new Account(-1, "", 0);
    this.accountService.changingAccountForAdd.subscribe((result: Account) => {
      this.newAccount = result;
      console.log("a");
    });
  }

  clickCancel() {
    this.accountService.changeAccountForAdd(null);
  }

  comingSum() {
    this.newAccount.balance = this.newAccount.balance + Number(this.addSum);
    this.accountService.updateAccount(this.newAccount);
    this.accountService.putAccount(this.newAccount).subscribe();
    this.addSum = 0;
  }

}
