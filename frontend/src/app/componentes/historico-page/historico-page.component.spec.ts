import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HistoricoPageComponent } from './historico-page.component';

describe('HistoricoPageComponent', () => {
  let component: HistoricoPageComponent;
  let fixture: ComponentFixture<HistoricoPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HistoricoPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HistoricoPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
