import { Component } from '@angular/core';
import { CartaoComponent } from '../cartao/cartao.component';

@Component({
  selector: 'app-cardpage',
  standalone: true,
  imports: [CartaoComponent],
  templateUrl: './cardpage.component.html',
  styleUrl: './cardpage.component.css'
})
export class CardpageComponent {

}
