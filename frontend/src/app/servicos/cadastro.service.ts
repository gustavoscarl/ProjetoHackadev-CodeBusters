import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cadastro } from '../modelos/Cadastro';

@Injectable({
  providedIn: 'root'
})
export class CadastroService {

  httpOptions = {
    headers: new HttpHeaders({
      'Access-Control-Allow-Headers': '*',
      'Access-Control-Allow-Credentials': 'true',
    }),
    withCredentials: true
  }

  constructor(private http: HttpClient) { }

  cadastrarCliente(cliente: Cadastro): Observable<Cadastro> {
    const url = 'https://localhost:7085/clientes';
    return this.http.post<Cadastro>(url, cliente, this.httpOptions);
  }
}