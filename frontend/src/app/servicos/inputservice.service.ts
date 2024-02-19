import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class InputserviceService {
  title = 'frontend';
  //Criando nome de usuario
  nomeDoUsuario: string = 'João';
  // Criando saldo do usuario
  saldoDoUsuario: number = 20000;
  constructor() { }
}
