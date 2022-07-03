/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { FundosimobiliariosComponent } from './fundosimobiliarios.component';

describe('FundosimobiliariosComponent', () => {
  let component: FundosimobiliariosComponent;
  let fixture: ComponentFixture<FundosimobiliariosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FundosimobiliariosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FundosimobiliariosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
