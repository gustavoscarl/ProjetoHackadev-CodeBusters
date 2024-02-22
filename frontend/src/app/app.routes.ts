import { Routes } from '@angular/router';
import { ContainerComponentComponent } from './hompage/container-component/container-component.component';
import { LoginComponent } from './login/login.component';
import { CadastroComponent } from './cadastro/cadastro.component';
import { HistoricoComponent } from './componentes/historico/historico.component';
import { TransacaoComponent } from './componentes/transacao/transacao.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'home', component: ContainerComponentComponent},
  { path: 'cadastro', component: CadastroComponent},
  { path: 'historico', component: HistoricoComponent},
  { path: 'transacao', component: TransacaoComponent},
  // Redireciona para 'login' se a rota é o caminho raiz ou não é reconhecida
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: 'login' }
];
