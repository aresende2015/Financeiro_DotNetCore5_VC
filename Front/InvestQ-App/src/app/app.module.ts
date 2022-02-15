import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';

import { AppRoutingModule } from './app-routing.module';

import { DateTimeFormatPipe } from './helpers/DateTimeFormat.pipe';

import { AppComponent } from './app.component';
import { NavComponent } from './shared/nav/nav.component';
import { TituloComponent } from './shared/titulo/titulo.component';
import { ClientesComponent } from './components/clientes/clientes.component';
import { CorretorasComponent } from './components/corretoras/corretoras.component';
import { PerfilComponent } from './components/user/perfil/perfil.component';
import { ContatosComponent } from './components/contatos/contatos.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ClienteDetalheComponent } from './components/clientes/cliente-detalhe/cliente-detalhe.component';
import { ClienteListaComponent } from './components/clientes/cliente-lista/cliente-lista.component';
import { CorretoraDetalheComponent } from './components/corretoras/corretora-detalhe/corretora-detalhe.component';
import { CorretoraListaComponent } from './components/corretoras/corretora-lista/corretora-lista.component';
import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { SetorDetalheComponent } from './components/setores/setor-detalhe/setor-detalhe.component';
import { SetoresComponent } from './components/setores/setores.component';
import { SetorListaComponent } from './components/setores/setor-lista/setor-lista.component';
import { SubsetoresListaComponent } from './components/setores/subsetores-lista/subsetores-lista.component';
import { SubsetorDetalheComponent } from './components/setores/subsetor-detalhe/subsetor-detalhe.component';
import { SegmentosListaComponent } from './components/setores/segmentos-lista/segmentos-lista.component';

import { CorretoraService } from './services/corretora.service';
import { ClienteService } from './services/cliente.service';
import { SetorService } from './services/setor.service';
import { SubsetorService } from './services/subsetor.service';
import { SegmentoService } from './services/segmento.service';


defineLocale('pt-br', ptBrLocale);
@NgModule({
  declarations: [
    AppComponent,
    ClientesComponent,
    ClienteDetalheComponent,
    ClienteListaComponent,
    CorretorasComponent,
    CorretoraDetalheComponent,
    SetoresComponent,
    SetorDetalheComponent,
    SetorListaComponent,
    SubsetoresListaComponent,
    SubsetorDetalheComponent,
    SegmentosListaComponent,
    CorretoraListaComponent,
    ContatosComponent,
    PerfilComponent,
    DashboardComponent,
    NavComponent,
    DateTimeFormatPipe,
    TituloComponent,
    UserComponent,
    LoginComponent,
    RegistrationComponent
   ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true
    }),
    NgxSpinnerModule,
    BsDatepickerModule.forRoot()

  ],
  providers: [CorretoraService, ClienteService, SetorService, SubsetorService, SegmentoService],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
