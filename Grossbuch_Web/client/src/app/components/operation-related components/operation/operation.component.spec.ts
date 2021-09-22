import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { OperationComponent } from './operation.component';

import { OperationService } from 'src/app/services/OperationService/operation.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { OperationListComponent } from '../operation-list/operation-list.component';
import { AppComponent } from 'src/app/app.component';
import { HttpClientModule } from '@angular/common/http';

describe('OperationComponent', () => {
  let component: OperationComponent;
  let fixture: ComponentFixture<OperationComponent>;

  let mockRouter = {
    navigate: jasmine.createSpy('')
  }

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientModule,
        FormsModule
      ],
      declarations: [
        OperationComponent
      ],
      providers: [
        OperationService,
        {provide: Router, useValue: mockRouter}
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OperationComponent);
    component = fixture.componentInstance;

    component.operation = {
      id: 1,
      date: new Date(5, 5, 2019),
      account: { id: 1, title: "Test Account", balance: 10, totalSum: 0, isAccount: true, checked: true },
      category: { id: 1, title: "Test Category", totalSum: 0, checked: true },
      currency: { id: 1, title: "Test Account", coefficient: 1, totalSum: 0 },
      purpose: { id: 2, title: "Test Purpose", balance: 0, totalSum: 0, isAccount: false, checked: true },
      summ: 100,
      description: "Test descr",
      showed: false
    }

    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });

  it('should update operation', () => {
    expect(component).toBeTruthy();
  });
});