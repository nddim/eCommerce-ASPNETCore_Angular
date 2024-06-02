import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KupacPostavkeComponent } from './kupac-postavke.component';

describe('KupacPostavkeComponent', () => {
  let component: KupacPostavkeComponent;
  let fixture: ComponentFixture<KupacPostavkeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KupacPostavkeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(KupacPostavkeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
