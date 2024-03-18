import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContaCriadaComponent } from './conta-criada.component';

describe('ContaCriadaComponent', () => {
  let component: ContaCriadaComponent;
  let fixture: ComponentFixture<ContaCriadaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContaCriadaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ContaCriadaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
