/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { SetorListaComponent } from './setor-lista.component';

describe('SetorListaComponent', () => {
  let component: SetorListaComponent;
  let fixture: ComponentFixture<SetorListaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SetorListaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SetorListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
