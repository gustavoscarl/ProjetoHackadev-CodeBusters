import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PixService {

  private url: string = 'http://localhost:5062/contas'; // URL do serviço de PIX

  constructor(private http: HttpClient) { }

  transferenciaPix(dadosPix: any): Observable<any> {
    return this.http.post<any>(`${this.url}/pix`, dadosPix); // Faz uma requisição POST para o endpoint 'pix'
  }
}
