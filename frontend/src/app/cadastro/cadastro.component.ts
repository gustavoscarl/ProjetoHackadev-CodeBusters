import { CommonModule } from '@angular/common';
import { Component} from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { EnderecoService } from '../servicos/endereco.service';
import { HttpClientModule } from '@angular/common/http';
import { Endereco } from '../modelos/Endereco';
import { NgxMaskDirective, NgxMaskPipe } from 'ngx-mask';
import { criarSenhaForte } from '../validators/senhaforte';
import { CadastroService } from '../servicos/cadastro.service';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [RouterModule, FormsModule, CommonModule, HttpClientModule, ReactiveFormsModule, NgxMaskDirective, NgxMaskPipe],
  templateUrl: './cadastro.component.html',
  styleUrl: './cadastro.component.css'
})

export class CadastroComponent {


  // Variavel CEP
  cep: FormControl = new FormControl('');
  enderecoPorCep: Endereco | undefined;

  constructor(private servico:EnderecoService, private cadastroService: CadastroService){}

  // Função para obter o endereço e chamar a função que preenche após obter os endereços via API.
  obterEndereco():void{
    this.servico.retornarEndereco(this.cep.value)
    .subscribe((retorno: Endereco | undefined) => { 
      this.enderecoPorCep = retorno;
      this.preencherCamposEndereco();
      console.log(retorno);
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

  // Form
  cadastroForm!: FormGroup;

  ngOnInit() {
    this.cadastroForm = new FormGroup({
      'name': new FormControl(null, 
        [
        Validators.required,
        Validators.pattern('^[a-zA-ZÀ-ú ]+$'),
        Validators.minLength(2),
      ]),
      'lastname': new FormControl(null, 
        [
        Validators.required, 
        Validators.pattern('^[a-zA-ZÀ-ú ]+$'),
        Validators.minLength(3)
      ]),
      'cep': new FormControl(null, 
        [
        Validators.required,
        Validators.pattern('^[0-9]+$')
        ]),
      'cidade': new FormControl(null, 
          [
            Validators.required,
            Validators.pattern('^[a-zA-ZÀ-ú ]+$'),
            Validators.minLength(3)
          ]),
      'logradouro': new FormControl(null, 
            [
              Validators.required,
              Validators.pattern('^[a-zA-ZÀ-ú ]+$'),
              Validators.minLength(3)
            ]),
      'numero': new FormControl(null, 
            [
              Validators.required,
              Validators.pattern('^[0-9]+$')
            ]),
      'bairro': new FormControl(null,
          [
            Validators.required,
            Validators.pattern('^[a-zA-ZÀ-ú ]+$')
          ]),
      'identidade': new FormControl(null,
        [
          Validators.required,
          Validators.pattern('^[0-9]+$')
        ]),
        'cpf': new FormControl(null,
          [
            Validators.required,
            Validators.pattern('^[0-9]+$')
          ]),
      'complemento': new FormControl(null,
            [
              Validators.required,
              Validators.pattern('^[a-zA-ZÀ-ú-0-9 ]+$')
            ]),
      'email': new FormControl(null,
        [
          Validators.required,
          Validators.email
        ]),
      'password': new FormControl(null,
          [
            Validators.required,
            Validators.minLength(6),
            criarSenhaForte(),
          ]),
      'confirm-password': new FormControl(null,
            [
              Validators.required,
            ]),
    });
  }

  onSubmit() {
    if (this.cadastroForm.valid) {
      this.cadastroService.cadastrarCliente(this.cadastroForm.value)
      .subscribe(
        retorno => {
          console.log('Cliente cadastrado com sucesso', retorno)
        }
      )
    }
    else {
      console.log('Formulário inválido ou erro no servidor')
    }
  }

}
