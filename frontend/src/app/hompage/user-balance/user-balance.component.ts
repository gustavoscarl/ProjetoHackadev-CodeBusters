import { Component, Inject, Input  } from '@angular/core';
import { InputserviceService } from '../../servicos/inputservice.service';
@Inject({ providedIn: 'root' })

@Component({
  selector: 'app-user-balance',
  standalone: true,
  imports: [],
  templateUrl: './user-balance.component.html',
  styleUrl: './user-balance.component.css'
})
export class UserBalanceComponent {
  @Input() saldoAtual: number = 0;
  @Input() userName!: string;
  constructor(private inputService: InputserviceService) {
    this.saldoAtual = this.inputService.saldoDoUsuario;
    this.userName = this.inputService.nomeDoUsuario;
  }
}
