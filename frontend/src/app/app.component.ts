import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HistoricoComponent } from './componentes/historico/historico.component';
import { TransacaoComponent } from "./componentes/transacao/transacao.component";
import { Transacao } from './modelo/Transacoes';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [CommonModule, RouterOutlet, HistoricoComponent, TransacaoComponent]
})
export class AppComponent {
  title = 'frontend';  
}
