import { Component } from '@angular/core';
import { HistoricoComponent } from '../historico/historico.component';
import { ResumoHistComponent } from '../resumo-hist/resumo-hist.component';

@Component({
  selector: 'app-historico-page',
  standalone: true,
  imports: [HistoricoComponent, ResumoHistComponent],
  templateUrl: './historico-page.component.html',
  styleUrl: './historico-page.component.css'
})
export class HistoricoPageComponent {

}
