/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TiposdeinvestimentosDetalheComponent } from './tiposdeinvestimentos-detalhe.component';

describe('TiposdeinvestimentosDetalheComponent', () => {
  let component: TiposdeinvestimentosDetalheComponent;
  let fixture: ComponentFixture<TiposdeinvestimentosDetalheComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TiposdeinvestimentosDetalheComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TiposdeinvestimentosDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
