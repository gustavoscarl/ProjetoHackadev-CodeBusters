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
  private userCard: boolean = false;

  constructor(private http: HttpClient, private route: Router, private cookie: CookieService) {}

  isUserCard(): boolean {
    return this.userCard
  }

  loginAsUserCard(): void {
    this.userCard = true;
  }

  guardarToken(tokenValue: string){
    localStorage.setItem('token', tokenValue);
  }

  getToken():any{
    return localStorage.getItem('token')
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
    return !!localStorage.getItem('token')
  }

  logout():void{
    localStorage.removeItem('token')
    this.cookie.delete('RefreshToken')
    this.route.navigate(['login'])
  }

}