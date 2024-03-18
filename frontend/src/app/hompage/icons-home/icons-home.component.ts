import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../auth.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';


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