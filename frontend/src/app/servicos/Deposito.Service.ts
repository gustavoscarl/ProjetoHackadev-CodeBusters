import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DepositoService {

  private url: string = 'http://localhost:5062/contas'; // URL do serviço de depósitos

  constructor(private http: HttpClient) { }

  depositar(dadosDeposito: any): Observable<any> {
    return this.http.put<any>(`${this.url}/depositar`, dadosDeposito); // Faz uma requisição PUT para o endpoint 'depositar'
  }
}
