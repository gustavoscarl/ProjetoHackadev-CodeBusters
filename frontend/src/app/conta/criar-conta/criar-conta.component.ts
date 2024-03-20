import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NgxMaskDirective, NgxMaskPipe } from 'ngx-mask';
import { CriarContaService } from '../../servicos/criarconta.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { CriarConta } from '../../modelos/CriarConta';

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
        Validators.required 
      ])})


  }
  constructor(private contaService:CriarContaService) {}
  onSubmit(): void {
    console.log(this.pinForm)
    this.pinForm?.markAllAsTouched();
    if (this.pinForm?.valid) {
      const pinData: CriarConta = {
        pin: this.pinForm.get('pin')?.value
      }
      this.contaService.cadastrarConta(pinData)
        .subscribe(retorno => {
          console.log(retorno);
        });
    }
  }
}