import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { Observable, map } from 'rxjs';
import { TokenApiModel } from './modelos/token-api.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {


  constructor(private http: HttpClient, private route: Router, private cookie: CookieService) {}


  guardarToken(tokenValue: string){
    this.cookie.set('token', tokenValue);
  }

  getToken():any{
    return this.cookie.get('token')
  }

  guardarRefreshToken(tokenValue: string){
    this.cookie.set('RefreshToken', tokenValue)
  }

  getRefreshToken(){
    return this.cookie.get('RefreshToken')
  }

  renewToken(): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Headers': '*',
        'Access-Control-Allow-Credentials': 'true',
      }),
      withCredentials: true
    };

    return this.http.post<any>('http://localhost:5062/auth/refresh', {}, httpOptions);
  }


  estaLogado(): boolean {
    return !!this.cookie.get('token')
  }

  logout():void{
    this.cookie.deleteAll();
    this.cookie.delete('token');
    this.cookie.delete('RefreshToken');
    this.route.navigate(['login']);
  }

}