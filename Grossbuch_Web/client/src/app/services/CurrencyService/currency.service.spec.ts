import { TestBed } from '@angular/core/testing';

import { CurrencyService } from './currency.service';
import { HttpClientModule } from '@angular/common/http';

describe('CurrencyService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientModule
    ],
  }));

  it('should be created', () => {
    const service: CurrencyService = TestBed.get(CurrencyService);
    expect(service).toBeTruthy();
  });

  it('should get currencies', () => {
    expect(true).toBeTruthy();
  });

  it('should create currency', () => {
    expect(true).toBeTruthy();
  });

  it('should update currency', () => {
    expect(true).toBeTruthy();
  });

  it('should delete currency', () => {
    expect(true).toBeTruthy();
  });
});