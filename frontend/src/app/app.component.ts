import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { UserBalanceComponent } from './user-balance/user-balance.component';
import { ContainerComponentComponent } from './container-component/container-component.component';
import { IconsHomeComponent } from "./icons-home/icons-home.component";


@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [CommonModule, RouterOutlet, UserBalanceComponent, ContainerComponentComponent, IconsHomeComponent]
})
export class AppComponent {
  title = 'frontend';
  //Criando nome de usuario
  nomeDoUsuario: string = 'Pessoal';
  // Criando saldo do usuario
  saldoDoUsuario: number = 20000;
}
