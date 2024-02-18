import { Component, HostBinding, HostListener, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { isPlatformBrowser } from '@angular/common';
@Component({
  selector: 'app-header',
  standalone: true,
  imports: [NgOptimizedImage, CommonModule, FormsModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

  darkMode: boolean;

  constructor(@Inject(PLATFORM_ID) private platformId: Object) {
    this.darkMode = isPlatformBrowser(this.platformId) && window.matchMedia('(prefers-color-scheme: dark)').matches;
    
    //Adiciona um ouvinte de evento para detectar alterações no tema do sistema
    if (isPlatformBrowser(this.platformId)) {
      window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', (e) => {
        this.handleThemeChange(e);
      });
    }
  }
  
  //Função para alternar os logos dark e light mode
  getImagePath(): string {
    return this.darkMode ? 'assets/img_header/logoDarkMode.svg' : 'assets/img_header/logoLightMode.svg';
  }

  //Função para alternar a lupa do dark e light mode
  getLupaPath(): string {
    return this.darkMode ? 'assets/img_header/lupaDark.svg' : 'assets/img_header/lupaLight.svg';
  }

  @HostListener('window:load', ['$event'])
  handleThemeChange(event: Event) {
    if (isPlatformBrowser(this.platformId)) {
      // Atualiza a variável darkMode quando o tema do sistema é alterado
      this.darkMode = window.matchMedia('(prefers-color-scheme: dark)').matches;
    }
  }
  
  // Exibir texto
    exibirTexto:boolean = false;
    exibirHistorico:boolean = false;

    // Funções para exibir ou ocultar o texto
    visibilidadeTexto():void{
      this.exibirTexto = !this.exibirTexto;
    }

    paginaHistorico():void{
      this.exibirHistorico = !this.exibirHistorico;
    }
}
