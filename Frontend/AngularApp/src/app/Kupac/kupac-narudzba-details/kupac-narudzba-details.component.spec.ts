import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KupacNarudzbaDetailsComponent } from './kupac-narudzba-details.component';

describe('KupacNarudzbaDetailsComponent', () => {
  let component: KupacNarudzbaDetailsComponent;
  let fixture: ComponentFixture<KupacNarudzbaDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KupacNarudzbaDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(KupacNarudzbaDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
