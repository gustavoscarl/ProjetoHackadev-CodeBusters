import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../auth.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HomePageService } from '../../servicos/homepage-conta.service';
@Component({
  selector: 'app-icons-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './icons-home.component.html',
  styleUrl: './icons-home.component.css'
})

export class IconsHomeComponent {
  clienteData: any;
  isUserAccount = true;
  isUserCard = false;

  constructor(private contaService: HomePageService) { }

  ngOnInit() {
    this.contaService.pegarCliente().subscribe({
      next: (data) => {
        this.clienteData = data; 
        console.log(data);
      },
      error: (error) => {
        console.error('Error fetching client data:', error);
        // Handle errors, show messages, etc.
      }
    });
  }
  

}