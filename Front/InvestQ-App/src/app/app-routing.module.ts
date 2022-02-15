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

const routes: Routes = [
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent},
      { path: 'registration', component: RegistrationComponent}
    ]
  },
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
      { path: 'detalhe', component: SetorDetalheComponent },
      { path: 'lista', component: SetorListaComponent }
    ]
  },
  { path: 'contatos', component: ContatosComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
