import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgxMaskDirective, NgxMaskPipe } from 'ngx-mask';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterModule, NgxMaskDirective, NgxMaskPipe, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

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
}
