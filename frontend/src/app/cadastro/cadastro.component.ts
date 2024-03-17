import { CommonModule } from '@angular/common';
import { Component} from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { EnderecoService } from '../servicos/endereco.service';
import { HttpClientModule } from '@angular/common/http';
import { Endereco } from '../modelos/Endereco';
import { NgxMaskDirective, NgxMaskPipe } from 'ngx-mask';
import { criarSenhaForte } from '../validators/senhaforte';
import { CadastroService } from '../servicos/cadastro.service';
import { Cadastro } from '../modelos/Cadastro';

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

  constructor(private servico:EnderecoService, private cadastroService: CadastroService, private route:Router){}

  // Função para obter o endereço e chamar a função que preenche após obter os endereços via API.
  obterEndereco():void{
    this.servico.retornarEndereco(this.cadastroForm.get('cep')?.value)
    .subscribe((retorno: Endereco | undefined) => { 
      this.enderecoPorCep = retorno;
      this.preencherCamposEndereco();
      console.log(retorno);
    });

  }

  // Função que preenche os campos endereço
  preencherCamposEndereco():void{
    if(this.enderecoPorCep){
      (document.getElementById('inputCity') as HTMLSelectElement).value = this.enderecoPorCep.localidade || '';
      (document.getElementById('inputLogradouro') as HTMLSelectElement).value = this.enderecoPorCep.logradouro || '';
      (document.getElementById('inputBairro') as HTMLSelectElement).value = this.enderecoPorCep.bairro || '';
      (document.getElementById('inputState') as HTMLSelectElement).value = this.enderecoPorCep.uf || '';
    };
    // Necessidade de um else caso nao tenha preenchido? A pensar.

  }

  // Form
  cadastroForm!: FormGroup;
  showAlert:boolean = false;

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
            Validators.minLength(8),
            criarSenhaForte(),
          ]),
      'confirm-password': new FormControl(null,
            [
              Validators.required,
            ]),
    });
  }

  onSubmit(): void {
    this.cadastroForm.markAllAsTouched();
    if (this.cadastroForm.valid) {
      const cadastroData: Cadastro = {
        nome: this.cadastroForm.get('name')?.value + this.cadastroForm.get('lastname')?.value,
        email: this.cadastroForm.get('email')?.value,
        senha: this.cadastroForm.get('password')?.value,
        cpf: this.cadastroForm.get('cpf')?.value,
        rg: this.cadastroForm.get('identidade')?.value,
        endereco: {
          id: this.cadastroForm.get('endereco.id')?.value,
          rua: this.cadastroForm.get('endereco.rua')?.value,
          numero: this.cadastroForm.get('endereco.numero')?.value,
          bairro: this.cadastroForm.get('endereco.bairro')?.value,
          complemento: this.cadastroForm.get('endereco.complemento')?.value,
          cep: this.cadastroForm.get('endereco.cep')?.value,
          cidade: this.cadastroForm.get('endereco.cidade')?.value,
          estado: this.cadastroForm.get('endereco.estado')?.value,
        }
      };

      this.cadastroService.cadastrarCliente(cadastroData)
        .subscribe(cadastro => {
          setTimeout(() => {
            this.route.navigateByUrl('login')
          }, 2500)
          this.showAlert = true;
          console.log(cadastro);
          // Lógica de sucesso após o POST
        });
    }
  }
}
