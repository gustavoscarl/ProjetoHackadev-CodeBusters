import { Injectable } from '@angular/core';
import { Router, NavigationEnd, RouterEvent } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { filter } from 'rxjs/operators';
import { MenuItem } from '../Interface/MenuHeader';

@Injectable({
  providedIn: 'root'
})
export class HeaderService {

  private menuItemsSubject = new BehaviorSubject<MenuItem[]>([]);
  menuItems$ = this.menuItemsSubject.asObservable();

  constructor(private router: Router) {
    this.router.events.pipe(
      filter((event): event is NavigationEnd => event instanceof NavigationEnd)
    ).subscribe((event: NavigationEnd) => {
      this.updateMenuItems(event.urlAfterRedirects);
    });
  }
  
    // lógica para atualizar os itens do menu com base na URL

  private updateMenuItems(url: string) {
    if (url.startsWith('/login')) {[
      {name: "home", route: "/home" },
    ]}
    if (url.startsWith('/home')) {
      this.menuItemsSubject.next([
      { name: "Sua Conta", route: "/conta/criada" },
      { name: "Pix", route: "/pix" },
      { name: "Cartão", route: "/cartao" },
      { name: "Histórico", route: "/historico" },
      { name: "Saque", route: "/saque" }
    ]);
    }
    if (url.startsWith('/cartao')) {
      this.menuItemsSubject.next([
      {name: "home", route: "/home" },
      { name: "Sua Conta", route: "/conta/criada" },
      { name: "Pix", route: "/pix" },
      { name: "Histórico", route: "/historico" },
      { name: "Saque", route: "/saque" }
    ]);
    }  
  }

};
