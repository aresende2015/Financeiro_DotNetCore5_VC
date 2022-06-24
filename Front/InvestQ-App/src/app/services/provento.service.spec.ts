/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ProventoService } from './provento.service';

describe('Service: Provento', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProventoService]
    });
  });

  it('should ...', inject([ProventoService], (service: ProventoService) => {
    expect(service).toBeTruthy();
  }));
});
