import { Component, OnInit } from '@angular/core';
import { MatButtonModule, MatCheckboxModule, MatDatepickerInputEvent } from '@angular/material';
import { OperationService } from 'src/app/services/OperationService/operation.service';
import { AccountService } from 'src/app/services/AccountService/account.service';
import { CategoryService } from 'src/app/services/CategoryService/category.service';
import { CurrencyService } from 'src/app/services/CurrencyService/currency.service';
import { PurposeService } from 'src/app/services/PurposeService/purpose.service';
import { Account } from 'src/app/classes/account';
import { Category } from 'src/app/classes/category';
import { Operation } from 'src/app/classes/operation';

@Component({
  selector: 'app-operaion-full-list',
  templateUrl: './operaion-full-list.component.html',
  styleUrls: ['./operaion-full-list.component.css']
})
export class OperaionFullListComponent implements OnInit {

  operationsAll = [];
  operations = [];
  accounts = [];
  categories = [];
  currencies = [];
  purposes = [];

  chosenAccounts = [];
  chosenCategories = [];
  chosenCurrencies = [];
  chosenPurpose = [];

  totalSum = 0; countVisible = 0;
  minSum = 0; maxSum = 0; averageSum = 0;

  sortedSumm = ["По умолчанию", "Дешевле", "Дороже"];
  selectedSotredSumm = this.sortedSumm[0];

  sortedDate = ["Новее", "Старее"];
  selectedSotredDate = this.sortedDate[0];

  searchString = '';
  dateStart: Date = null;
  dateFinish: Date = null;

  constructor(private operationService: OperationService,
    private accountService: AccountService,
    private categoryService: CategoryService,
    private currencyService: CurrencyService,
    private purposeService: PurposeService
  ) { }

  ngOnInit() {
    this.getOperations();

    this.getAccounts();
    this.getCategories();
    this.getCurrencies();
    this.getPurposes();
  }

  //#region GetFromServer 
  getOperations(): void {
    this.operationService.getOperations()
      .subscribe(prodResp => {
        this.operationsAll = prodResp.results;
        this.operations = prodResp.results;
        this.minSum = this.operations[0].summ;
        this.maxSum = this.operations[0].summ;
        for (let i = 0; i < this.operations.length; i++) {
          this.operations[i].showed = true;
          
          this.countingProps();
          /* this.totalSum = this.totalSum + this.operations[i].summ; */
        }
      });
  }

  getAccounts(): void {
    this.accountService.getAccounts()
      .subscribe(prodResp => {
        this.accounts = prodResp.results;
        for (let i = 0; i < this.accounts.length; i++) {
          this.accounts[i].checked = false;
        }
      });
  }

  getCategories(): void {
    this.categoryService.getCategories()
      .subscribe(prodResp => {
        this.categories = prodResp.results;
        for (let i = 0; i < this.categories.length; i++) {
          this.categories[i].checked = false;
        }
      });
  }

  getCurrencies(): void {
    this.currencyService.getCurrencies()
      .subscribe(prodResp => {
        this.currencies = prodResp.results;
        for (let i = 0; i < this.currencies.length; i++) {
          this.currencies[i].checked = false;
        }
      });
  }

  getPurposes(): void {
    this.purposeService.getPurposes()
      .subscribe(prodResp => {
        this.purposes = prodResp.results;
        for (let i = 0; i < this.purposes.length; i++) {
          this.purposes[i].checked = false;
        }
      });
  }
  //#endregion

  //#region Filter
  accountCheck(account: Account) {
    if (account.checked == false) {
      this.chosenAccounts.push(account);
    } else {
      const index: number = this.chosenAccounts.indexOf(account);
      if (index !== -1) {
        this.chosenAccounts.splice(index, 1);
      }
    }
    this.filter("");
  }

  categoryCheck(category: Category) {
    if (category.checked == false) {
      this.chosenCategories.push(category);
    } else {
      const index: number = this.chosenCategories.indexOf(category);
      if (index !== -1) {
        this.chosenCategories.splice(index, 1);
      }
    }
    this.filter("");
  }

  filter(text: string) {
    this.totalSum = 0;
    this.minSum = 0; this.maxSum = 0; this.averageSum = 0;
    for (let i = 0; i < this.operations.length; i++) {
      this.operations[i].showed = true;
      
      for (let j = 0; j < this.chosenAccounts.length; j++) {
        if (this.operations[i].account.id != this.chosenAccounts[j].id) {
          this.operations[i].showed = false;
        }
      }

      if (this.operations[i].showed == true) {
        for (let j = 0; j < this.chosenCategories.length; j++) {
          if (this.chosenCategories[j].id != this.operations[i].category.id) {
            this.operations[i].showed = false;
          } else {
            this.operations[i].showed = true;
          }
        }
      }

      if (this.operations[i].showed == true) {
        for (let j = 0; j < this.chosenCurrencies.length; j++) {
          if (this.chosenCurrencies[j].id != this.operations[i].currency.id) {
            this.operations[i].showed = false;
          } else {
            this.operations[i].showed = true;
          }
        }
      }

      if (this.operations[i].showed == true) {
        for (let j = 0; j < this.chosenPurpose.length; j++) {
          if (this.chosenPurpose[j].id != this.operations[i].purpose.id) {
            this.operations[i].showed = false;
          } else {
            this.operations[i].showed = true;
          }
        }
      }
      
      if (text != "" && this.operations[i].showed == true) {
        this.search(text, this.operations[i]);
      }
      /* if ((this.dateStart || this.dateFinish) && this.operations[i].showed == true) {
        this.changeDateFilter(this.operations[i]);
      } */
      if (this.operations[i].showed == true) {
        this.maxSum = this.operations[i].summ;
        this.minSum = this.operations[i].summ;
      }
    }

    this.countingProps();
  }

  countingProps() {
    this.totalSum = 0;
    for (let i = 0; i < this.operations.length; i++) {
      if (this.operations[i].showed == true) {
        this.countVisible = this.countVisible + 1;
        this.totalSum = this.totalSum + this.operations[i].summ;
        if (this.operations[i].summ > this.maxSum) { this.maxSum = this.operations[i].summ; }
        if (this.operations[i].summ < this.minSum) { this.minSum = this.operations[i].summ; }
      }
    }
  }
  //#endregion

  //#region Search
  get searchInput() {
    return this.searchString;
  }

  set searchInput(value: string) {
    this.searchString = value;
    this.onSearchInputChanged(value);
  }

  onSearchInputChanged(text: string): void {
    this.filter(text);
  }

  search(text: string, operation: Operation): void {
    if (!operation.description.includes(text) &&
      !operation.summ.toString().includes(text) &&
      !operation.account.title.includes(text) &&
      !operation.category.title.includes(text) &&
      !operation.currency.title.includes(text) &&
      !operation.purpose.title.includes(text)) {
      operation.showed = false;
    }
  }

  clearSearch() {
    this.searchInput = '';
  }
  //#endregion

  //#region Date
  changeDate() {
    this.changeDateFilter();
  }

  changeDateFilter() {
    for (let i = 0; i < this.operations.length; i++) {
      if ((this.dateStart || this.dateFinish) && this.operations[i].showed == true) {
        let date = new Date(this.operations[i].date);
        if (date.getDate() < this.dateStart.getDate() || date.getDate() > this.dateFinish.getDate()) {
          this.operations[i].showed = false;
        }
      }
    }
  }

  clearDate() {
    this.dateStart = null;
    this.dateFinish = null;
    this.filter("");
  }
  //#endregion

  onChangeSortSum(deviceValue: any) {
    console.log(this.operations);
    this.selectedSotredSumm = deviceValue;
    if (deviceValue === this.sortedSumm[0]) {
      this.operations.sort((a, b) => {
        if (a.date > b.date) return -1;
        else if (a.date < b.date) return 1;
        else return 0;
      });
    }
    if (deviceValue === this.sortedSumm[1]) {
      this.operations.sort((a, b) => {
        if (a.summ < b.summ) return -1;
        else if (a.summ > b.summ) return 1;
        else return 0;
      });
    }
    if (deviceValue === this.sortedSumm[2]) {
      this.operations.sort((a, b) => {
        if (a.summ > b.summ) return -1;
        else if (a.summ < b.summ) return 1;
        else return 0;
      });
    }
  }

  onChangeSortDate(deviceValue: any) {
    console.log(this.operations);
    this.selectedSotredDate = deviceValue;
    this.selectedSotredSumm = this.sortedSumm[0];
    if (deviceValue === this.sortedDate[0]) {
      this.operations.sort((a: Operation, b: Operation) => {
        if (a.date > b.date) return -1;
        else if (a.date < b.date) return 1;
        else return 0;
      });
    }
    if (deviceValue === this.sortedDate[1]) {
      this.operations.sort((a: Operation, b: Operation) => {
        if (a.date < b.date) return -1;
        else if (a.date > b.date) return 1;
        else return 0;
      });
    }
  }
}
