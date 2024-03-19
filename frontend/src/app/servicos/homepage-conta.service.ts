import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../auth.service';

@Injectable({
  providedIn: 'root'
})
export class HomePageService {

  private url: string = 'http://localhost:5062'; // URL do serviço de depósitos

  constructor(private http: HttpClient, private auth: AuthService) { }

  pegarCliente(): Observable<any> {
    let token: string = this.auth.getToken();
    console.log(token)
    return this.http.get<any>(`${this.url}/clientes`, { headers: { Authorization: `Bearer ${token}` } });
    
  }
}