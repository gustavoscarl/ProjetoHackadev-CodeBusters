import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgxCurrencyDirective } from 'ngx-currency';
import { DepositoService } from '../servicos/Deposito.Service'; // Importe o serviço de depósito
import { Deposito } from '../modelos/Deposito';

@Component({
  selector: 'app-deposito',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NgxCurrencyDirective],
  templateUrl: './deposito.component.html',
  styleUrls: ['./deposito.component.css']
})

export class DepositoComponent {
  valor: string| undefined;
  descricao: string | undefined;

  constructor(private depositoService: DepositoService) { }
  
  // Form
  depositoForm!: FormGroup;
  showAlert: boolean = false;

  ngOnInit() {
    this.depositoForm = new FormGroup({
      'valor': new FormControl(null,
        [
          Validators.required,
          Validators.pattern('^[(([\\d]{1,3})(\\,([\\d]{1,2}))?)$'),
          Validators.minLength(1),
        ]),
        'descricao': new FormControl(null,
          [
            Validators.minLength(1),
          ]),
    });
  }

  // onSubmit(): void {
  //   console.log(this.depositoForm);
  //   this.depositoForm.markAllAsTouched();

  //   if(this.depositoForm.valid) {
  //     const depositoData: Deposito = {
  //       valor: this.depositoForm.get('valor')?.value,
  //       descricao: this.depositoForm.get('descricao')?.value,
  //     };

  //     this.depositoService.depositar(depositoData)
  //       .subscribe((deposito: Deposito) => {
  //         setTimeout(() => {
  //           this.route.navigateByUrl('deposito')
  //         }, 1000)
  //         this.showAlert = true;
  //         console.log(deposito);
  //       });
  //     }
  // }

  // formulario = new FormGroup({
  //   contaId: new FormControl(''),
  //   valor: new FormControl(''),
  //   descricao: new FormControl('')
  // });

  depositos: any[] = []; // Seu array de depósitos aqui


  onSubmit(): void {
    this.depositoForm.markAllAsTouched();

    if (this.depositoForm.valid) {
      const depositoData: Deposito = {
        valor: this.depositoForm.get('valor')?.value,
        descricao: this.depositoForm.get('descricao')?.value,
      };

      this.depositoService.depositar(depositoData)
        .subscribe( {
          next: () => {
          this.depositoForm.reset();
          alert('Deposito realizado com sucesso!');
        }, 
        error: () => {
          alert('Erro ao depositar.');
        }
        });
    } else {
      alert('Formulário inválido!');
    }
  }

  // Função para criar o validador de valor mínimo
  minValueValidator(minValue: number) {
    return (control: { value: string; }) => {
      const value = parseFloat(control.value);
      return value >= minValue ? null : { minValue: { min: minValue } };
    };
  }
}