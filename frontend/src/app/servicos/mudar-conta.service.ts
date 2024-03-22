import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CriarConta } from '../modelos/CriarConta';
import { MudarConta } from '../modelos/MudarConta';

@Injectable({
  providedIn: 'root'
})
export class MudarContaService {


  constructor(private http: HttpClient) { }

  cadastrarConta(conta: MudarConta): Observable<MudarConta> {
    const url = 'http://localhost:5062/contas/mudar';
    return this.http.put<MudarConta>(url, conta);
  }
}