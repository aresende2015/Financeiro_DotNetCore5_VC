/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AdministradoresdefundosimobiliariosComponent } from './administradoresdefundosimobiliarios.component';

describe('AdministradoresdefundosimobiliariosComponent', () => {
  let component: AdministradoresdefundosimobiliariosComponent;
  let fixture: ComponentFixture<AdministradoresdefundosimobiliariosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdministradoresdefundosimobiliariosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdministradoresdefundosimobiliariosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
