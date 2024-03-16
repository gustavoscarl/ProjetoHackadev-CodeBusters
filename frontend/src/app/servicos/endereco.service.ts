import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Endereco } from '../modelos/Endereco';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EnderecoService {

  constructor(private http:HttpClient) { }

  // Método para retornar endereço via CEP
  retornarEndereco(cep:string):Observable<Endereco>{
    return this.http.get<Endereco>(`http://viacep.com.br/ws/${cep}/json/`)
  }
}
