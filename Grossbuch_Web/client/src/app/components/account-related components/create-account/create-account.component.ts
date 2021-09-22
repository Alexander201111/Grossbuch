import { Component, OnInit, Input } from '@angular/core';
import { Account } from 'src/app/classes/account';
import { AccountService } from 'src/app/services/AccountService/account.service';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.css']
})
export class CreateAccountComponent implements OnInit {

  creating: boolean = true;
  
  @Input() newAccount: Account;

  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.newAccount = new Account(-1, "", 0);
    this.accountService.changingAccountForChange.subscribe((result: Account) => {
      this.newAccount = result;
      this.creating = (this.newAccount.id != -1) ? false : true;
    });
  }

  clickSave() {
    if(this.creating == true) {
      console.log("added operation: ", this.newAccount);
      this.accountService.createAccount(this.newAccount);
      this.accountService.postAccount(this.newAccount).subscribe();
    } else {
      console.log("updated operation: ", this.newAccount);
      this.accountService.updateAccount(this.newAccount);
      this.accountService.putAccount(this.newAccount).subscribe();
    }
    this.accountService.changeAccountForChange(null);
  }

  clickDelete() {
    console.log("deleted account: ", this.newAccount);
    this.accountService.deleteAccount(this.newAccount.id).subscribe();
    this.accountService.changeAccountForChange(null);
  }

  clickCancel() {
    this.accountService.changeAccountForChange(null);
  }

}
