import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { MainComponent } from './components/main/main.component';
import { AppComponent } from './app.component';
import { AuthGuard } from './guard/auth.guard';
import { CreateOperationComponent } from './components/operation-related components/create-operation/create-operation.component';
import { CreateCategoryComponent } from './components/category-related components/create-category/create-category.component';
import { CreateAccountComponent } from './components/account-related components/create-account/create-account.component';
import { OperaionFullListComponent } from './components/operation-related components/operaion-full-list/operaion-full-list.component';
import { CreatePurposeComponent } from './components/purpose-related components/create-purpose/create-purpose.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: '', component: AppComponent, canActivate: [AuthGuard],
    children: [
      { path: '', component: MainComponent, pathMatch: 'full'},
      { path: 'newoperation', component: CreateOperationComponent },
      { path: 'newaccount', component: CreateAccountComponent },
      { path: 'newcategory', component: CreateCategoryComponent },
      { path: 'newpurpose', component: CreatePurposeComponent },

      { path: 'allOperations', component: OperaionFullListComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }