import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminVijestComponent } from './admin-vijest.component';

describe('AdminVijestComponent', () => {
  let component: AdminVijestComponent;
  let fixture: ComponentFixture<AdminVijestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminVijestComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdminVijestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
