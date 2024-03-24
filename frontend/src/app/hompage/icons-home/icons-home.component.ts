import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../auth.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HomePageService } from '../../servicos/homepage-conta.service';
import { Cliente } from '../../modelos/Cliente';
import { InputserviceService } from '../../servicos/inputservice.service';
@Component({
  selector: 'app-icons-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './icons-home.component.html',
  styleUrl: './icons-home.component.css'
})

export class IconsHomeComponent {
  clienteData: any | undefined;
  isUserAccount?:boolean;
  userName?: string;




  constructor(private contaService: HomePageService, private inputService: InputserviceService) { }

  ngOnInit() {
    this.contaService.pegarCliente().subscribe({
      next: ((data: any) => {
        console.log(data)
        this.clienteData = data;
        console.log(this.clienteData)
        this.isUserAccount = this.clienteData.temConta;
        this.inputService.nomeDoUsuario = this.clienteData.nome
        this.inputService.temConta = this.clienteData.temConta
        this.inputService.enviarDadosProntos()
      }),
      error: (error) => {
        console.error('Error fetching client data:', error);
      }
    });
  }
}
  
