import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransacaoComponent } from './transacao.component';

describe('TransacaoComponent', () => {
  let component: TransacaoComponent;
  let fixture: ComponentFixture<TransacaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TransacaoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TransacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
