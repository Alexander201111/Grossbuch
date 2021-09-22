import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
import { Operation } from 'src/app/classes/operation';
import { AccountService } from 'src/app/services/AccountService/account.service';
import { CategoryService } from 'src/app/services/CategoryService/category.service';
import { OperationService } from 'src/app/services/OperationService/operation.service';
import { CurrencyService } from 'src/app/services/CurrencyService/currency.service';
import { PurposeService } from 'src/app/services/PurposeService/purpose.service';

@Component({
  selector: 'app-create-operation',
  templateUrl: './create-operation.component.html',
  styleUrls: ['./create-operation.component.css']
})
export class CreateOperationComponent implements OnInit {

  accounts = [];
  categories = [];
  currencies = [];
  purposes = [];

  creating: boolean;
  chosenCurrency: string;

  newOperation: Operation;

  constructor(private operationService: OperationService,
    private accountService: AccountService,
    private categoryService: CategoryService,
    private currencyService: CurrencyService,
    private purposeService: PurposeService,
    private router: Router) { }

  ngOnInit() {
    if(this.operationService.operationForChange != null) {
      this.newOperation = this.operationService.operationForChange;
      console.log("updating: ", this.newOperation);
      this.creating = false;
    } else {
      this.newOperation = new Operation(-1, null, null, null, null, "", 0);
      this.creating = true;
    }
    this.getAccounts();
    this.getCategories();
    this.getCurrencies();
    this.getPurposes();
  }

  getAccounts(): void {
    this.accountService.getAccounts()
      .subscribe(prodResp =>
        this.accounts = prodResp.results
      );
  }

  getCategories(): void {
    this.categoryService.getCategories()
      .subscribe(prodResp =>
        this.categories = prodResp.results
      );
  }

  getCurrencies(): void {
    this.currencyService.getCurrencies()
      .subscribe(prodResp => {
        this.currencies = prodResp.results,
        this.chosenCurrency = this.currencies[0].title;
        if(this.creating === true) {
          this.newOperation.currency = this.currencies[0];
        }
      });
  }

  getPurposes(): void {
    this.purposeService.getPurposes()
      .subscribe(prodResp =>
        this.purposes = prodResp.results
      );
  }

  onChangeAccount(deviceValue: any) {
    for(let i=0; i<this.accounts.length; i++) {
      if(this.accounts[i].title == deviceValue) {
        this.newOperation.account = this.accounts[i];
        console.log("newOperation.account=", this.accounts[i]);
        break;
      }
    }
  }

  onChangeCategory(deviceValue: any) {
    for(let i=0; i<this.categories.length; i++) {
      if(this.categories[i].title == deviceValue) {
        this.newOperation.category = this.categories[i];
        console.log("newOperation.category=", this.categories[i]);
        break;
      }
    }
  }

  onChangeCurrency(deviceValue: any) {
    this.chosenCurrency = deviceValue;
    for(let i=0; i<this.currencies.length; i++) {
      if(this.currencies[i].title == deviceValue) {
        this.newOperation.currency = this.currencies[i];
        console.log("newOperation.currency=", this.currencies[i]);
        break;
      }
    }
  }

  onChangePurpose(deviceValue: any) {
    for(let i=0; i<this.purposes.length; i++) {
      if(this.purposes[i].title == deviceValue) {
        this.newOperation.purpose = this.purposes[i];
        console.log("newOperation.purpose=", this.purposes[i]);
        break;
      }
    }
  }

  clickSave() {
    if(this.creating == true) {
      console.log("added operation: ", this.newOperation);
      this.operationService.createOperation(this.newOperation);
      this.operationService.postOperation(this.newOperation).subscribe((data) => { /* console.log(data); */ });
      
      this.newOperation.account.balance = this.newOperation.account.balance - this.newOperation.summ;
      this.newOperation.category.totalSum = this.newOperation.category.totalSum + this.newOperation.summ;
      this.newOperation.purpose.totalSum = this.newOperation.purpose.totalSum + this.newOperation.summ;

      this.accountService.updateAccount(this.newOperation.account);
      this.categoryService.updateCategory(this.newOperation.category);
      this.purposeService.updatePurpose(this.newOperation.purpose);
      this.router.navigate(['']);
    } else {
      this.operationService.updateOperation(this.newOperation);
      console.log("updated operation: ", this.newOperation);
      this.operationService.operationForChange = null;
      this.operationService.putOperation(this.newOperation).subscribe((data) => { /* console.log(data); */ });
      this.router.navigate(['']);
    }
  }

  clickCancel() {
    this.operationService.operationForChange = null;
    this.router.navigate(['']);
  }

}
