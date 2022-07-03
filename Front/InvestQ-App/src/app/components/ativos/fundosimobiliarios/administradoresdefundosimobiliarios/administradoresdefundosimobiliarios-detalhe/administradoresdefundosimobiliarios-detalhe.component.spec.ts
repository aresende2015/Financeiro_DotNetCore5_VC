/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AdministradoresdefundosimobiliariosDetalheComponent } from './administradoresdefundosimobiliarios-detalhe.component';

describe('AdministradoresdefundosimobiliariosDetalheComponent', () => {
  let component: AdministradoresdefundosimobiliariosDetalheComponent;
  let fixture: ComponentFixture<AdministradoresdefundosimobiliariosDetalheComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdministradoresdefundosimobiliariosDetalheComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdministradoresdefundosimobiliariosDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
