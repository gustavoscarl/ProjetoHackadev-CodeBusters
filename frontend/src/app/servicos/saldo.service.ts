import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Endereco } from '../modelos/Endereco';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SaldoService {

  constructor(private http:HttpClient) { }

  // Método para retornar endereço via CEP
  getSaldo():Observable<any>{
    return this.http.get<any>(`http://localhost:5062/contas/saldo`)
  }
}
