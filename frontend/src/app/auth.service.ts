import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private userCard: boolean = false;

  constructor(private http: HttpClient, private route: Router) {}

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

  estaLogado(): boolean {
    return !!localStorage.getItem('token')
  }

  logout():void{
    localStorage.removeItem('token')
    this.route.navigate(['login'])
  }

}