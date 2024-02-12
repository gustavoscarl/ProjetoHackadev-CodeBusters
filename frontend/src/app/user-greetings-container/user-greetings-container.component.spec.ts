import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserGreetingsContainerComponent } from './user-greetings-container.component';

describe('UserGreetingsContainerComponent', () => {
  let component: UserGreetingsContainerComponent;
  let fixture: ComponentFixture<UserGreetingsContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserGreetingsContainerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserGreetingsContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
