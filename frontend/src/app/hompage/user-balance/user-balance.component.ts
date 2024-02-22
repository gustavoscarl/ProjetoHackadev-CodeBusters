import { Component, Inject, Input } from '@angular/core';
import { InputserviceService } from '../../servicos/inputservice.service';
import { ChartModule } from 'primeng/chart';
import { RouterLink } from '@angular/router';

@Inject({ providedIn: 'root' })

@Component({
  selector: 'app-user-balance',
  standalone: true,
  imports: [ChartModule, RouterLink],
  templateUrl: './user-balance.component.html',
  styleUrl: './user-balance.component.css'
})
export class UserBalanceComponent {
  @Input() saldoAtual: number = 0;
  @Input() userName!: string;

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

  constructor(private inputService: InputserviceService) {
    this.saldoAtual = this.inputService.saldoDoUsuario;
    this.userName = this.inputService.nomeDoUsuario;
  }
}
