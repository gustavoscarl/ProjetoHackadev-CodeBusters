import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CriarConta } from '../modelos/CriarConta';

@Injectable({
  providedIn: 'root'
})
export class CriarContaService {


  constructor(private http: HttpClient) { }

  cadastrarConta(conta: CriarConta): Observable<CriarConta> {
    const url = 'https://localhost:7085/contas/criar';
    return this.http.post<CriarConta>(url, conta);
  }
}