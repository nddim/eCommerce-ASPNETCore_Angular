import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginFormaComponent } from './login-forma.component';

describe('LoginFormaComponent', () => {
  let component: LoginFormaComponent;
  let fixture: ComponentFixture<LoginFormaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoginFormaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LoginFormaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
