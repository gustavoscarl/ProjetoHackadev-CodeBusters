import { Component, Inject, Input, Renderer2 } from '@angular/core';
import { InputserviceService } from '../../servicos/inputservice.service';
import { ChartModule } from 'primeng/chart';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';

@Inject({ providedIn: 'root' })

@Component({
  selector: 'app-user-balance',
  standalone: true,
  imports: [ChartModule, RouterLink, CommonModule],
  templateUrl: './user-balance.component.html',
  styleUrl: './user-balance.component.css'
})
export class UserBalanceComponent {
  saldoAtual?: number = 0;
  userName?: string;
  temConta?: boolean;
  dadosProntosSubscription: Subscription | undefined;

  chartData: any = {
    labels: ['Alimentação', 'Lazer', 'Educação', 'Viagens'],
    datasets: [
      {
        label: 'Gastos',
        data: [1000, 500, 150, 300],
        backgroundColor: 'rgba(127, 83, 223, 1)',
        hoverBackgroundColor: 'rgba(102, 48, 217, 1)',
        borderRadius: 8
      }
    ]
  }

  chartOptions: any = {
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
        }
      }
    }
  }

  constructor(private inputService: InputserviceService, private renderer : Renderer2) {

    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', event => {
      this.isdarkMode = event.matches;
    });
  }
    saldoVisivel: boolean = true;
    isdarkMode: boolean = window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches;

  toggleSaldoVisibilidade() {
    this.saldoVisivel = !this.saldoVisivel;
  }

  ngOnInit() {
    this.dadosProntosSubscription = this.inputService.dadosProntos$.subscribe(() => {
      // Obtenha os dados quando estiverem prontos
      this.saldoAtual = this.inputService.saldoDoUsuario;
      this.userName = this.inputService.nomeDoUsuario;
    });
  }

  ngOnDestroy() {
    if (this.dadosProntosSubscription) {
      this.dadosProntosSubscription.unsubscribe();
    }
  }
}