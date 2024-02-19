import { Component, Input } from '@angular/core';
import { UserBalanceComponent } from "../user-balance/user-balance.component";
import { IconsHomeComponent } from "../icons-home/icons-home.component";
import { HelperComponent } from '../helper/helper.component';


@Component({
    selector: 'app-container-component',
    standalone: true,
    templateUrl: './container-component.component.html',
    styleUrl: './container-component.component.css',
    imports: [UserBalanceComponent, IconsHomeComponent, HelperComponent]
})
export class ContainerComponentComponent {
  @Input() saldoAtual: number = 0;
  @Input() userName!: string;

}
