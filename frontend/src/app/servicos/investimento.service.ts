import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Investimento } from '../modelos/Investimento';
import { InputserviceService } from './inputservice.service';

@Injectable({
  providedIn: 'root'
})
export class InvestimentosService {

  constructor(private http: HttpClient, private input: InputserviceService) { }

  getInformacoesInvestimento():Observable<any> {
    return this.http.get('http://localhost:5062/contas/investimentos')
  }

  fazerInvestimento(investimento: Investimento): Observable<Investimento> {
    const url = 'http://localhost:5062/contas/investimentos';
    return this.http.post<Investimento>(url, investimento)
  }

}
