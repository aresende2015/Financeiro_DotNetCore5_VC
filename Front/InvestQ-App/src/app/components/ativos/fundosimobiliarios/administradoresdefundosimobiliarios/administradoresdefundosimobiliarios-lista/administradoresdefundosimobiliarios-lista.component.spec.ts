/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AdministradoresdefundosimobiliariosListaComponent } from './administradoresdefundosimobiliarios-lista.component';

describe('AdministradoresdefundosimobiliariosListaComponent', () => {
  let component: AdministradoresdefundosimobiliariosListaComponent;
  let fixture: ComponentFixture<AdministradoresdefundosimobiliariosListaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdministradoresdefundosimobiliariosListaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdministradoresdefundosimobiliariosListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
