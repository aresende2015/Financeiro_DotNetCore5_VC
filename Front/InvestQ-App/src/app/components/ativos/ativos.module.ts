import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxCurrencyModule } from 'ngx-currency';
import { AppRoutingModule } from '@app/app-routing.module';
import { HelpersModule } from '@app/helpers/helpers.module';
import { SharedModule } from '@app/shared/shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TiposdeinvestimentosComponent } from './tiposdeinvestimentos/tiposdeinvestimentos.component';
import { TiposdeinvestimentosListaComponent } from './tiposdeinvestimentos/tiposdeinvestimentos-lista/tiposdeinvestimentos-lista.component';
import { TiposdeinvestimentosDetalheComponent } from './tiposdeinvestimentos/tiposdeinvestimentos-detalhe/tiposdeinvestimentos-detalhe.component';
import { TesourosdiretosComponent } from './tesourosdiretos/tesourosdiretos.component';
import { TesourosdiretosListaComponent } from './tesourosdiretos/tesourosdiretos-lista/tesourosdiretos-lista.component';
import { TesourodiretoDetalheComponent } from './tesourosdiretos/tesourodireto-detalhe/tesourodireto-detalhe.component';
import { ProventosComponent } from './proventos/proventos.component';
import { ProventosListaComponent } from './proventos/proventos-lista/proventos-lista.component';
import { ProventosDetalheComponent } from './proventos/proventos-detalhe/proventos-detalhe.component';
import { FundosimobiliariosComponent } from './fundosimobiliarios/fundosimobiliarios.component';
import { FundosimobiliariosListaComponent } from './fundosimobiliarios/fundosimobiliarios-lista/fundosimobiliarios-lista.component';
import { FundosimobiliariosDetalheComponent } from './fundosimobiliarios/fundosimobiliarios-detalhe/fundosimobiliarios-detalhe.component';
import { AdministradoresdefundosimobiliariosComponent } from './fundosimobiliarios/administradoresdefundosimobiliarios/administradoresdefundosimobiliarios.component';
import { AdministradoresdefundosimobiliariosListaComponent } from './fundosimobiliarios/administradoresdefundosimobiliarios/administradoresdefundosimobiliarios-lista/administradoresdefundosimobiliarios-lista.component';
import { AdministradoresdefundosimobiliariosDetalheComponent } from './fundosimobiliarios/administradoresdefundosimobiliarios/administradoresdefundosimobiliarios-detalhe/administradoresdefundosimobiliarios-detalhe.component';
import { SegmentosanbimasComponent } from './fundosimobiliarios/segmentosanbimas/segmentosanbimas.component';
import { SegmentosanbimasListaComponent } from './fundosimobiliarios/segmentosanbimas/segmentosanbimas-lista/segmentosanbimas-lista.component';
import { SegmentosanbimasDetalheComponent } from './fundosimobiliarios/segmentosanbimas/segmentosanbimas-detalhe/segmentosanbimas-detalhe.component';
import { SetoresComponent } from './acoes/setores/setores.component';
import { SetorDetalheComponent } from './acoes/setores/setor-detalhe/setor-detalhe.component';
import { SetorListaComponent } from './acoes/setores/setor-lista/setor-lista.component';
import { SubsetoresListaComponent } from './acoes/setores/subsetores-lista/subsetores-lista.component';
import { SubsetorDetalheComponent } from './acoes/setores/subsetor-detalhe/subsetor-detalhe.component';
import { SegmentosListaComponent } from './acoes/setores/segmentos-lista/segmentos-lista.component';
import { SegmentoDetalheComponent } from './acoes/setores/segmento-detalhe/segmento-detalhe.component';
import { AcoesComponent } from './acoes/acoes.component';
import { AcoesListaComponent } from './acoes/acoes-lista/acoes-lista.component';
import { AcoesDetalheComponent } from './acoes/acoes-detalhe/acoes-detalhe.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgxCurrencyModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HelpersModule,
    SharedModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
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
  declarations: [
    TiposdeinvestimentosComponent,
    TiposdeinvestimentosListaComponent,
    TiposdeinvestimentosDetalheComponent,
    TesourosdiretosComponent,
    TesourosdiretosListaComponent,
    TesourodiretoDetalheComponent,
    ProventosComponent,
    ProventosListaComponent,
    ProventosDetalheComponent,
    FundosimobiliariosComponent,
    FundosimobiliariosListaComponent,
    FundosimobiliariosDetalheComponent,
    AdministradoresdefundosimobiliariosComponent,
    AdministradoresdefundosimobiliariosListaComponent,
    AdministradoresdefundosimobiliariosDetalheComponent,
    SegmentosanbimasComponent,
    SegmentosanbimasListaComponent,
    SegmentosanbimasDetalheComponent,
    SetoresComponent,
    SetorDetalheComponent,
    SetorListaComponent,
    SubsetoresListaComponent,
    SubsetorDetalheComponent,
    SegmentosListaComponent,
    SegmentoDetalheComponent,
    AcoesComponent,
    AcoesListaComponent,
    AcoesDetalheComponent,
  ]
})
export class AtivosModule { }
