import { CommonModule } from '@angular/common';
import { Component} from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { EnderecoService } from '../servicos/endereco.service';
import { HttpClientModule } from '@angular/common/http';
import { Endereco } from '../modelos/Endereco';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [RouterModule, FormsModule, CommonModule, HttpClientModule, ReactiveFormsModule],
  templateUrl: './cadastro.component.html',
  styleUrl: './cadastro.component.css'
})
export class CadastroComponent {


  // Variavel CEP
  cep: FormControl = new FormControl('');
  enderecoPorCep: Endereco | undefined;

  constructor(private servico:EnderecoService){}

  // Função para obter o endereço e chamar a função que preenche após obter os endereços via API.
  obterEndereco():void{
    this.servico.retornarEndereco(this.cep.value)
    .subscribe((retorno: Endereco | undefined) => { 
      this.enderecoPorCep = retorno;
      this.preencherCamposEndereco();
    });
  }

  // Função que preenche os campos endereço
  preencherCamposEndereco():void{
    if(this.enderecoPorCep){
      document.getElementById('inputCity')?.setAttribute('value', this.enderecoPorCep.localidade || '');
      document.getElementById('inputLogradouro')?.setAttribute('value', this.enderecoPorCep.logradouro || '');
      document.getElementById('inputBairro')?.setAttribute('value', this.enderecoPorCep.bairro || '');
      document.getElementById('inputComplemento')?.setAttribute('value', this.enderecoPorCep.complemento || '');
      (document.getElementById('inputState') as HTMLSelectElement).value = this.enderecoPorCep.uf || '';
    };
    // Necessidade de um else caso nao tenha preenchido? A pensar.

  }
  cadastroForm!: FormGroup;

  ngOnInit() {
    this.cadastroForm = new FormGroup({
      'name': new FormControl('', [Validators.required]),
      'lastname': new FormControl(''),
      'cep': new FormControl(null),
    });
  }

  onSubmit() {
    console.log(this.cadastroForm);
  }

}
