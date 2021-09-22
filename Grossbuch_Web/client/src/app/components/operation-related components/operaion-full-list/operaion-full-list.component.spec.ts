import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
import { MatButtonModule, MatCheckboxModule } from '@angular/material';

import { OperaionFullListComponent } from './operaion-full-list.component';

import { OperationService } from 'src/app/services/OperationService/operation.service';
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from '../../navbar/navbar.component';
import { OperationComponent } from '../operation/operation.component';

describe('OperaionFullListComponent', () => {
  let component: OperaionFullListComponent;
  let fixture: ComponentFixture<OperaionFullListComponent>;

  let mockRouter = {
    navigate: jasmine.createSpy('')
  }

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientModule,
        FormsModule,
        OwlDateTimeModule,
        OwlNativeDateTimeModule,
        MatButtonModule,
        MatCheckboxModule
      ],
      declarations: [
        OperaionFullListComponent,
        OperationComponent,
        NavbarComponent
      ],
      providers: [
        OperationService,
        {provide: Router, useValue: mockRouter}
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OperaionFullListComponent);
    component = fixture.componentInstance;

    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });

  it('should get data from services', () => {
    expect(component).toBeTruthy();
  });

  it('should search operations', () => {
    expect(component).toBeTruthy();
  });
});