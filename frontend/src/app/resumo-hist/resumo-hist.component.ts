import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CalendarModule } from 'primeng/calendar';
import { ChartModule } from 'primeng/chart';
import { HeaderComponent } from '../header/header.component';
import { Transacao } from '../modelos/Transacoes';

@Component({
  selector: 'app-resumo-hist',
  standalone: true,
  imports: [CommonModule, FormsModule, ChartModule, CalendarModule, RouterModule, HeaderComponent],
  templateUrl: './resumo-hist.component.html',
  styleUrl: './resumo-hist.component.css'
})


export class ResumoHistComponent {

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

    // BARRA DE PESQUISA POR NOME, VALOR, TIPO E DESCRIÇÃO.

    filteredItems: Transacao[] = [];

    searchTerm: string = '';
    searchMade: boolean = false;

    
    // Vetor de transacões - Onde as transações ficarão armazenadas no momento (sem backend)
  transacoes: Transacao[] = [
    { nome:'Code Busters', descricao:'Compra de aplicat...', tipo:'Pix', valor:'R$ 120,00', contaDestino:123123, data:'2024-01-28'},
    { nome:'Amazon', descricao:'Campanha publici...', tipo:'Debito', valor:'R$ 15,00', contaDestino:111111, data:'2024-01-29'},
    { nome:'Code Busters', descricao:'Compra de aplicat...', tipo:'Credito', valor:'R$ 1000,00', contaDestino:222222, data:'2024-01-30'},
    { nome:'Amazon', descricao:'Campanha publici...', tipo:'Pix', valor:'R$ 120,00', contaDestino:123123, data:'2024-01-28'},
    ];

  
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
  
  
  

}
