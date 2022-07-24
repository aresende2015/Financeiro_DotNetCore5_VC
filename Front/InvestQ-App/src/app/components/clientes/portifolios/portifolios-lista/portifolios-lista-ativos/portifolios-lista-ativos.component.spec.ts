/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PortifoliosListaAtivosComponent } from './portifolios-lista-ativos.component';

describe('PortifoliosListaAtivosComponent', () => {
  let component: PortifoliosListaAtivosComponent;
  let fixture: ComponentFixture<PortifoliosListaAtivosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PortifoliosListaAtivosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PortifoliosListaAtivosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
