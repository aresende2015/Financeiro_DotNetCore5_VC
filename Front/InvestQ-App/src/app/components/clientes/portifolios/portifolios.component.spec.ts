/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PortifoliosComponent } from './portifolios.component';

describe('PortifoliosComponent', () => {
  let component: PortifoliosComponent;
  let fixture: ComponentFixture<PortifoliosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PortifoliosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PortifoliosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
