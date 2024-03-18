import { Routes } from '@angular/router';
import { ContainerComponentComponent } from './hompage/container-component/container-component.component';
import { LoginComponent } from './login/login.component';
import { CadastroComponent } from './cadastro/cadastro.component';
import { HistoricoComponent } from './componentes/historico/historico.component';
import { TransacaoComponent } from './componentes/transacao/transacao.component';
import { EsquecerSenhaComponent } from './esquecer-senha/esquecer-senha.component';
import { DepositoComponent } from './deposito/deposito.component';
import { PixComponent } from './pix/pix.component';
import { PinPadComponent } from './pin-pad/pin-pad.component';
import { CardpageComponent } from './componentes/cardpage/cardpage.component';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'home', component: ContainerComponentComponent, canActivate: [authGuard]},
  { path: 'cadastro', component: CadastroComponent},
  { path: 'esquecer-senha', component: EsquecerSenhaComponent},
  { path: 'historico', component: HistoricoComponent},
  { path: 'transacao', component: TransacaoComponent},
  { path: 'pix', component: PixComponent},
  { path: 'deposito', component: DepositoComponent},
  { path: 'pin-pad', component: PinPadComponent },
  { path: 'cartao', component: CardpageComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: 'login' }
];
