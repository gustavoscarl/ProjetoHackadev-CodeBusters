import { Component } from '@angular/core';
import { CardpageComponent } from '../cardpage/cardpage.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-cartao',
  standalone: true,
  imports: [CardpageComponent, RouterModule],
  templateUrl: './cartao.component.html',
  styleUrl: './cartao.component.css'
})
export class CartaoComponent {

}
