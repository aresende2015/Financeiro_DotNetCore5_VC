/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SubsetorService } from './subsetor.service';

describe('Service: Subsetor', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SubsetorService]
    });
  });

  it('should ...', inject([SubsetorService], (service: SubsetorService) => {
    expect(service).toBeTruthy();
  }));
});
