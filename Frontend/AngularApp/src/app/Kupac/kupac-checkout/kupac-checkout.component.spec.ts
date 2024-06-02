import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KupacCheckoutComponent } from './kupac-checkout.component';

describe('KupacCheckoutComponent', () => {
  let component: KupacCheckoutComponent;
  let fixture: ComponentFixture<KupacCheckoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KupacCheckoutComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KupacCheckoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
