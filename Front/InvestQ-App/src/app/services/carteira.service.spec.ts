/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CarteiraService } from './carteira.service';

describe('Service: Carteira', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CarteiraService]
    });
  });

  it('should ...', inject([CarteiraService], (service: CarteiraService) => {
    expect(service).toBeTruthy();
  }));
});
