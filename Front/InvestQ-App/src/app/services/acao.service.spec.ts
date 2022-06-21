/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AcaoService } from './acao.service';

describe('Service: Acao', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AcaoService]
    });
  });

  it('should ...', inject([AcaoService], (service: AcaoService) => {
    expect(service).toBeTruthy();
  }));
});
