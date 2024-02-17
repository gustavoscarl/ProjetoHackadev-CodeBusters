import { Component, Input  } from '@angular/core';

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
}
