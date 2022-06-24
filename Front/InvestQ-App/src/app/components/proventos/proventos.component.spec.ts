/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ProventosComponent } from './proventos.component';

describe('ProventosComponent', () => {
  let component: ProventosComponent;
  let fixture: ComponentFixture<ProventosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProventosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProventosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
