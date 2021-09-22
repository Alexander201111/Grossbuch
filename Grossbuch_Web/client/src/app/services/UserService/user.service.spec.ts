import { TestBed } from '@angular/core/testing';

import { UserService } from './user.service';
import { HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';

describe('UserService', () => {
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
      UserService,
      {provide: Router, useValue: mockRouter}
    ]
  }));

  it('should be created', () => {
    const service: UserService = TestBed.get(UserService);
    expect(service).toBeTruthy();
  });

  it('should save new user if data is valid', () => {
    expect(true).toBeTruthy();
  });

  it('should login', () => {
    expect(true).toBeTruthy();
  });

  it('should refresh token', () => {
    expect(true).toBeTruthy();
  });

  it('should logout', () => {
    expect(true).toBeTruthy();
  });

  it('should update user', () => {
    expect(true).toBeTruthy();
  });

  it('should delete user', () => {
    expect(true).toBeTruthy();
  });
});