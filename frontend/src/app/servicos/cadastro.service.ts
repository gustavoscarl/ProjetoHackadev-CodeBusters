import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cadastro } from '../modelos/Cadastro';

@Injectable({
  providedIn: 'root'
})
export class CadastroService {


  constructor(private http: HttpClient) { }

  cadastrarCliente(cliente: Cadastro): Observable<Cadastro> {
    const url = 'http://localhost:7085/clientes';
    return this.http.post<Cadastro>(url, cliente, this.httpOptions);
  }
}