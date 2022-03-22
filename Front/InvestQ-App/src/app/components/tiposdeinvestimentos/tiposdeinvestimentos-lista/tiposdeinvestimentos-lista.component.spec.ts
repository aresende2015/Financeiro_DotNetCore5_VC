/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TiposdeinvestimentosListaComponent } from './tiposdeinvestimentos-lista.component';

describe('TiposdeinvestimentosListaComponent', () => {
  let component: TiposdeinvestimentosListaComponent;
  let fixture: ComponentFixture<TiposdeinvestimentosListaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TiposdeinvestimentosListaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TiposdeinvestimentosListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
