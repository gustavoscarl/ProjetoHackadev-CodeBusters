import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from '../auth.service';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {
  constructor(private authService: AuthService) {}

  // Método de login para usuário de cartão
  loginUserCard(): void {
    this.authService.loginAsUserCard();
    // redirecionamento
  }

  // // Método de login para usuário de conta
  // loginUserAccount(): void {
  //   this.authService.loginAsUserAccount();
  //   // redirecionamento
  // }
}
