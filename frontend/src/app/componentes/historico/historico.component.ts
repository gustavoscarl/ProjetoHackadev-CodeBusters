import { Component, EventEmitter, Input, input } from '@angular/core';
import { Transacao } from '../../modelo/Transacoes';
import { CommonModule } from '@angular/common';
import { TransacaoComponent } from '../transacao/transacao.component';

import { FormsModule } from '@angular/forms';
import { ChartModule } from 'primeng/chart';
import { CalendarModule } from 'primeng/calendar';
import { RouterModule } from '@angular/router';
import { HeaderComponent } from '../../header/header.component';

@Component({
  selector: 'app-historico',
  standalone: true,

  imports: [CommonModule, TransacaoComponent, FormsModule, ChartModule, CalendarModule, RouterModule, HeaderComponent],
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

  ordenarTransacoes(){
    this.transacoes.sort((a,b) => {
      const dataA = new Date(a.data).getTime();
      const dataB = new Date(b.data).getTime();
      return dataB - dataA;
    })
  }

  
  // BARRA DE PESQUISA POR NOME, VALOR, TIPO E DESCRIÇÃO.

  filteredItems: Transacao[] = [];

  searchTerm: string = '';
  searchMade: boolean = false;

  constructor() {
    this.filteredItems = this.transacoes;
  }

  filterItems() {
    
    if (!this.searchTerm.trim()) {
      this.searchMade = false;
      // Se não houver termo de pesquisa, mostre todos os itens
      this.filteredItems = this.transacoes;
    } else {
      
      this.searchMade = true;
      // Filtra os itens que incluem o termo de pesquisa
      this.filteredItems = this.transacoes.filter(transacoes =>
      (transacoes.nome?.toLowerCase().includes(this.searchTerm.toLowerCase()) ?? false) ||
      (transacoes.valor?.toString().toLowerCase().includes(this.searchTerm.toLowerCase()) ?? false) ||
      (transacoes.tipo?.toString().toLowerCase().includes(this.searchTerm.toLowerCase()) ?? false) ||
      (transacoes.descricao?.toString().toLowerCase().includes(this.searchTerm.toLowerCase()) ?? false)
      );
    }
  }

 value: Date | undefined;


  //  IMAGEM CHARTS TELA MENOR

  chartData: any = {
    labels: ['Alimentação', 'Lazer', 'Educação', 'Viagens', 'Jogos', 'Mercado', 'Jogos', 'Mercado', 'Mercado'],
    datasets: [
      {
        label: 'Gastos',
        data: [100, 50, 15, 30, 40, 100, 70, 20, 30],
        backgroundColor: 'rgba(127, 83, 223, 1)',
        hoverBackgroundColor: 'rgba(102, 48, 217, 1)',
        borderRadius: [{ topLeft: 0, topRight: 12, bottomLeft: 0, bottomRight: 0 }, 0, 0, 0, 0, 0, 0, 0, { topLeft: 0, topRight: 0, bottomLeft: 0, bottomRight: 12 }]
      }
    ]
  }

  chartOptions: any = {
    indexAxis: 'y',
    responsive: true,

    layout: {
      padding: 0,
    },

    plugins: {
      legend: {
        display: false,
      }
    },

    scales: {
      x: {
        border: {
          color: 'transparent',
        },
        ticks: {
          display: false,
        },
        grid: {
          display: false,
        }
      },
      y: {
        border: {
          color: 'transparent',
        },
        ticks: {
          display: false,
        },
        grid: {
          display: false,
        },
      },
    }
  }

};