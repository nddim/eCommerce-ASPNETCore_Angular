import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResetLozinkeComponent } from './reset-lozinke.component';

describe('ResetLozinkeComponent', () => {
  let component: ResetLozinkeComponent;
  let fixture: ComponentFixture<ResetLozinkeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ResetLozinkeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ResetLozinkeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
