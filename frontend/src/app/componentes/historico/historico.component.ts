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
    { tipo:'Pix', valor:120, contaDestino:202020, data:'01/02/2024' },
    ];

  // FUNCAO TRANSACAO
  realizarTrasacao(obj:Transacao):void {
    this.transacoes.push(obj);
  }

}
