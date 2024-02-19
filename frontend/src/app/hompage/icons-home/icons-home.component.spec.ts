import { ComponentFixture, TestBed } from '@angular/core/testing';
import { IconsHomeComponent } from './icons-home.component';

describe('IconsHomeComponent', () => {
  let component: IconsHomeComponent;
  let fixture: ComponentFixture<IconsHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IconsHomeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(IconsHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
