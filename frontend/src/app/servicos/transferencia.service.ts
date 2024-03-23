import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Transferencia } from '../modelos/Transferencia';

@Injectable({
  providedIn: 'root'
})
export class TransferenciaService {

  private url: string = 'http://localhost:5062/contas'; // URL do serviço de depósitos

  constructor(private http: HttpClient) { }

  transferir(dadosTransferencia: Transferencia): Observable<any> {
    return this.http.put<Transferencia>(`${this.url}/transferir`, dadosTransferencia);
  }
}
