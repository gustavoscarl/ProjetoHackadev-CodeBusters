import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { NgxMaskDirective, NgxMaskPipe } from 'ngx-mask';
import { Login } from '../modelos/Login';
import { LoginService } from '../servicos/login.service';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterModule, NgxMaskDirective, NgxMaskPipe, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  constructor (private loginService: LoginService, private route: Router) {}

  // Form
  loginForm!: FormGroup;
  showAlert:boolean = false;

  ngOnInit() {
    this.loginForm = new FormGroup({
      'cpf': new FormControl(null, 
        [
        Validators.required,
        Validators.pattern('^[0-9]+$'),
        Validators.minLength(11),
      ]),
      'senha': new FormControl(null, 
        [
        Validators.required,
      ]),
    })
  }

  onSubmit():void {
    console.log(this.loginForm)
    this.loginForm.markAllAsTouched();
    if (this.loginForm.valid) { 
      this.loginService.logarCliente(this.loginForm.value as Login)
        .subscribe(login => {
          setTimeout(() => {
            this.route.navigateByUrl('home')
          }, 2500)
          this.showAlert = true;
          console.log(login);
          // Lógica de sucesso após o POST
        });
    }
  }
}
