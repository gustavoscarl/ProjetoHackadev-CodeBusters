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
    const url = 'http://localhost:5062/clientes';
    return this.http.post<Cadastro>(url, cliente);
  }
}