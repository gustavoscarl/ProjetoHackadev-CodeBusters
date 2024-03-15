import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';


@Component({
  selector: 'app-deposito',
  templateUrl: './deposito.component.html',
  styleUrls: ['./deposito.component.css']
})
export class DepositoComponent implements OnInit {

  depositoForm: FormGroup;

  constructor() {
    this.depositoForm = new FormGroup({
      tipoConta: new FormControl('corrente'),
      valorDeposito: new FormControl(''),
      contaOrigem: new FormControl('contaNubank'),
      agendarDeposito: new FormControl(false),
    });
  }

  ngOnInit(): void {
  }

  onSubmit(): void {
    // TODO: Implementar a lógica de envio do formulário
  }

}
