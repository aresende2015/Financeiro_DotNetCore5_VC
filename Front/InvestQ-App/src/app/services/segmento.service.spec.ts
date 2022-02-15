/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SegmentoService } from './segmento.service';

describe('Service: Segmento', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SegmentoService]
    });
  });

  it('should ...', inject([SegmentoService], (service: SegmentoService) => {
    expect(service).toBeTruthy();
  }));
});
