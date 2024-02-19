import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';


import { UserBalanceComponent } from './hompage/user-balance/user-balance.component';
import { ContainerComponentComponent } from './hompage/container-component/container-component.component';
import { IconsHomeComponent } from "./hompage/icons-home/icons-home.component";
import { HeaderComponent } from './header/header.component';
import { LoginComponent } from './login/login.component';
import { Component } from '@angular/core';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [CommonModule, RouterOutlet, UserBalanceComponent, ContainerComponentComponent, IconsHomeComponent, HeaderComponent, LoginComponent]
})

export class AppComponent {
}
