import { Component, EventEmitter, Input, input } from '@angular/core';
import { Transacao } from '../../modelo/Transacoes';
import { CommonModule } from '@angular/common';
import { TransacaoComponent } from '../transacao/transacao.component';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-historico',
  standalone: true,
  imports: [CommonModule, TransacaoComponent, RouterModule],
  templateUrl: './historico.component.html',
  styleUrl: './historico.component.css'
})


export class HistoricoComponent {

  ngOnInit() {
    this.ordenarTransacoes();
  }
  

    // Vetor de transacões - Onde as transações ficarão armazenadas no momento (sem backend)
  transacoes: Transacao[] = [
    { nome:'Code Busters', descricao:'Compra de aplicat...', tipo:'Pix', valor:'R$ 120,00', contaDestino:123123, data:'2024-01-28'},
    { nome:'Amazon', descricao:'Campanha publici...', tipo:'Debito', valor:'R$ 15,00', contaDestino:111111, data:'2024-01-29'},
    { nome:'Code Busters', descricao:'Compra de aplicat...', tipo:'Credito', valor:'R$ 1000,00', contaDestino:222222, data:'2024-01-30'},
    { nome:'Amazon', descricao:'Campanha publici...', tipo:'Pix', valor:'R$ 120,00', contaDestino:123123, data:'2024-01-28'},
    { nome:'Spotify', descricao:'Compra de aplicat...', tipo:'Debito', valor:'R$ 120,00', contaDestino:111111, data:'2024-01-29'},
    { nome:'Code Busters', descricao:'Compra de aplicat...', tipo:'Credito', valor:'R$ 120,00', contaDestino:222222, data:'2024-01-30'},
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
