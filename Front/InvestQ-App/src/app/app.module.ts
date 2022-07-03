import { DEFAULT_CURRENCY_CODE, LOCALE_ID, CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { NgxCurrencyModule } from 'ngx-currency';

import { AppRoutingModule } from './app-routing.module';

import ptBr from '@angular/common/locales/pt'
import { registerLocaleData } from '@angular/common';

import { JwtInterceptor } from './interceptors/jwt.interceptor';

import { AppComponent } from './app.component';
import { PerfilComponent } from './components/user/perfil/perfil.component';
import { ContatosComponent } from './components/contatos/contatos.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { UserComponent } from './components/user/user.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { CorretoraService } from './services/corretora.service';
import { ClienteService } from './services/cliente.service';
import { SetorService } from './services/setor.service';
import { SubsetorService } from './services/subsetor.service';
import { SegmentoService } from './services/segmento.service';
import { UserService } from './services/user.service';
import { TipodeinvestimentoService } from './services/tipodeinvestimento.service';
import { TesourodiretoService } from './services/tesourodireto.service';
import { AtivoService } from './services/ativo.service';
import { AdministradordefundoimobiliarioService } from './services/administradordefundoimobiliario.service';
import { SegmentoanbimaService } from './services/segmentoanbima.service';
import { FundoimobiliarioService } from './services/fundoimobiliario.service';
import { AcaoService } from './services/acao.service';
import { ProventoService } from './services/provento.service';
import { CarteiraService } from './services/carteira.service';
import { SharedModule } from './shared/shared.module';
import { CorretorasModule } from './components/corretoras/corretoras.module';
import { ClientesModule } from './components/clientes/clientes.module';
import { HelpersModule } from './helpers/helpers.module';
import { AtivosModule } from './components/ativos/ativos.module';

defineLocale('pt-br', ptBrLocale);
registerLocaleData(ptBr);
@NgModule({
  declarations: [
    AppComponent,
    ContatosComponent,
    PerfilComponent,
    DashboardComponent,
    UserComponent,
    HomeComponent,
    LoginComponent,
    RegistrationComponent
   ],
  imports: [
    BrowserModule,
    FormsModule,
    NgxCurrencyModule,
    ReactiveFormsModule,
    HelpersModule,
    SharedModule,
    CorretorasModule,
    ClientesModule,
    AtivosModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true
    }),
    NgxSpinnerModule,
    BsDatepickerModule.forRoot()

  ],
  providers: [
    CorretoraService,
    ClienteService,
    CarteiraService,
    SetorService,
    SubsetorService,
    SegmentoService,
    AcaoService,
    TipodeinvestimentoService,
    AdministradordefundoimobiliarioService,
    ProventoService,
    FundoimobiliarioService,
    SegmentoanbimaService,
    TesourodiretoService,
    AtivoService,
    UserService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    { provide: DEFAULT_CURRENCY_CODE, useValue: 'BRL' },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
