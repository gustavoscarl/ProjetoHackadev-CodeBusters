import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { NgxMaskDirective, NgxMaskPipe } from 'ngx-mask';
import { CriarContaService } from '../../servicos/criarconta.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { CriarConta } from '../../modelos/CriarConta';
import { AuthService } from '../../auth.service';
import { confirmarSenha } from '../../validators/confirmasenha';

@Component({
  selector: 'app-criar-conta',
  standalone: true,
  imports: [RouterModule, NgxMaskDirective, NgxMaskPipe, ReactiveFormsModule, CommonModule, HttpClientModule],
  templateUrl: './criar-conta.component.html',
  styleUrl: './criar-conta.component.css'
})

export class CriarContaComponent {
pinForm!: FormGroup
  ngOnInit() {
    this.pinForm = new FormGroup({
      'pin': new FormControl(null, 
        [
        Validators.required,
        Validators.minLength(4),
      ]),
      'pin-confirm': new FormControl(null, 
        [
        Validators.required,
        confirmarSenha('pin')
      ])})


  }
  constructor(private contaService:CriarContaService, private authService: AuthService, private route: Router) {}

  onSubmit(): void {
    console.log(this.pinForm)
    this.pinForm?.markAllAsTouched();
    if (this.pinForm?.valid) {
      const pinData: CriarConta = {
        pin: this.pinForm.get('pin')?.value
      }
      this.contaService.cadastrarConta(pinData)
        .subscribe({
          next: (retorno: any) => {
            console.log(retorno)
            this.authService.guardarToken(retorno.accessToken)
            this.pinForm.reset();
            alert('Conta cadastrada!')
            setTimeout(() => {
              this.route.navigate(['/home']);
            }, 1200);
          },
          error: (error) => {
            alert(`Erro: ${error.error.message}`);
          }
        });
    }
  }
}