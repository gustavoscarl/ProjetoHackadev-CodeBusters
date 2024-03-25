import { Routes } from '@angular/router';
import { ContainerComponentComponent } from './hompage/container-component/container-component.component';
import { LoginComponent } from './login/login.component';
import { CadastroComponent } from './cadastro/cadastro.component';
import { HistoricoComponent } from './componentes/historico/historico.component';
import { EsquecerSenhaComponent } from './esquecer-senha/esquecer-senha.component';
import { DepositoComponent } from './deposito/deposito.component';
import { PixComponent } from './pix/pix.component';
import { PinPadComponent } from './pin-pad/pin-pad.component';
import { CardpageComponent } from './componentes/cardpage/cardpage.component';
import { authGuard } from './guards/auth.guard';
import { CriarContaComponent } from './conta/criar-conta/criar-conta.component';
import { ContaCriadaComponent } from './conta/conta-criada/conta-criada.component';
import { HistoricoPageComponent } from './componentes/historico-page/historico-page.component';
import { TransferenciaComponent } from './componentes/transferencia/transferencia.component';
import { SaqueComponent } from './componentes/saque/saque.component';
import { InvestimentosComponent } from './investimentos/investimentos.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'home', component: ContainerComponentComponent, canActivate: [authGuard]},
  {
    path: 'conta',
    children: [
      { path: 'criar-conta', component: CriarContaComponent, canActivate: [authGuard] },
      { path: 'criada', component: ContaCriadaComponent, canActivate: [authGuard] }
    ]
  },
  { path: 'cadastro', component: CadastroComponent},
  { path: 'esquecer-senha', component: EsquecerSenhaComponent},
  { path: 'historico', component: HistoricoPageComponent, canActivate: [authGuard]},
  { path: 'pix', component: PixComponent, canActivate: [authGuard]},
  { path: 'deposito', component: DepositoComponent, canActivate: [authGuard]},
  { path: 'pin-pad', component: PinPadComponent, canActivate: [authGuard] },
  { path: 'cartao', component: CardpageComponent, canActivate: [authGuard] },
  { path: 'transfer', component: TransferenciaComponent, canActivate: [authGuard] },
  { path: 'saque', component: SaqueComponent, canActivate: [authGuard] },
  { path: 'investimento', component: InvestimentosComponent, canActivate: [authGuard]},
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' }
];
