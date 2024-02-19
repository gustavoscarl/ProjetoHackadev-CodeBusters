import { Component, EventEmitter, Input, input } from '@angular/core';
import { Transacao } from '../../modelo/Transacoes';
import { CommonModule } from '@angular/common';
import { TransacaoComponent } from '../transacao/transacao.component';

@Component({
  selector: 'app-historico',
  standalone: true,
  imports: [CommonModule, TransacaoComponent],
  templateUrl: './historico.component.html',
  styleUrl: './historico.component.css'
})
export class HistoricoComponent {

    // Vetor de transacões - Onde as transações ficarão armazenadas no momento (sem backend)
  transacoes: Transacao[] = [
    { tipo:'Pix', valor:120, contaDestino:202020, data:'2024-01-28'},
    ];

  // FUNÇÃO QUE ADICIONA A TRANSAÇÃO NO VETOR DO HISTÓRICO.
  realizarTrasacao(obj:Transacao):void {
    this.transacoes.unshift(obj);
  }

  transacaoFiltrada: Transacao[] = [];

  filtroAno(ano: number){
    this.transacaoFiltrada = this.transacoes.filter(transacoes => {
      return new Date(transacoes.data).getFullYear() === ano;
    })
  }

}
