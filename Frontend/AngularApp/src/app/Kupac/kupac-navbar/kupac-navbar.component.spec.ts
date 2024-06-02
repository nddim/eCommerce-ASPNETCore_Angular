import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KupacNavbarComponent } from './kupac-navbar.component';

describe('KupacNavbarComponent', () => {
  let component: KupacNavbarComponent;
  let fixture: ComponentFixture<KupacNavbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KupacNavbarComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(KupacNavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
