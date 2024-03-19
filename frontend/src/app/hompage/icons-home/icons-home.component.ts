import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../auth.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HomePageService } from '../../servicos/homepage-conta.service';
import { Cliente } from '../../modelos/Cliente';
@Component({
  selector: 'app-icons-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './icons-home.component.html',
  styleUrl: './icons-home.component.css'
})

export class IconsHomeComponent {
  clienteData: Cliente | undefined;
  isUserCard?:boolean;
  isUserAccount?:boolean;



  constructor(private contaService: HomePageService) { }

  ngOnInit() {
    this.contaService.pegarCliente().subscribe({
      next: ((data: Cliente) => {
        this.clienteData = data;
        console.log(this.clienteData);
        this.isUserCard = this.clienteData?.temCartao;
        this.isUserAccount = this.clienteData?.temConta;
      }),
      error: (error) => {
        console.error('Error fetching client data:', error);
      }
    });
    
  }
  
  

}