/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TesourosdiretosComponent } from './tesourosdiretos.component';

describe('TesourosdiretosComponent', () => {
  let component: TesourosdiretosComponent;
  let fixture: ComponentFixture<TesourosdiretosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TesourosdiretosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TesourosdiretosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
