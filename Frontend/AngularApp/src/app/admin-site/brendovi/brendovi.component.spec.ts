import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrendoviComponent } from './brendovi.component';

describe('BrendoviComponent', () => {
  let component: BrendoviComponent;
  let fixture: ComponentFixture<BrendoviComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BrendoviComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BrendoviComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
