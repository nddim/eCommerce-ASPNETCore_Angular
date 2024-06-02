import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProizvodiNoviPageComponent } from './proizvodi-novi-page.component';

describe('ProizvodiNoviPageComponent', () => {
  let component: ProizvodiNoviPageComponent;
  let fixture: ComponentFixture<ProizvodiNoviPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProizvodiNoviPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProizvodiNoviPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
