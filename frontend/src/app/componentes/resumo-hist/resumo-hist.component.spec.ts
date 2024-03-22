import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResumoHistComponent } from './resumo-hist.component';

describe('ResumoHistComponent', () => {
  let component: ResumoHistComponent;
  let fixture: ComponentFixture<ResumoHistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ResumoHistComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ResumoHistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
