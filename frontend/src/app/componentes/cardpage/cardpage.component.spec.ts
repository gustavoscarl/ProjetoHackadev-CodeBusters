import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardpageComponent } from './cardpage.component';

describe('CardpageComponent', () => {
  let component: CardpageComponent;
  let fixture: ComponentFixture<CardpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CardpageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CardpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
