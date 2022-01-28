/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CorretoraService } from './corretora.service';

describe('Service: Corretora', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CorretoraService]
    });
  });

  it('should ...', inject([CorretoraService], (service: CorretoraService) => {
    expect(service).toBeTruthy();
  }));
});
