import { TestBed } from '@angular/core/testing';

import { InputserviceService } from './inputservice.service';

describe('InputserviceService', () => {
  let service: InputserviceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InputserviceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
