import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { UserGreetingsComponent } from './user-greetings/user-greetings.component';
import { UserBalanceComponent } from './user-balance/user-balance.component';
import { ContainerComponentComponent } from './container-component/container-component.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, UserGreetingsComponent, UserBalanceComponent, ContainerComponentComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'frontend';
  nomeDoUsuario: string = 'Pessoal';
  saldoDoUsuario: number = 20000;
}
