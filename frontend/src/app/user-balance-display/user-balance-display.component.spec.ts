import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserBalanceDisplayComponent } from './user-balance-display.component';

describe('UserBalanceDisplayComponent', () => {
  let component: UserBalanceDisplayComponent;
  let fixture: ComponentFixture<UserBalanceDisplayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserBalanceDisplayComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserBalanceDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
