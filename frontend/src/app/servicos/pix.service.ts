import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pix } from '../modelos/Pix';

@Injectable({
  providedIn: 'root'
})

export class PixService {

  constructor(private http: HttpClient) { }

  efetuarPix(pix: any): Observable<any> {
    const url = 'http://localhost:5062/pix';
    return this.http.post<any>(url, pix);
  }
}