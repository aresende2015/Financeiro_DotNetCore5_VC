/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ProventosListaComponent } from './proventos-lista.component';

describe('ProventosListaComponent', () => {
  let component: ProventosListaComponent;
  let fixture: ComponentFixture<ProventosListaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProventosListaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProventosListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
