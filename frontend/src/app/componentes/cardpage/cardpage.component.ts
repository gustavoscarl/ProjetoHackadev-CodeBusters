import { Component } from '@angular/core';
import { CartaoComponent } from '../cartao/cartao.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HistoricoComponent } from '../historico/historico.component';

@Component({
  selector: 'app-cardpage',
  standalone: true,
  imports: [CartaoComponent, ReactiveFormsModule, HistoricoComponent],
  templateUrl: './cardpage.component.html',
  styleUrl: './cardpage.component.css'
})
export class CardpageComponent {

}
