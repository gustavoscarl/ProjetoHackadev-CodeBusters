import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Saque } from '../modelos/Saque';

@Injectable({
  providedIn: 'root'
})
export class SaqueService {


  httpOptions = {
    headers: new HttpHeaders({
      'Access-Control-Allow-Headers': '*',
      'Access-Control-Allow-Credentials': 'true',
    }),
    withCredentials: true
  }

  constructor(private http: HttpClient) { }

  efetuarSaque(saque: Saque): Observable<Saque> {
    const url = 'http://localhost:5062/contas/sacar';
    return this.http.put<Saque>(url, saque, this.httpOptions);
  }
}