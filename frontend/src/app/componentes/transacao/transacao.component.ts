import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Transacao } from '../../modelo/Transacoes';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-transacao',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './transacao.component.html',
  styleUrl: './transacao.component.css'
})
export class TransacaoComponent {
  
  // Formulario de transação
  formulario = new FormGroup({
    tipo: new FormControl(''),
    valor: new FormControl(''),
    contaDestino: new FormControl(''),
    data: new FormControl(''),
  });
  
}
