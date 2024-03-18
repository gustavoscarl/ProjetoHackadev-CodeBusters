import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private userCard: boolean = false;

  constructor() {}

  // Método para verificar se o usuário é somente de cartão
  isUserCard(): boolean {
    return this.userCard
  }

  // // Método para verificar se o usuário é usuário de conta
  // isUserAccount(): boolean {
  //   return this.userAccount
  // }

  // Método para simular o login como usuário de cartão
  loginAsUserCard(): void {
    this.userCard = true;
  }

  // // Método para simular o login como usuário de conta
  // loginAsUserAccount(): void {
  //   this.userAccount = false;
  // }

}