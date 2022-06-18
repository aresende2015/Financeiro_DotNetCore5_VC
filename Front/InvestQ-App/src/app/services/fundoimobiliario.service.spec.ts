/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { FundoimobiliarioService } from './fundoimobiliario.service';

describe('Service: Fundoimobiliario', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FundoimobiliarioService]
    });
  });

  it('should ...', inject([FundoimobiliarioService], (service: FundoimobiliarioService) => {
    expect(service).toBeTruthy();
  }));
});
