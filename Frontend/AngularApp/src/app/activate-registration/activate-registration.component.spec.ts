import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActivateRegistrationComponent } from './activate-registration.component';

describe('ActivateRegistrationComponent', () => {
  let component: ActivateRegistrationComponent;
  let fixture: ComponentFixture<ActivateRegistrationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ActivateRegistrationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ActivateRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
