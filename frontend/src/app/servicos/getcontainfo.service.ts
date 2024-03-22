import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { InfoConta } from '../modelos/InfoConta';

@Injectable({
  providedIn: 'root'
})
export class ContaInfoService {

  constructor(private http: HttpClient) { }

  getInformacoes(): Observable<InfoConta> {
    return this.http.get<InfoConta>('http://localhost:5062/contas')
  }
}
