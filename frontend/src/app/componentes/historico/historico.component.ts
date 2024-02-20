import { Component, EventEmitter, Input, input } from '@angular/core';
import { Transacao } from '../../modelo/Transacoes';
import { CommonModule } from '@angular/common';
import { TransacaoComponent } from '../transacao/transacao.component';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-historico',
  standalone: true,
  imports: [CommonModule, TransacaoComponent],
  templateUrl: './historico.component.html',
  styleUrl: './historico.component.css'
})


export class HistoricoComponent {

  ngOnInit() {
    this.ordenarTransacoes();
  }
  

    // Vetor de transacões - Onde as transações ficarão armazenadas no momento (sem backend)
  transacoes: Transacao[] = [
    { tipo:'Pix', valor:120, contaDestino:123123, data:'2024-01-28'},
    { tipo:'Debito', valor:15, contaDestino:111111, data:'2024-01-29'},
    { tipo:'Credito', valor:1000, contaDestino:222222, data:'2024-01-30'},
    { tipo:'Pix', valor:120, contaDestino:123123, data:'2024-01-28'},
    { tipo:'Debito', valor:15, contaDestino:111111, data:'2024-01-29'},
    { tipo:'Credito', valor:1000, contaDestino:222222, data:'2024-01-30'},
    ];

  // FUNÇÃO QUE ADICIONA A TRANSAÇÃO NO VETOR DO HISTÓRICO.
  realizarTrasacao(obj:Transacao):void {
    this.transacoes.unshift(obj);
  }

  // HISTÓRICO EXIBIDO, ORGANIZADO POR DIA.

  historicotransacoes: Transacao[] = [];

  ordenarTransacoes(){
    this.transacoes.sort((a,b) => {
      const dataA = new Date(a.data).getTime();
      const dataB = new Date(b.data).getTime();
      return dataB - dataA;
    })
  }


  transacaoFiltrada: Transacao[] = [];

  filtroAno(ano: number){
    this.transacaoFiltrada = this.transacoes.filter(transacoes => {
      return new Date(transacoes.data).getFullYear() === ano;
    })
  }

}
