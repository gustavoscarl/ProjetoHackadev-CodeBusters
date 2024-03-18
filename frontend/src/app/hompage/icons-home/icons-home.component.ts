import { Component, OnInit, ModuleWithProviders } from '@angular/core';
import { AuthService } from '../../auth.service';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { ContainerComponentComponent } from '../container-component/container-component.component';
import { CriarContaComponent } from '../../conta/criar-conta/criar-conta.component';
import { ContaCriadaComponent } from '../../conta/conta-criada/conta-criada.component';

@Component({
  selector: 'app-icons-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './icons-home.component.html',
  styleUrl: './icons-home.component.css'
})

export class IconsHomeComponent implements OnInit {
  isUserCard: boolean = false;
  isUserAccount: boolean = true;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.isUserCard = this.authService.isUserCard();
  }

}