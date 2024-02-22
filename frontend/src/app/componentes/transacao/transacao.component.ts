import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
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

  // Obtendo a função de realizar a transacao vindo do componente histórico

  @Output() cadastrarTransacao = new EventEmitter<Transacao>();
  
  // funcao para realizar a transacao (EMITE EVENTO (cadastrarTransacao) PARA FUNÇÃO realizarTrasacao DO COMPONENTE HISTÓRICO)

  transacao():void{
    this.cadastrarTransacao.emit(this.formulario.value as Transacao);
    // alert('Transação Realizada com sucesso');
  }


  // Formulario de transação
  
  formulario = new FormGroup({
    tipo: new FormControl(''),
    valor: new FormControl(''),
    contaDestino: new FormControl(''),
    data: new FormControl(''),
  });

  get data() {
    return this.formulario.get('data');
  } 
  
}
