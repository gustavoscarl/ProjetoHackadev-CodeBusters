import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Login } from '../modelos/Login';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  


  constructor(private http: HttpClient) { }

  logarCliente(cliente: Login): Observable<Login> {
    const url = 'https://localhost:7085/auth';
    return this.http.post<Login>(url, cliente);
  }
}