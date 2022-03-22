/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { TipodeinvestimentoService } from './tipodeinvestimento.service';

describe('Service: Tipodeinvestimento', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TipodeinvestimentoService]
    });
  });

  it('should ...', inject([TipodeinvestimentoService], (service: TipodeinvestimentoService) => {
    expect(service).toBeTruthy();
  }));
});
