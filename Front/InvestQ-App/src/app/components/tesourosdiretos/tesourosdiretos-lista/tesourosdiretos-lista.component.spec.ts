/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TesourosdiretosListaComponent } from './tesourosdiretos-lista.component';

describe('TesourosdiretosListaComponent', () => {
  let component: TesourosdiretosListaComponent;
  let fixture: ComponentFixture<TesourosdiretosListaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TesourosdiretosListaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TesourosdiretosListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
