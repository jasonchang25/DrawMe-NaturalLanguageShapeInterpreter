import { TestBed } from '@angular/core/testing';

import { InputInterpreterService } from './input-interpreter.service';

describe('InputInterpreterService', () => {
  let service: InputInterpreterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InputInterpreterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
