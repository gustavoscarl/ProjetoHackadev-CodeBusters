import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class InputserviceService {
  title = 'frontend';

  private dadosProntos = new Subject<boolean>();

  dadosProntos$ = this.dadosProntos.asObservable();

  enviarDadosProntos() {
    this.dadosProntos.next(true);
  }

  temInvestimento?:boolean;
  temConta?:boolean;
  nomeDoUsuario?: string;
  saldoDoUsuario?: number;
  constructor() { }
}
