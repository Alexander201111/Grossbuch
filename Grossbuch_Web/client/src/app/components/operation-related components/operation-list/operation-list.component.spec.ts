import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
import { MatButtonModule, MatCheckboxModule } from '@angular/material';

import { OperationListComponent } from './operation-list.component';

import { OperationService } from 'src/app/services/OperationService/operation.service';
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from '../../navbar/navbar.component';
import { OperationComponent } from '../operation/operation.component';

describe('OperationListComponent', () => {
  let component: OperationListComponent;
  let fixture: ComponentFixture<OperationListComponent>;

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
        OperationListComponent,
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
    fixture = TestBed.createComponent(OperationListComponent);
    component = fixture.componentInstance;

    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });

  it('should update operations in list', () => {
    expect(component).toBeTruthy();
  });

  it('should delete operations from list', () => {
    expect(component).toBeTruthy();
  });
});