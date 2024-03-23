import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-transferencia',
  standalone: true,
  imports: [FormsModule, RouterModule],
  templateUrl: './transferencia.component.html',
  styleUrl: './transferencia.component.css'
})

export class TransferenciaComponent {

  valor: number | undefined;
  contaDestino: number | undefined;
  descricao: string | undefined;

  constructor() { }

  realizarTransferencia(): void {

    console.log('Transação realizada:', { valor: this.valor, ContaDestino: this.contaDestino, descricao: this.descricao });

  }
}