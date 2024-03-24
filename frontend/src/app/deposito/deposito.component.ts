import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { DepositoService } from '../servicos/Deposito.Service'; // Importe o serviço de depósito
import { CommonModule } from '@angular/common';
import { NgxCurrencyDirective } from 'ngx-currency';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-deposito',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NgxCurrencyDirective, RouterModule],
  templateUrl: './deposito.component.html',
  styleUrls: ['./deposito.component.css']
})
export class DepositoComponent {
  formulario = new FormGroup({
    valor: new FormControl(''),
    descricao: new FormControl('')
  });

  depositos: any[] = []; // Seu array de depósitos aqui

  constructor(private depositoService: DepositoService, private route: Router) { }

  enviar(): void {
    if (this.formulario.valid) {
      console.log(this.formulario.value)
      this.depositoService
        .depositar(this.formulario.value)
        .subscribe(() => {
          this.formulario.reset();
          alert('Deposito realizado com sucesso!');
          setTimeout(() => {
            this.route.navigate(['home'])
          }, 250);
        }, error => {
          alert(`Erro ao depositar:${error.error.message}`);
        });
    } else {
      console.log('Formulário inválido!');
    }
  }
}