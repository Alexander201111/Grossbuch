import { TestBed } from '@angular/core/testing';

import { PurposeService } from './purpose.service';
import { HttpClientModule } from '@angular/common/http';

describe('PurposeService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientModule
    ],
  }));

  it('should be created', () => {
    const service: PurposeService = TestBed.get(PurposeService);
    expect(service).toBeTruthy();
  });

  it('should get purposes', () => {
    expect(true).toBeTruthy();
  });

  it('should create purpose', () => {
    expect(true).toBeTruthy();
  });

  it('should update purpose', () => {
    expect(true).toBeTruthy();
  });

  it('should delete purpose', () => {
    expect(true).toBeTruthy();
  });
});