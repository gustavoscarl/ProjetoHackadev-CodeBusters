import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { NgxCurrencyDirective } from 'ngx-currency';
import { NgxMaskDirective } from 'ngx-mask';
import { SaqueService } from '../../servicos/saque.service';
import { Route, Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-saque',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, NgxCurrencyDirective, NgxMaskDirective, RouterModule],
  templateUrl: './saque.component.html',
  styleUrl: './saque.component.css'
})
export class SaqueComponent {
  saqueForm!: FormGroup

  constructor(private saqueService: SaqueService, private route: Router) { }

  ngOnInit(){
    this.saqueForm = new FormGroup({
      'valor': new FormControl(null, 
        [
        Validators.required,
      ]),
      'descricao': new FormControl(""),
      'pin': new FormControl(null,
        [
        Validators.required,
      ])
    })
  }

  ordenarSaque(): void {
    if (this.saqueForm.valid) {
      this.saqueService.efetuarSaque(this.saqueForm.value)
        .subscribe(
          retorno => {
            setTimeout(() => {
              alert('Saque feito com sucessso!')
              this.route.navigateByUrl('home');
            }, 250);
            console.log(retorno);
          },
          error => {
            console.log(error)
            alert('Erro ao efetuar saque: ' + error.error.message);
          }
        );
    }
  }
}

