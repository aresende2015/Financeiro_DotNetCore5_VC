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
import { SetoresComponent } from './components/setores/setores.component';
import { SetorDetalheComponent } from './components/setores/setor-detalhe/setor-detalhe.component';
import { SetorListaComponent } from './components/setores/setor-lista/setor-lista.component';
import { SubsetoresListaComponent } from './components/setores/subsetores-lista/subsetores-lista.component';
import { SegmentosListaComponent } from './components/setores/segmentos-lista/segmentos-lista.component';
import { SubsetorDetalheComponent } from './components/setores/subsetor-detalhe/subsetor-detalhe.component';
import { SegmentoDetalheComponent } from './components/setores/segmento-detalhe/segmento-detalhe.component';
import { AdministradoresdefundosimobiliariosComponent } from './components/administradoresdefundosimobiliarios/administradoresdefundosimobiliarios.component';
import { AdministradoresdefundosimobiliariosListaComponent } from './components/administradoresdefundosimobiliarios/administradoresdefundosimobiliarios-lista/administradoresdefundosimobiliarios-lista.component';
import { TiposdeinvestimentosComponent } from './components/tiposdeinvestimentos/tiposdeinvestimentos.component';
import { TiposdeinvestimentosListaComponent } from './components/tiposdeinvestimentos/tiposdeinvestimentos-lista/tiposdeinvestimentos-lista.component';
import { TiposdeinvestimentosDetalheComponent } from './components/tiposdeinvestimentos/tiposdeinvestimentos-detalhe/tiposdeinvestimentos-detalhe.component';
import { TesourosdiretosComponent } from './components/tesourosdiretos/tesourosdiretos.component';
import { TesourosdiretosListaComponent } from './components/tesourosdiretos/tesourosdiretos-lista/tesourosdiretos-lista.component';
import { TesourodiretoDetalheComponent } from './components/tesourosdiretos/tesourodireto-detalhe/tesourodireto-detalhe.component';
import { AuthGuard } from './guard/auth.guard';
import { HomeComponent } from './components/home/home.component';
import { AdministradoresdefundosimobiliariosDetalheComponent } from './components/administradoresdefundosimobiliarios/administradoresdefundosimobiliarios-detalhe/administradoresdefundosimobiliarios-detalhe.component';
import { SegmentosanbimasComponent } from './components/segmentosanbimas/segmentosanbimas.component';
import { SegmentosanbimasDetalheComponent } from './components/segmentosanbimas/segmentosanbimas-detalhe/segmentosanbimas-detalhe.component';
import { SegmentosanbimasListaComponent } from './components/segmentosanbimas/segmentosanbimas-lista/segmentosanbimas-lista.component';
import { FundosimobiliariosComponent } from './components/fundosimobiliarios/fundosimobiliarios.component';
import { FundosimobiliariosDetalheComponent } from './components/fundosimobiliarios/fundosimobiliarios-detalhe/fundosimobiliarios-detalhe.component';
import { FundosimobiliariosListaComponent } from './components/fundosimobiliarios/fundosimobiliarios-lista/fundosimobiliarios-lista.component';

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
      { path: 'clientes', redirectTo: 'clientes/lista' },
      {
        path: 'clientes', component: ClientesComponent,
        children: [
          { path: 'detalhe/:id', component: ClienteDetalheComponent },
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
