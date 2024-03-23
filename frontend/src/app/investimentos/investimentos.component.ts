import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-investimentos',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './investimentos.component.html',
  styleUrl: './investimentos.component.css'
})
export class InvestimentosComponent {


  constructor(){}

  realizarInvestimento(){}
}
