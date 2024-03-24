import { Component, HostBinding, HostListener, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { isPlatformBrowser } from '@angular/common';
import { NavigationEnd, NavigationStart, RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { filter } from 'rxjs';
import { HeaderService } from '../servicos/header.service';
@Component({
  selector: 'app-header',
  standalone: true,
  imports: [NgOptimizedImage, CommonModule, FormsModule, RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  
  
  darkMode: boolean;
  
  menuValue:boolean = false;
  menu_icon:string = 'bi bi-list';

  openMenu() {
    this.menuValue = !this.menuValue;
    this.menu_icon = (this.menu_icon === 'bi bi-list') ? 'bi bi-x' : 'bi bi-list';
  }

  closeMenu(){
    this.menuValue = false;
    this.menu_icon = 'bi bi-list';
  }

  constructor(@Inject(PLATFORM_ID) private platformId: Object, private route: Router, public headerService: HeaderService) {
    // Define se o histórico deve ser exibido com base na rota atual
    this.exibirHistorico = this.deveExibirHistorico();
    this.exibirHome = this.deveExibirHome();
    
    this.route.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      this.loginHeader();
      this.exibirHome = this.deveExibirHome();
      this.exibirHistorico = this.deveExibirHistorico();
    });

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
    exibirHome:boolean = true;
    exibirHistorico:boolean = false;

    // Funções para exibir ou ocultar o texto
    loginHeader(): void {
      this.exibirHome = !this.route.url.includes('/login'); // Hide 'Home' when the route is '/login'
      this.exibirHistorico = false; // Always hide 'Historico' in the login route
    }

    deveExibirHome():boolean{
      return this.route.url === '/home';
    }

    deveExibirHistorico(): boolean {
      // Substitua '/alguma-rota' pela rota específica que deseja verificar
      return this.route.url === '/historico';
    };

    
}
