import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../auth.service';

@Injectable({
  providedIn: 'root'
})
export class HomePageService {

  private url: string = 'https://localhost:7085'; // URL do serviço de depósitos



  


  constructor(private http: HttpClient, private auth: AuthService) { }

  pegarCliente(): Observable<any> {
    let token: string = this.auth.getToken();
    return this.http.get<any>(`${this.url}/clientes`);
    
  }
}
