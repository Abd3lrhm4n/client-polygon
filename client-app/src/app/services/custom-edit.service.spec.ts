import { TestBed } from '@angular/core/testing';

import { CustomEditService } from './custom-edit.service';

describe('CustomEditService', () => {
  let service: CustomEditService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CustomEditService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
