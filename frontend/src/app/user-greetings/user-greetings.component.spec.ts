import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserGreetingsComponent } from './user-greetings.component';

describe('UserGreetingsComponent', () => {
  let component: UserGreetingsComponent;
  let fixture: ComponentFixture<UserGreetingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserGreetingsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserGreetingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
