/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AtivoService } from './ativo.service';

describe('Service: Ativo', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AtivoService]
    });
  });

  it('should ...', inject([AtivoService], (service: AtivoService) => {
    expect(service).toBeTruthy();
  }));
});
