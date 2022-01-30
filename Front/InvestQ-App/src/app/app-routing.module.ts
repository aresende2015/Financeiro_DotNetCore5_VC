import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CorretorasComponent } from './components/corretoras/corretoras.component';
import { ClientesComponent } from './components/clientes/clientes.component';
import { PerfilComponent } from './components/perfil/perfil.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ContatosComponent } from './components/contatos/contatos.component';
import { ClienteDetalheComponent } from './components/clientes/cliente-detalhe/cliente-detalhe.component';
import { ClienteListaComponent } from './components/clientes/cliente-lista/cliente-lista.component';

const routes: Routes = [
  { path: 'corretoras', component: CorretorasComponent },
  {
    path: 'clientes', component: ClientesComponent,
    children: [
      { path: 'detalhe/:id', component: ClienteDetalheComponent },
      { path: 'detalhe', component: ClienteDetalheComponent },
      { path: 'lista', component: ClienteListaComponent }
    ]
  },
  { path: 'contatos', component: ContatosComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'perfil', component: PerfilComponent },
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
