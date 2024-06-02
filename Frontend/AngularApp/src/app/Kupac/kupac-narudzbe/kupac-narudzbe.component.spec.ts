import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KupacNarudzbeComponent } from './kupac-narudzbe.component';

describe('KupacNarudzbeComponent', () => {
  let component: KupacNarudzbeComponent;
  let fixture: ComponentFixture<KupacNarudzbeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KupacNarudzbeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(KupacNarudzbeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
