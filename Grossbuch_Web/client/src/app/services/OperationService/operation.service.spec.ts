import { TestBed } from '@angular/core/testing';

import { OperationService } from './operation.service';
import { HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';

describe('OperationService', () => {
  let mockRouter = {
    navigate: jasmine.createSpy('')
  }

  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientModule
    ],
    declarations: [
    ],
    providers: [
      OperationService,
      {provide: Router, useValue: mockRouter}
    ]
  }));

  it('should be created', () => {
    const service: OperationService = TestBed.get(OperationService);
    expect(service).toBeTruthy();
  });

  it('should get operations', () => {
    expect(true).toBeTruthy();
  });

  it('should create operation', () => {
    expect(true).toBeTruthy();
  });

  it('should update operation', () => {
    expect(true).toBeTruthy();
  });

  it('should delete operation', () => {
    expect(true).toBeTruthy();
  });
});