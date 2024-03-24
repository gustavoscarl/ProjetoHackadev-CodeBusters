import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { NgxMaskDirective } from 'ngx-mask';
import { InvestimentosService } from '../servicos/investimento.service';
import { Observable, Subscription, catchError, of, shareReplay, take, throwError } from 'rxjs';
import { InputserviceService } from '../servicos/inputservice.service';
import { CommonModule } from '@angular/common';
import { NgxCurrencyDirective } from 'ngx-currency';
import { HttpClient } from '@angular/common/http';
import { Investimento } from '../modelos/Investimento';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-investimentos',
  standalone: true,
  imports: [RouterModule, NgxMaskDirective, ReactiveFormsModule, CommonModule, NgxMaskDirective, NgxCurrencyDirective],
  templateUrl: './investimentos.component.html',
  styleUrl: './investimentos.component.css'
})
export class InvestimentosComponent implements OnInit {
temInvestimento?:boolean;
investimento?:any;
formInvestimento!: FormGroup;

constructor(private investimentoService: InvestimentosService, private route: Router) {}

ngOnInit() {
 

  this.formInvestimento = new FormGroup({
    'valor': new FormControl(null, 
      [
      Validators.required,
    ]),
    'tempo': new FormControl(null, 
      [
      Validators.required,
    ]),
    'pin': new FormControl(null,
      [
      Validators.required,
    ])
  })

  this.investimentoService.getInformacoesInvestimento()
      .pipe(
        take(1),
        catchError((error) => {
          if (error.status === 404) {
            this.temInvestimento = false;
            console.error('Investment data not found:', error);
          } else {
            console.error('Error fetching investment data:', error);
          }
          return throwError(error); // Propaga o erro para o fluxo
        })
      )
      .subscribe((retorno) => {
        if (retorno) {
          this.investimento = retorno;
          console.log(this.investimento)
          console.log(this.investimento)
          this.temInvestimento = true;
        } else {
          this.temInvestimento = false;
          console.error('Investment data is empty.');
        }
      });
  }





realizarInvestimento() {
  if (this.formInvestimento.valid) {
    const tempoSelecionado = this.formInvestimento.get('tempo')?.value;
    const dataParaEnviar = this.converterTempoParaData(tempoSelecionado);
    
    // Criar um objeto com os dados a serem enviados para o serviço de investimento
    const dadosInvestimento = {
      tempo: dataParaEnviar,
      valor: this.formInvestimento.get('valor')?.value
    };

    this.investimentoService.fazerInvestimento(dadosInvestimento).subscribe(
      () => {
        this.formInvestimento.reset();
        alert('Investimento realizado com sucesso!');
        this.route.navigate(['home'])
      },
      (error) => {
        alert('Erro ao realizar investimento: ' + error.message);
      }
    );
  } else {
    console.log('Formulário inválido!');
  }
}
  

  converterTempoParaData(tempo: string): Date {
    const dataAtual = new Date();
    const dataConvertida = new Date(dataAtual.setMonth(dataAtual.getMonth() + parseInt(tempo, 10)));
    return dataConvertida;
  }
}




