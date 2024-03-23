import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgxCurrencyDirective } from 'ngx-currency';
import { NgxMaskDirective } from 'ngx-mask';
import { AuthService } from '../../auth.service';
import { TransferenciaService } from '../../servicos/transferencia.service';

@Component({
  selector: 'app-transferencia',
  standalone: true,
  imports: [FormsModule, NgxCurrencyDirective , NgxMaskDirective, ReactiveFormsModule],
  templateUrl: './transferencia.component.html',
  styleUrl: './transferencia.component.css'
})

export class TransferenciaComponent {
  transferForm!: FormGroup
  valor: number | undefined;
  contaDestino: number | undefined;
  descricao: string | undefined;

  constructor(private authService: AuthService, private transferService: TransferenciaService) { }

  
    
  ngOnInit(){
    this.transferForm = new FormGroup({
      'valor': new FormControl(null, 
        [
        Validators.required,
      ]),
      'contaDestino': new FormControl(null, 
        [
        Validators.required,
      ]),
      'pin': new FormControl(null, 
        [
        Validators.required,
      ]),
      'descricao': new FormControl(null, 
        [
        Validators.required,
      ]),
    })
  }

  realizarTransferencia(): void {
    if (this.transferForm.valid) {
      this.transferService.transferir(this.transferForm.value)
      .subscribe(retorno => {
        setTimeout(() => {
          // this.route.navigateByUrl('login')
        }, 1000)
        console.log(retorno);
      });
    }
  }
}
