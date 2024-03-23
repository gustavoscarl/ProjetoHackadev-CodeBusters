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

  alterarConta(conta: MudarConta): Observable<MudarConta> {
    const url = 'https://localhost:7085/contas/alterar/limites';
    return this.http.put<MudarConta>(url, conta);
  }

  excluirConta(): any{
    const url = 'https://localhost:7085/contas'
    return this.http.delete(url)
  }
}