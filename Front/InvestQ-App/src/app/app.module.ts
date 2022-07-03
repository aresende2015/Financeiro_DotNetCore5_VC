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

import { DateTimeFormatPipe } from './helpers/DateTimeFormat.pipe';
import { CnpjFormatPipe } from './helpers/CnpjFormat.pipe';
import { CpfFormatPipe } from './helpers/CpfFormat.pipe';

import ptBr from '@angular/common/locales/pt'
import { registerLocaleData } from '@angular/common';

import { JwtInterceptor } from './interceptors/jwt.interceptor';

import { AppComponent } from './app.component';
import { ClientesComponent } from './components/clientes/clientes.component';
import { PerfilComponent } from './components/user/perfil/perfil.component';
import { ContatosComponent } from './components/contatos/contatos.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ClienteDetalheComponent } from './components/clientes/cliente-detalhe/cliente-detalhe.component';
import { ClienteListaComponent } from './components/clientes/cliente-lista/cliente-lista.component';
import { UserComponent } from './components/user/user.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { SetorDetalheComponent } from './components/ativos/acoes/setores/setor-detalhe/setor-detalhe.component';
import { SetoresComponent } from './components/ativos/acoes/setores/setores.component';
import { SetorListaComponent } from './components/ativos/acoes/setores/setor-lista/setor-lista.component';
import { SubsetoresListaComponent } from './components/ativos/acoes/setores/subsetores-lista/subsetores-lista.component';
import { SubsetorDetalheComponent } from './components/ativos/acoes/setores/subsetor-detalhe/subsetor-detalhe.component';
import { SegmentosListaComponent } from './components/ativos/acoes/setores/segmentos-lista/segmentos-lista.component';
import { SegmentoDetalheComponent } from './components/ativos/acoes/setores/segmento-detalhe/segmento-detalhe.component';
import { TiposdeinvestimentosComponent } from './components/ativos/tiposdeinvestimentos/tiposdeinvestimentos.component';
import { TiposdeinvestimentosListaComponent } from './components/ativos/tiposdeinvestimentos/tiposdeinvestimentos-lista/tiposdeinvestimentos-lista.component';
import { TiposdeinvestimentosDetalheComponent } from './components/ativos/tiposdeinvestimentos/tiposdeinvestimentos-detalhe/tiposdeinvestimentos-detalhe.component';
import { TesourosdiretosComponent } from './components/ativos/tesourosdiretos/tesourosdiretos.component';
import { TesourosdiretosListaComponent } from './components/ativos/tesourosdiretos/tesourosdiretos-lista/tesourosdiretos-lista.component';
import { TesourodiretoDetalheComponent } from './components/ativos/tesourosdiretos/tesourodireto-detalhe/tesourodireto-detalhe.component';
import { AdministradoresdefundosimobiliariosComponent } from './components/ativos/fundosimobiliarios/administradoresdefundosimobiliarios/administradoresdefundosimobiliarios.component';
import { AdministradoresdefundosimobiliariosListaComponent } from './components/ativos/fundosimobiliarios/administradoresdefundosimobiliarios/administradoresdefundosimobiliarios-lista/administradoresdefundosimobiliarios-lista.component';
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
import { AdministradoresdefundosimobiliariosDetalheComponent } from './components/ativos/fundosimobiliarios/administradoresdefundosimobiliarios/administradoresdefundosimobiliarios-detalhe/administradoresdefundosimobiliarios-detalhe.component';
import { SegmentosanbimasComponent } from './components/ativos/fundosimobiliarios/segmentosanbimas/segmentosanbimas.component';
import { SegmentosanbimasListaComponent } from './components/ativos/fundosimobiliarios/segmentosanbimas/segmentosanbimas-lista/segmentosanbimas-lista.component';
import { SegmentosanbimasDetalheComponent } from './components/ativos/fundosimobiliarios/segmentosanbimas/segmentosanbimas-detalhe/segmentosanbimas-detalhe.component';
import { SegmentoanbimaService } from './services/segmentoanbima.service';
import { FundoimobiliarioService } from './services/fundoimobiliario.service';
import { FundosimobiliariosComponent } from './components/ativos/fundosimobiliarios/fundosimobiliarios.component';
import { FundosimobiliariosListaComponent } from './components/ativos/fundosimobiliarios/fundosimobiliarios-lista/fundosimobiliarios-lista.component';
import { FundosimobiliariosDetalheComponent } from './components/ativos/fundosimobiliarios/fundosimobiliarios-detalhe/fundosimobiliarios-detalhe.component';
import { AcoesComponent } from './components/ativos/acoes/acoes.component';
import { AcoesListaComponent } from './components/ativos/acoes/acoes-lista/acoes-lista.component';
import { AcoesDetalheComponent } from './components/ativos/acoes/acoes-detalhe/acoes-detalhe.component';
import { AcaoService } from './services/acao.service';
import { ProventosComponent } from './components/ativos/proventos/proventos.component';
import { ProventosListaComponent } from './components/ativos/proventos/proventos-lista/proventos-lista.component';
import { ProventosDetalheComponent } from './components/ativos/proventos/proventos-detalhe/proventos-detalhe.component';
import { ProventoService } from './services/provento.service';
import { CarteiraService } from './services/carteira.service';
import { ClienteCarteiraListaComponent } from './components/clientes/cliente-carteira-lista/cliente-carteira-lista.component';
import { ClienteCarteiraDetalheComponent } from './components/clientes/cliente-carteira-detalhe/cliente-carteira-detalhe.component';
import { SharedModule } from './shared/shared.module';
import { CorretorasModule } from './components/corretoras/corretoras.module';

defineLocale('pt-br', ptBrLocale);
registerLocaleData(ptBr);
@NgModule({
  declarations: [
    AppComponent,
    AdministradoresdefundosimobiliariosComponent,
    AdministradoresdefundosimobiliariosListaComponent,
    AdministradoresdefundosimobiliariosDetalheComponent,
    SegmentosanbimasComponent,
    SegmentosanbimasListaComponent,
    SegmentosanbimasDetalheComponent,
    ClientesComponent,
    ClienteDetalheComponent,
    ClienteListaComponent,
    ClienteCarteiraListaComponent,
    ClienteCarteiraDetalheComponent,
    SetoresComponent,
    SetorDetalheComponent,
    SetorListaComponent,
    SubsetoresListaComponent,
    SubsetorDetalheComponent,
    SegmentosListaComponent,
    SegmentoDetalheComponent,
    TiposdeinvestimentosComponent,
    TiposdeinvestimentosListaComponent,
    TiposdeinvestimentosDetalheComponent,
    ProventosComponent,
    ProventosListaComponent,
    ProventosDetalheComponent,
    AcoesComponent,
    AcoesListaComponent,
    AcoesDetalheComponent,
    FundosimobiliariosComponent,
    FundosimobiliariosListaComponent,
    FundosimobiliariosDetalheComponent,
    TesourosdiretosComponent,
    TesourosdiretosListaComponent,
    TesourodiretoDetalheComponent,
    ContatosComponent,
    PerfilComponent,
    DashboardComponent,
    DateTimeFormatPipe,
    CnpjFormatPipe,
    CpfFormatPipe,
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
    SharedModule,
    CorretorasModule,
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
