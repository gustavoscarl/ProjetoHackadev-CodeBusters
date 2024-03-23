import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CriarContaService } from '../../servicos/criarconta.service';
import { AuthService } from '../../auth.service';
import { MudarConta } from '../../modelos/MudarConta';
import { MudarContaService } from '../../servicos/mudar-conta.service';
import { ContaInfoService } from '../../servicos/getcontainfo.service';
import { NgxCurrencyDirective, NgxCurrencyInputMode } from 'ngx-currency';
import { NgxMaskDirective } from 'ngx-mask';
import { catchError, throwError } from 'rxjs';
import { InfoConta } from '../../modelos/InfoConta';

@Component({
  selector: 'app-conta-criada',
  standalone: true,
  imports: [RouterModule, ReactiveFormsModule, NgxCurrencyDirective, NgxMaskDirective],
  templateUrl: './conta-criada.component.html',
  styleUrl: './conta-criada.component.css'
})
export class ContaCriadaComponent {
  changeAccountForm!: FormGroup
  numeroConta?: any;
  numeroAgencia?: any;

  ngOnInit() {

 
      this.contaInfoService.getInformacoes().subscribe({
        next: ((data: any) => {
          console.log(data)
          this.numeroAgencia = data.agencia;
          this.numeroConta = data.numero;
        }),
        error: (error) => {
          console.error('Error fetching client data:', error);
        }
      })

    this.changeAccountForm = new FormGroup({
      'pix-geral': new FormControl(null, 
        [
        Validators.required,
        Validators.minLength(4),
        ]),
      'pix-noturno': new FormControl(null, 
        [
        Validators.required 
        ]),
      'pin': new FormControl(null, 
        [
        Validators.required 
        ]),
    })



  }

  constructor(private contaService:MudarContaService, private authService: AuthService, private route: Router, private contaInfoService: ContaInfoService) {}



  onSubmit(): void {
    document.querySelector('.mensagem-erro')?.classList.add('d-none')
    console.log(this.changeAccountForm)
    this.changeAccountForm?.markAllAsTouched();
    if (this.changeAccountForm?.valid) {
      let mudarContaData: MudarConta = {
        limitePixGeral: this.changeAccountForm.get('pix-geral')?.value,
        limitePixNoturno: this.changeAccountForm.get('pix-noturno')?.value,
        pin: this.changeAccountForm.get('pin')?.value
      }
      
      this.contaService.alterarConta(mudarContaData)
      .pipe(
        catchError((error: any) => {
          if (error.status === 400) {
            // Erro 400: Pin incorreto
            this.mostrarMensagemDeErro('Pin incorreto, verifique suas credenciais.');
          } else {
            console.error('Erro ao alterar conta:', error);
          }
          return throwError(error); // Reenvia o erro para ser tratado posteriormente
        })
      )
        .subscribe({
          next: (retorno: any) => {
            console.log(retorno)
            this.changeAccountForm.reset();
            setTimeout(() => {
              this.route.navigate(['/home']);
            }, 1200);
          },
          error: (error) => {
            console.log(error);
          }
        });
    }
  }

  excluir(): void{
    document.querySelector('.mensagem-erro')?.classList.add('d-none')
    this.contaService.excluirConta().pipe(
      catchError((error: any) => {
        if (error.status === 400) {
          this.mostrarMensagemDeErro('Existe saldo na sua conta. Saque ou transfira para excluir a conta.');
        } else {
          console.error('Erro ao alterar conta:', error);
        }
        return throwError(error); // Reenvia o erro para ser tratado posteriormente
      })
    )
      .subscribe({
        next: (retorno: any) => {
          console.log(retorno)
          this.changeAccountForm.reset();
          setTimeout(() => {
            this.route.navigate(['/home']);
          }, 1200);
        },
        error: (error: any) => {
          console.log(error);
        }
      });
  }
  


  mostrarMensagemDeErro(mensagem: string): void {
    const mensagemErroElemento = document.querySelector('.mensagem-erro');
    if (mensagemErroElemento) {
      mensagemErroElemento.textContent = mensagem;
      mensagemErroElemento.classList.remove('d-none');
    }
  }
}