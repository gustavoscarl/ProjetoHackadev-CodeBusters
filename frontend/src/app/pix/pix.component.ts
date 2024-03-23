import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-pix',
  standalone: true,
  imports: [FormsModule, RouterModule],
  templateUrl: './pix.component.html',
  styleUrl: './pix.component.css'
})
export class PixComponent {
  valor: number | undefined;
  chavePix: string | undefined;
  descricao: string | undefined;

  constructor() { }

  realizarTransacaoPix(): void {
    // Lógica para realizar a transação PIX
    console.log('Transação PIX realizada:', { valor: this.valor, chavePix: this.chavePix, descricao: this.descricao });
    // Aqui você pode adicionar a lógica para enviar os detalhes da transação para o backend
  }
}
