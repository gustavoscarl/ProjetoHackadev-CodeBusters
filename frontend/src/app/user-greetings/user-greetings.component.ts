import { Component, Input} from '@angular/core';

@Component({
  selector: 'app-user-greetings',
  standalone: true,
  imports: [],
  templateUrl: './user-greetings.component.html',
  styleUrl: './user-greetings.component.css'
})
export class UserGreetingsComponent {
  @Input() userName!: string;
}
