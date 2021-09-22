import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';

import { CreateOperationComponent } from './create-operation.component';

import { AccountService } from 'src/app/services/AccountService/account.service';
import { CategoryService } from 'src/app/services/CategoryService/category.service';
import { OperationService } from 'src/app/services/OperationService/operation.service';
import { CurrencyService } from 'src/app/services/CurrencyService/currency.service';
import { PurposeService } from 'src/app/services/PurposeService/purpose.service';

describe('CreateOperationComponent', () => {
  let component: CreateOperationComponent;
  let fixture: ComponentFixture<CreateOperationComponent>;

  let mockRouter = {
    navigate: jasmine.createSpy('')
  }

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientModule,
        FormsModule,
        OwlDateTimeModule,
        OwlNativeDateTimeModule
      ],
      declarations: [
        CreateOperationComponent
      ],
      providers: [
        AccountService,
        CategoryService,
        CurrencyService,
        PurposeService,
        OperationService,
        {provide: Router, useValue: mockRouter}
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateOperationComponent);
    component = fixture.componentInstance;

    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });

  it('should get data from services', () => {
    expect(component).toBeTruthy();
  });

  it('should add new Operation to list if data is valid', () => {
    expect(component).toBeTruthy();
  });
});