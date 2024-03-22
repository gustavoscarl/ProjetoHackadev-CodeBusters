import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CriarContaService } from '../../servicos/criarconta.service';
import { AuthService } from '../../auth.service';
import { MudarConta } from '../../modelos/MudarConta';
import { MudarContaService } from '../../servicos/mudar-conta.service';
import { ContaInfoService } from '../../servicos/getcontainfo.service';
import { NgxCurrencyDirective, NgxCurrencyInputMode } from 'ngx-currency';

@Component({
  selector: 'app-conta-criada',
  standalone: true,
  imports: [RouterModule, ReactiveFormsModule, NgxCurrencyDirective],
  templateUrl: './conta-criada.component.html',
  styleUrl: './conta-criada.component.css'
})
export class ContaCriadaComponent {
  changeAccountForm!: FormGroup

  ngOnInit() {
    this.changeAccountForm = new FormGroup({
      'pix-geral': new FormControl(null, 
        [
        Validators.required,
        Validators.minLength(4),
      ]),
      'pix-noturno': new FormControl(null, 
        [
        Validators.required 
      ])})


    }

  constructor(private contaService:MudarContaService, private authService: AuthService, private route: Router, private contaInfoService: ContaInfoService) {}

  onSubmit(): void {
    console.log(this.changeAccountForm)
    this.changeAccountForm?.markAllAsTouched();
    if (this.changeAccountForm?.valid) {
      let mudarContaData: MudarConta = {
        limitePixGeral: this.changeAccountForm.get('pix-geral')?.value,
        limitePixNoturno: this.changeAccountForm.get('pix-noturno')?.value
      }
      this.contaService.cadastrarConta(mudarContaData)
        .subscribe({
          next: (retorno: any) => {
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
}
