/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { FundosimobiliariosDetalheComponent } from './fundosimobiliarios-detalhe.component';

describe('FundosimobiliariosDetalheComponent', () => {
  let component: FundosimobiliariosDetalheComponent;
  let fixture: ComponentFixture<FundosimobiliariosDetalheComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FundosimobiliariosDetalheComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FundosimobiliariosDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
