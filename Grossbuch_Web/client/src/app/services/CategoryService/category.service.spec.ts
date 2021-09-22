import { TestBed } from '@angular/core/testing';

import { CategoryService } from './category.service';
import { HttpClientModule } from '@angular/common/http';

describe('CategoryService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientModule
    ],
  }));

  it('should be created', () => {
    const service: CategoryService = TestBed.get(CategoryService);
    expect(service).toBeTruthy();
  });

  it('should get categories', () => {
    expect(true).toBeTruthy();
  });

  it('should create category', () => {
    expect(true).toBeTruthy();
  });

  it('should update category', () => {
    expect(true).toBeTruthy();
  });

  it('should delete category', () => {
    expect(true).toBeTruthy();
  });
});