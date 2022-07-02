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
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { SetorDetalheComponent } from './components/setores/setor-detalhe/setor-detalhe.component';
import { SetoresComponent } from './components/setores/setores.component';
import { SetorListaComponent } from './components/setores/setor-lista/setor-lista.component';
import { SubsetoresListaComponent } from './components/setores/subsetores-lista/subsetores-lista.component';
import { SubsetorDetalheComponent } from './components/setores/subsetor-detalhe/subsetor-detalhe.component';
import { SegmentosListaComponent } from './components/setores/segmentos-lista/segmentos-lista.component';
import { SegmentoDetalheComponent } from './components/setores/segmento-detalhe/segmento-detalhe.component';
import { TiposdeinvestimentosComponent } from './components/tiposdeinvestimentos/tiposdeinvestimentos.component';
import { TiposdeinvestimentosListaComponent } from './components/tiposdeinvestimentos/tiposdeinvestimentos-lista/tiposdeinvestimentos-lista.component';
import { TiposdeinvestimentosDetalheComponent } from './components/tiposdeinvestimentos/tiposdeinvestimentos-detalhe/tiposdeinvestimentos-detalhe.component';
import { TesourosdiretosComponent } from './components/tesourosdiretos/tesourosdiretos.component';
import { TesourosdiretosListaComponent } from './components/tesourosdiretos/tesourosdiretos-lista/tesourosdiretos-lista.component';
import { TesourodiretoDetalheComponent } from './components/tesourosdiretos/tesourodireto-detalhe/tesourodireto-detalhe.component';
import { AdministradoresdefundosimobiliariosComponent } from './components/administradoresdefundosimobiliarios/administradoresdefundosimobiliarios.component';
import { AdministradoresdefundosimobiliariosListaComponent } from './components/administradoresdefundosimobiliarios/administradoresdefundosimobiliarios-lista/administradoresdefundosimobiliarios-lista.component';
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
import { AdministradoresdefundosimobiliariosDetalheComponent } from './components/administradoresdefundosimobiliarios/administradoresdefundosimobiliarios-detalhe/administradoresdefundosimobiliarios-detalhe.component';
import { SegmentosanbimasComponent } from './components/segmentosanbimas/segmentosanbimas.component';
import { SegmentosanbimasListaComponent } from './components/segmentosanbimas/segmentosanbimas-lista/segmentosanbimas-lista.component';
import { SegmentosanbimasDetalheComponent } from './components/segmentosanbimas/segmentosanbimas-detalhe/segmentosanbimas-detalhe.component';
import { SegmentoanbimaService } from './services/segmentoanbima.service';
import { FundoimobiliarioService } from './services/fundoimobiliario.service';
import { FundosimobiliariosComponent } from './components/fundosimobiliarios/fundosimobiliarios.component';
import { FundosimobiliariosListaComponent } from './components/fundosimobiliarios/fundosimobiliarios-lista/fundosimobiliarios-lista.component';
import { FundosimobiliariosDetalheComponent } from './components/fundosimobiliarios/fundosimobiliarios-detalhe/fundosimobiliarios-detalhe.component';
import { SalvarComponent } from './shared/salvar/salvar.component';
import { FiltrarComponent } from './shared/filtrar/filtrar.component';
import { AcoesComponent } from './components/acoes/acoes.component';
import { AcoesListaComponent } from './components/acoes/acoes-lista/acoes-lista.component';
import { AcoesDetalheComponent } from './components/acoes/acoes-detalhe/acoes-detalhe.component';
import { AcaoService } from './services/acao.service';
import { ProventosComponent } from './components/proventos/proventos.component';
import { ProventosListaComponent } from './components/proventos/proventos-lista/proventos-lista.component';
import { ProventosDetalheComponent } from './components/proventos/proventos-detalhe/proventos-detalhe.component';
import { ProventoService } from './services/provento.service';
import { CarteiraService } from './services/carteira.service';
import { ClienteCarteiraListaComponent } from './components/clientes/cliente-carteira-lista/cliente-carteira-lista.component';
import { ClienteCarteiraDetalheComponent } from './components/clientes/cliente-carteira-detalhe/cliente-carteira-detalhe.component';
import { FormDebugComponent } from './shared/form-debug/form-debug.component';

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
    CorretorasComponent,
    CorretoraDetalheComponent,
    SetoresComponent,
    SetorDetalheComponent,
    SetorListaComponent,
    SubsetoresListaComponent,
    SubsetorDetalheComponent,
    SegmentosListaComponent,
    SegmentoDetalheComponent,
    CorretoraListaComponent,
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
    FormDebugComponent,
    NavComponent,
    DateTimeFormatPipe,
    CnpjFormatPipe,
    CpfFormatPipe,
    TituloComponent,
    SalvarComponent,
    FiltrarComponent,
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
