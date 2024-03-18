import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DepositoService {

  private url: string = 'http://localhost:5062/contas'; // URL do serviço de depósitos

  // http://localhost:5062/contas/depositar?contaId=2

  constructor(private http: HttpClient) { }

  depositar(dadosDeposito: any): Observable<any> {
    return this.http.put<any>(`${this.url}/depositar?contaId=${dadosDeposito.contaId}`, dadosDeposito); // Faz uma requisição PUT para o endpoint 'depositar'
  }
}
