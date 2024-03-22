import { Component, Inject, Input, Renderer2 } from '@angular/core';
import { InputserviceService } from '../../servicos/inputservice.service';
import { ChartModule } from 'primeng/chart';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { SaldoService } from '../../servicos/saldo.service';
import { NgxCurrencyDirective, NgxCurrencyInputMode } from 'ngx-currency';
import { AuthService } from '../../auth.service';

@Inject({ providedIn: 'root' })

@Component({
  selector: 'app-user-balance',
  standalone: true,
  imports: [ChartModule, RouterLink, CommonModule, NgxCurrencyDirective],
  templateUrl: './user-balance.component.html',
  styleUrl: './user-balance.component.css'
})
export class UserBalanceComponent {
  saldoAtual?: number;
  userName?: string;
  temConta?: boolean;
  dadosProntosSubscription: Subscription | undefined;
  valorVisivel: boolean = true;

  alternarExibicaoSaldo(): void {
    this.valorVisivel = !this.valorVisivel;
  }

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

  constructor(private inputService: InputserviceService, private renderer : Renderer2, private saldoService: SaldoService, private authService: AuthService) {

    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', event => {
      this.isdarkMode = event.matches;
    });
  }
    saldoVisivel: boolean = true;
    isdarkMode: boolean = window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches;

  toggleSaldoVisibilidade() {
    this.saldoVisivel = !this.saldoVisivel;
  }

  logout(){
    this.authService.logout();
  }

  ngOnInit() {
    this.dadosProntosSubscription = this.inputService.dadosProntos$.subscribe(() => {
      // Obtenha os dados quando estiverem prontos
      this.userName = this.inputService.nomeDoUsuario;
      this.temConta = this.inputService.temConta;
    });
    this.saldoService.getSaldo()
    .subscribe({
      next: ((data: any) => {
        console.log(data)
        this.saldoAtual = data.saldo
        this.inputService.saldoDoUsuario = data.saldo
        this.inputService.enviarDadosProntos()
      }),
      error: (error) => {
        console.error('Error fetching client data:', error);
      }
    });
  }

  ngOnDestroy() {
    if (this.dadosProntosSubscription) {
      this.dadosProntosSubscription.unsubscribe();
    }
  }
}