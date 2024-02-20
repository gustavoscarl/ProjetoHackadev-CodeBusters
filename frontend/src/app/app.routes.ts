import { Routes } from '@angular/router';
import { ContainerComponentComponent } from './hompage/container-component/container-component.component';
import { LoginComponent } from './login/login.component';
import { CadastroComponent } from './cadastro/cadastro.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'home', component: ContainerComponentComponent},
  { path: 'cadastro', component: CadastroComponent},
  // Redireciona para 'login' se a rota é o caminho raiz ou não é reconhecida
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: 'login' }
];
