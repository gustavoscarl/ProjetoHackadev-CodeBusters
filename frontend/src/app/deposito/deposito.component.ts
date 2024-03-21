import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { DepositoService } from '../servicos/Deposito.Service'; // Importe o serviço de depósito
import { CommonModule } from '@angular/common';
import { NgxCurrencyDirective } from 'ngx-currency';

@Component({
  selector: 'app-deposito',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NgxCurrencyDirective],
  templateUrl: './deposito.component.html',
  styleUrls: ['./deposito.component.css']
})
export class DepositoComponent {
  formulario = new FormGroup({
    contaId: new FormControl(''),
    valor: new FormControl(''),
    descricao: new FormControl('')
  });

  depositos: any[] = []; // Seu array de depósitos aqui

  constructor(private depositoService: DepositoService) { }

  enviar(): void {
    if (this.formulario.valid) {
      this.depositoService
        .depositar(this.formulario.value)
        .subscribe(() => {
          this.formulario.reset();
          alert('Deposito realizado com sucesso!');
        }, error => {
          alert('Erro ao depositar:');
        });
    } else {
      console.log('Formulário inválido!');
    }
  }
}