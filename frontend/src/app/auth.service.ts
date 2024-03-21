import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

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

  getRefreshToken(){
    return this.cookie.get('RefreshToken')
  }

  renovarToken(token: string){
    const httpOptions ={
      withCredentials: true,
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    return this.http.post<any>(`http://localhost:5062/auth/refresh`, token, httpOptions)
  }


  estaLogado(): boolean {
    return !!localStorage.getItem('token')
  }

  logout():void{
    localStorage.removeItem('token')
    this.route.navigate(['login'])
  }

}