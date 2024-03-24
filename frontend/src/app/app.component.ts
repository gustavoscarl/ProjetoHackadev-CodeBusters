import { Router, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';

import { UserBalanceComponent } from './hompage/user-balance/user-balance.component';
import { ContainerComponentComponent } from './hompage/container-component/container-component.component';
import { IconsHomeComponent } from "./hompage/icons-home/icons-home.component";
import { HeaderComponent } from './header/header.component';
import { AuthService } from './auth.service';
import { LoginComponent } from './login/login.component';
import { Component } from '@angular/core';
import { ContaCriadaComponent } from './conta/conta-criada/conta-criada.component';
import { CriarContaComponent } from './conta/criar-conta/criar-conta.component';
import { InvestimentosComponent } from './investimentos/investimentos.component';
import { SaqueComponent } from './componentes/saque/saque.component';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [CommonModule, RouterOutlet, UserBalanceComponent, ContainerComponentComponent, IconsHomeComponent, HeaderComponent, LoginComponent, ContaCriadaComponent, CriarContaComponent, InvestimentosComponent, SaqueComponent],
    providers: [AuthService]
})

export class AppComponent {
    constructor(private router: Router) {}

    isLoginPage(): boolean {
      return this.router.url === '/login' || this.router.url === '/esquecer-senha' || this.router.url === '/cadastro';
    }
}
