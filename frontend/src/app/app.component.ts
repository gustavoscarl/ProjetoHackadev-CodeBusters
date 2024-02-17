import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { UserBalanceComponent } from './user-balance/user-balance.component';
import { ContainerComponentComponent } from './container-component/container-component.component';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, UserBalanceComponent, ContainerComponentComponent ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'frontend';
  //Criando nome de usuario
  nomeDoUsuario: string = 'Pessoal';
  // Criando saldo do usuario
  saldoDoUsuario: number = 20000;
}
