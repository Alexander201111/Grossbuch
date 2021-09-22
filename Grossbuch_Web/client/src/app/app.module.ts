import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AuthGuard } from './guard/auth.guard';

import { MatFormFieldModule } from '@angular/material';
import { MatInputModule } from '@angular/material';
import { MatDialogModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatCheckboxModule } from '@angular/material';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material';
import { MatAutocompleteModule } from '@angular/material/autocomplete';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { MainComponent } from './components/main/main.component';
import { OperationListComponent } from './components/operation-related components/operation-list/operation-list.component';
import { OperationComponent } from './components/operation-related components/operation/operation.component';
import { AccountListComponent } from './components/account-related components/account-list/account-list.component';
import { CategoryListComponent } from './components/category-related components/category-list/category-list.component';
import { AccountComponent } from './components/account-related components/account/account.component';
import { CategoryComponent } from './components/category-related components/category/category.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { CreateOperationComponent } from './components/operation-related components/create-operation/create-operation.component';
import { AddAccountComponent } from './components/account-related components/add-account/add-account.component';
import { CreateAccountComponent } from './components/account-related components/create-account/create-account.component';
import { CreateCategoryComponent } from './components/category-related components/create-category/create-category.component';
import { OperaionFullListComponent } from './components/operation-related components/operaion-full-list/operaion-full-list.component';

import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
import { PurposeComponent } from './components/purpose-related components/purpose/purpose.component';
import { PurposeListComponent } from './components/purpose-related components/purpose-list/purpose-list.component';
import { CreatePurposeComponent } from './components/purpose-related components/create-purpose/create-purpose.component';
import { PieChartComponent } from './components/pie-chart/pie-chart.component';
import { ChartsModule } from 'ng2-charts';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    MainComponent,
    OperationListComponent,
    OperationComponent,
    AccountListComponent,
    CategoryListComponent,
    AccountComponent,
    CategoryComponent,
    NavbarComponent,
    CreateOperationComponent,
    CreateAccountComponent,
    CreateCategoryComponent,
    AddAccountComponent,
    OperaionFullListComponent,
    PurposeComponent,
    PurposeListComponent,
    CreatePurposeComponent,
    PieChartComponent
  ],
  imports: [
    BrowserModule,
    OwlDateTimeModule, OwlNativeDateTimeModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatButtonModule, MatCheckboxModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatAutocompleteModule,
    ChartsModule
  ],
  providers: [
    AuthGuard,
    MatDatepickerModule,
  ],
  bootstrap: [AppComponent],
  exports: [MatButtonModule, MatCheckboxModule],
})
export class AppModule { }
