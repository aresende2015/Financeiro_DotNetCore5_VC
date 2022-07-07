import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CorretorasComponent } from './components/corretoras/corretoras.component';
import { ClientesComponent } from './components/clientes/clientes.component';
import { PerfilComponent } from './components/user/perfil/perfil.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ContatosComponent } from './components/contatos/contatos.component';
import { ClienteDetalheComponent } from './components/clientes/cliente-detalhe/cliente-detalhe.component';
import { ClienteListaComponent } from './components/clientes/cliente-lista/cliente-lista.component';
import { CorretoraListaComponent } from './components/corretoras/corretora-lista/corretora-lista.component';
import { CorretoraDetalheComponent } from './components/corretoras/corretora-detalhe/corretora-detalhe.component';
import { UserComponent } from './components/user/user.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { LoginComponent } from './components/user/login/login.component';
import { SetoresComponent } from './components/ativos/acoes/setores/setores.component';
import { SetorDetalheComponent } from './components/ativos/acoes/setores/setor-detalhe/setor-detalhe.component';
import { SetorListaComponent } from './components/ativos/acoes/setores/setor-lista/setor-lista.component';
import { SubsetoresListaComponent } from './components/ativos/acoes/setores/subsetores-lista/subsetores-lista.component';
import { SegmentosListaComponent } from './components/ativos/acoes/setores/segmentos-lista/segmentos-lista.component';
import { SubsetorDetalheComponent } from './components/ativos/acoes/setores/subsetor-detalhe/subsetor-detalhe.component';
import { SegmentoDetalheComponent } from './components/ativos/acoes/setores/segmento-detalhe/segmento-detalhe.component';
import { AdministradoresdefundosimobiliariosComponent } from './components/ativos/fundosimobiliarios/administradoresdefundosimobiliarios/administradoresdefundosimobiliarios.component';
import { AdministradoresdefundosimobiliariosListaComponent } from './components/ativos/fundosimobiliarios/administradoresdefundosimobiliarios/administradoresdefundosimobiliarios-lista/administradoresdefundosimobiliarios-lista.component';
import { TiposdeinvestimentosComponent } from './components/ativos/tiposdeinvestimentos/tiposdeinvestimentos.component';
import { TiposdeinvestimentosListaComponent } from './components/ativos/tiposdeinvestimentos/tiposdeinvestimentos-lista/tiposdeinvestimentos-lista.component';
import { TiposdeinvestimentosDetalheComponent } from './components/ativos/tiposdeinvestimentos/tiposdeinvestimentos-detalhe/tiposdeinvestimentos-detalhe.component';
import { TesourosdiretosComponent } from './components/ativos/tesourosdiretos/tesourosdiretos.component';
import { TesourosdiretosListaComponent } from './components/ativos/tesourosdiretos/tesourosdiretos-lista/tesourosdiretos-lista.component';
import { TesourodiretoDetalheComponent } from './components/ativos/tesourosdiretos/tesourodireto-detalhe/tesourodireto-detalhe.component';
import { AuthGuard } from './guard/auth.guard';
import { HomeComponent } from './components/home/home.component';
import { AdministradoresdefundosimobiliariosDetalheComponent } from './components/ativos/fundosimobiliarios/administradoresdefundosimobiliarios/administradoresdefundosimobiliarios-detalhe/administradoresdefundosimobiliarios-detalhe.component';
import { SegmentosanbimasComponent } from './components/ativos/fundosimobiliarios/segmentosanbimas/segmentosanbimas.component';
import { SegmentosanbimasDetalheComponent } from './components/ativos/fundosimobiliarios/segmentosanbimas/segmentosanbimas-detalhe/segmentosanbimas-detalhe.component';
import { SegmentosanbimasListaComponent } from './components/ativos/fundosimobiliarios/segmentosanbimas/segmentosanbimas-lista/segmentosanbimas-lista.component';
import { FundosimobiliariosComponent } from './components/ativos/fundosimobiliarios/fundosimobiliarios.component';
import { FundosimobiliariosDetalheComponent } from './components/ativos/fundosimobiliarios/fundosimobiliarios-detalhe/fundosimobiliarios-detalhe.component';
import { FundosimobiliariosListaComponent } from './components/ativos/fundosimobiliarios/fundosimobiliarios-lista/fundosimobiliarios-lista.component';
import { AcoesComponent } from './components/ativos/acoes/acoes.component';
import { AcoesDetalheComponent } from './components/ativos/acoes/acoes-detalhe/acoes-detalhe.component';
import { AcoesListaComponent } from './components/ativos/acoes/acoes-lista/acoes-lista.component';
import { ProventosComponent } from './components/ativos/proventos/proventos.component';
import { ProventosDetalheComponent } from './components/ativos/proventos/proventos-detalhe/proventos-detalhe.component';
import { ProventosListaComponent } from './components/ativos/proventos/proventos-lista/proventos-lista.component';
import { ClienteCarteiraDetalheComponent } from './components/clientes/cliente-carteira-detalhe/cliente-carteira-detalhe.component';
import { ClienteCarteiraListaComponent } from './components/clientes/cliente-carteira-lista/cliente-carteira-lista.component';
import { LancamentosComponent } from './components/clientes/lancamentos/lancamentos.component';
import { LancamentoDetalheComponent } from './components/clientes/lancamentos/lancamento-detalhe/lancamento-detalhe.component';
import { LancamentosListaComponent } from './components/clientes/lancamentos/lancamentos-lista/lancamentos-lista.component';
import { LancamentoFiltroListaComponent } from './components/clientes/lancamentos/lancamento-filtro-lista/lancamento-filtro-lista.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'user', redirectTo: 'user/perfil'},
      {
        path: 'user/perfil', component: PerfilComponent
      },
      { path: 'corretoras', redirectTo: 'corretoras/lista' },
      {
        path: 'corretoras', component: CorretorasComponent,
        children: [
          { path: 'detalhe/:id', component: CorretoraDetalheComponent },
          { path: 'detalhe', component: CorretoraDetalheComponent },
          { path: 'lista', component: CorretoraListaComponent }
        ]
      },
      { path: 'lancamentos', redirectTo: 'lancamentos/filtro/lista' },
      { path: 'lancamentos/lista', redirectTo: 'lancamentos/filtro/lista' },
      {
        path: 'lancamentos', component: LancamentosComponent,
        children: [
          { path: 'filtro/lista', component: LancamentoFiltroListaComponent },
          { path: 'detalhe/:carteiraid/:id', component: LancamentoDetalheComponent },
          { path: 'detalhe/:carteiraid', component: LancamentoDetalheComponent },
          { path: 'detalhe', component: LancamentoDetalheComponent },
          { path: 'lista/:id', component: LancamentosListaComponent }
        ]
      },
      { path: 'clientes', redirectTo: 'clientes/lista' },
      {
        path: 'clientes', component: ClientesComponent,
        children: [
          { path: 'detalhe/:id', component: ClienteDetalheComponent },
          { path: 'carteiradetalhe/:clienteid/:id', component: ClienteCarteiraDetalheComponent },
          { path: 'carteiradetalhe/:clienteid', component: ClienteCarteiraDetalheComponent },
          { path: 'carteiradetalhe', component: ClienteCarteiraDetalheComponent },
          { path: 'listarcarteiras/:id', component: ClienteCarteiraListaComponent },
          { path: 'detalhe', component: ClienteDetalheComponent },
          { path: 'lista', component: ClienteListaComponent }
        ]
      },
      { path: 'setores', redirectTo: 'setores/lista' },
      {
        path: 'setores', component: SetoresComponent,
        children: [
          { path: 'detalhe/:id', component: SetorDetalheComponent },
          { path: 'subsetordetalhe/:id', component: SubsetorDetalheComponent },
          { path: 'listarsubsetores/:id', component: SubsetoresListaComponent },
          { path: 'listarsegmentos/:id', component: SegmentosListaComponent },
          { path: 'segmentodetalhe/:subsetorId/:id', component: SegmentoDetalheComponent },
          { path: 'detalhe', component: SetorDetalheComponent },
          { path: 'lista', component: SetorListaComponent }
        ]
      },
      { path: 'tiposdeinvestimentos', redirectTo: 'tiposdeinvestimentos/lista' },
      {
        path: 'tiposdeinvestimentos', component: TiposdeinvestimentosComponent,
        children: [
          { path: 'detalhe/:id', component: TiposdeinvestimentosDetalheComponent },
          { path: 'detalhe', component: TiposdeinvestimentosDetalheComponent },
          { path: 'lista', component: TiposdeinvestimentosListaComponent }
        ]
      },
      { path: 'administradoresdefundosimobiliarios', redirectTo: 'administradoresdefundosimobiliarios/lista' },
      {
        path: 'administradoresdefundosimobiliarios', component: AdministradoresdefundosimobiliariosComponent,
        children: [
          { path: 'detalhe/:id', component: AdministradoresdefundosimobiliariosDetalheComponent },
          { path: 'detalhe', component: AdministradoresdefundosimobiliariosDetalheComponent },
          { path: 'lista', component: AdministradoresdefundosimobiliariosListaComponent }
        ]
      },
      { path: 'segmentosanbimas', redirectTo: 'segmentosanbimas/lista' },
      {
        path: 'segmentosanbimas', component: SegmentosanbimasComponent,
        children: [
          { path: 'detalhe/:id', component: SegmentosanbimasDetalheComponent },
          { path: 'detalhe', component: SegmentosanbimasDetalheComponent },
          { path: 'lista', component: SegmentosanbimasListaComponent }
        ]
      },
      { path: 'tesourosdiretos', redirectTo: 'tesourosdiretos/lista' },
      {
        path: 'tesourosdiretos', component: TesourosdiretosComponent,
        children: [
          { path: 'detalhe/:id', component: TesourodiretoDetalheComponent },
          { path: 'detalhe', component: TesourodiretoDetalheComponent },
          { path: 'lista', component: TesourosdiretosListaComponent }
        ]
      },
      { path: 'fundosimobiliarios', redirectTo: 'fundosimobiliarios/lista' },
      {
        path: 'fundosimobiliarios', component: FundosimobiliariosComponent,
        children: [
          { path: 'detalhe/:id', component: FundosimobiliariosDetalheComponent },
          { path: 'detalhe', component: FundosimobiliariosDetalheComponent },
          { path: 'lista', component: FundosimobiliariosListaComponent }
        ]
      },
      { path: 'acoes', redirectTo: 'acoes/lista' },
      {
        path: 'acoes', component: AcoesComponent,
        children: [
          { path: 'detalhe/:id', component: AcoesDetalheComponent },
          { path: 'detalhe', component: AcoesDetalheComponent },
          { path: 'lista', component: AcoesListaComponent }
        ]
      },
      { path: 'proventos', redirectTo: 'proventos/lista' },
      {
        path: 'proventos', component: ProventosComponent,
        children: [
          { path: 'detalhe/:id', component: ProventosDetalheComponent },
          { path: 'detalhe', component: ProventosDetalheComponent },
          { path: 'lista', component: ProventosListaComponent }
        ]
      },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'contatos', component: ContatosComponent },
    ]
  },
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent},
      { path: 'registration', component: RegistrationComponent}
    ]
  },
  { path: 'home', component: HomeComponent },
  { path: '**', redirectTo: 'home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
