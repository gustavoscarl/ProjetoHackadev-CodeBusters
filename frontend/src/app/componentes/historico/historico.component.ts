import { Component, Input, input } from '@angular/core';
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
    { tipo:'Credito', valor:1200, contaDestino:101010, data:'31/01/2024' },
    { tipo:'Débito', valor:20, contaDestino:303030, data:'30/01/2024' },
    { tipo:'Credito', valor:600, contaDestino:404040, data:'29/01/2024' },
    { tipo:'Pix', valor:19.9, contaDestino:505050, data:'28/01/2024' }
  ];

}
