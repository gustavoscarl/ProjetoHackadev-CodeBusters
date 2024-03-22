import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-saque',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './saque.component.html',
  styleUrl: './saque.component.css'
})
export class SaqueComponent {
  
  valor: number | undefined;
  descricao: string | undefined;
  data:number = Date.now();

  constructor() { }

  ordenarSaque(): void {

    console.log('Saque em conta realizado:', { valor: this.valor, descricao: this.descricao, dia: this.data });

  }
}
