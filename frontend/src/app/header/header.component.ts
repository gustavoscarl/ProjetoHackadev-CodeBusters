import { Component } from '@angular/core';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [NgOptimizedImage, CommonModule, FormsModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

  // Exibir texto
    exibirTexto:boolean = false;
    exibirHistorico:boolean = false;

    // Função para exibir ou ocultar o texto
    visibilidadeTexto():void{
      this.exibirTexto = !this.exibirTexto;
    }

    paginaHistorico():void{
      this.exibirHistorico = !this.exibirHistorico;
    }
}
