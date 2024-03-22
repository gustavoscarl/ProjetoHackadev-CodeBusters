import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Login } from '../modelos/Login';

@Injectable({
  providedIn: 'root'
})
export class LoginService {


  httpOptions = {
    headers: new HttpHeaders({
      'Access-Control-Allow-Headers': '*',
      'Access-Control-Allow-Credentials': 'true',
    }),
    withCredentials: true
  }

  constructor(private http: HttpClient) { }

  logarCliente(cliente: Login): Observable<Login> {
    const url = 'http://localhost:5062/auth';
    return this.http.post<Login>(url, cliente, this.httpOptions);
  }
}