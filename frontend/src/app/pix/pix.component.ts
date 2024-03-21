import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { PixService } from '../servicos/pix.service';
import { Pix } from '../modelos/Pix'

@Component({
  selector: 'app-pix',
  standalone: true,
  imports: [FormsModule, CommonModule, ReactiveFormsModule],
  templateUrl: './pix.component.html',
  styleUrl: './pix.component.css'
})

export class PixComponent {
  valor: number | undefined;
  chavePix: string | undefined;
  descricao: string | undefined;
  pixService: any;

  constructor(private servico: PixService, private route:Router) { }

  efetuarPix(): void {
    // Lógica para realizar a transação PIX
    console.log('Transação PIX realizada:', { valor: this.valor, chavePix: this.chavePix, descricao: this.descricao });
    // Aqui você pode adicionar a lógica para enviar os detalhes da transação para o backend
  }

  // Form
  pixForm!: FormGroup;
  showAlert:boolean = false;
  
  ngOnInit() {
    this.pixForm = new FormGroup({
      'valor': new FormControl(null,
        [
          Validators.required,
          Validators.pattern('^[0-9]*$'),
          Validators.minLength(1),
        ]),
      'chavePix': new FormControl(null,
        [
          Validators.required,
          Validators.pattern('^[a-zA-Z0-9.@_-]*$'),
          Validators.minLength(6),
        ]),
    });
  }
  
  onSubmit(): void {
    console.log(this.pixForm);
    this.pixForm.markAllAsTouched();
    if(this.pixForm.valid) {
      const pixData: Pix = {
        valor: this.pixForm.get('valor')?.value,
        chavePix: this.pixForm.get('chavePix')?.value,
        descricao: this.pixForm.get('descricao')?.value,
      }
    };
  
    this.pixService.efetuarPix(this.valor, this.chavePix)
      .subscribe((pix: Pix) => {
        setTimeout(() => {
          this.route.navigateByUrl('pix')
        }, 1000)
        this.showAlert = true;
        console.log(pix);
      });
  }
}