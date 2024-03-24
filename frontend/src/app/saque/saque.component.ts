import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { NgxCurrencyDirective } from 'ngx-currency';
import { NgxMaskDirective } from 'ngx-mask';

@Component({
  selector: 'app-saque',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, NgxCurrencyDirective, NgxMaskDirective],
  templateUrl: './saque.component.html',
  styleUrl: './saque.component.css'
})
export class SaqueComponent {
  saqueForm!: FormGroup

  constructor() { }

  ngOnInit(){
    // this.saqueForm = new FormGroup({
    //   'valor': new FormControl(null, 
    //     [
    //     Validators.required,
    //   ]),
    //   'tempo': new FormControl(null, 
    //     [
    //     Validators.required,
    //   ]),
    //   'pin': new FormControl(null,
    //     [
    //     Validators.required,
    //   ])
    // })
  }

  ordenarSaque(): void {


  }
}
