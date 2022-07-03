import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FiltrarComponent } from './filtrar/filtrar.component';
import { FormDebugComponent } from './form-debug/form-debug.component';
import { NavComponent } from './nav/nav.component';
import { SalvarComponent } from './salvar/salvar.component';
import { TituloComponent } from './titulo/titulo.component';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { AppRoutingModule } from '@app/app-routing.module';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@NgModule({
  imports: [
    CommonModule,
    AppRoutingModule,
    CollapseModule.forRoot(),
    BsDropdownModule.forRoot(),
  ],
  declarations: [
    FiltrarComponent,
    FormDebugComponent,
    NavComponent,
    SalvarComponent,
    TituloComponent
  ],
  exports: [
    FiltrarComponent,
    FormDebugComponent,
    NavComponent,
    SalvarComponent,
    TituloComponent
  ]
})
export class SharedModule { }
