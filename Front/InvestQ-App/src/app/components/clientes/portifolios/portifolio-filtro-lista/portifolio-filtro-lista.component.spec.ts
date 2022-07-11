/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PortifolioFiltroListaComponent } from './portifolio-filtro-lista.component';

describe('PortifolioFiltroListaComponent', () => {
  let component: PortifolioFiltroListaComponent;
  let fixture: ComponentFixture<PortifolioFiltroListaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PortifolioFiltroListaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PortifolioFiltroListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
