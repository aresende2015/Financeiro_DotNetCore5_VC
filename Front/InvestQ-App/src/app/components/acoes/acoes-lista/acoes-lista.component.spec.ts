/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AcoesListaComponent } from './acoes-lista.component';

describe('AcoesListaComponent', () => {
  let component: AcoesListaComponent;
  let fixture: ComponentFixture<AcoesListaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AcoesListaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AcoesListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
