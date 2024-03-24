import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { NgxMaskDirective, NgxMaskPipe } from 'ngx-mask';
import { Login } from '../modelos/Login';
import { LoginService } from '../servicos/login.service';
import { AuthService } from '../auth.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterModule, NgxMaskDirective, NgxMaskPipe, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {
  senhaVisivel: boolean = false;
  
  constructor (private http: HttpClient ,private authService: AuthService, private loginService: LoginService, private route: Router) {
    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', event => {
      this.isdarkMode = event.matches;
    });
  }
  isdarkMode: boolean = window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches;
  
  senhaVisibilidade() {
    this.senhaVisivel = !this.senhaVisivel;
  }
  
  alternarExibicaoSenha(): void {
    this.senhaVisivel = !this.senhaVisivel;
  }
  
  // Form
  loginForm!: FormGroup;
  showAlert:boolean = false;
  
  ngOnInit() {
    this.loginForm = new FormGroup({
      'cpf': new FormControl(null, 
        [
          Validators.required,
          Validators.pattern('^[0-9]+$'),
          Validators.minLength(11),
      ]),
      'senha': new FormControl(null, 
        [
          Validators.required,
        ]),
      })
    }
    
    
    
    
    onSubmit(): void {
      const mensagemErroElemento = document.querySelector('.mensagem-erro');
      
      this.loginForm.markAllAsTouched();
      if (this.loginForm.valid) { 
        this.loginService.logarCliente(this.loginForm.value as Login).subscribe({
          next: (retorno: any) => {
          this.authService.guardarToken(retorno.accessToken)
          mensagemErroElemento?.classList.add('d-none');
          this.loginForm.reset();
          this.showAlert = true;
          setTimeout(() => {
            this.route.navigate(['/home']);
          }, 1200);
        },
        error: (error) => {
          if (error.status === 404) {
            this.mostrarMensagemDeErro('Credenciais inválidas. Por favor, verifique seus dados ou realize o Cadastro.')
          }
        }
      });
    }
  }
  
  mostrarMensagemDeErro(mensagem: string): void {
    const mensagemErroElemento = document.querySelector('.mensagem-erro');
    if (mensagemErroElemento) {
      mensagemErroElemento.textContent = mensagem;
      mensagemErroElemento.classList.remove('d-none');
    }
  }
}