/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PortifolioService } from './portifolio.service';

describe('Service: Portifolio', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PortifolioService]
    });
  });

  it('should ...', inject([PortifolioService], (service: PortifolioService) => {
    expect(service).toBeTruthy();
  }));
});
