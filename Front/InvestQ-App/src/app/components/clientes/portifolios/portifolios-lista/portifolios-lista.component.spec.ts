/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PortifoliosListaComponent } from './portifolios-lista.component';

describe('PortifoliosListaComponent', () => {
  let component: PortifoliosListaComponent;
  let fixture: ComponentFixture<PortifoliosListaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PortifoliosListaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PortifoliosListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
