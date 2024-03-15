import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { EnderecoService } from '../servicos/endereco.service';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [RouterModule, FormsModule, CommonModule, HttpClientModule],
  templateUrl: './cadastro.component.html',
  styleUrl: './cadastro.component.css'
})
export class CadastroComponent {


  // Variavel CEP
  cep:string = '';

  constructor(private servico:EnderecoService){}

  // Função para obter o endereço
  obterEndereco():void{
    this.servico.retornarEndereco(this.cep)
    .subscribe( retorno => { console.log(retorno)});
  }
}
