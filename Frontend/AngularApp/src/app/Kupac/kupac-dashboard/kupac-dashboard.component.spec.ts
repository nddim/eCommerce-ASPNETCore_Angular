import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KupacDashboardComponent } from './kupac-dashboard.component';

describe('KupacDashboardComponent', () => {
  let component: KupacDashboardComponent;
  let fixture: ComponentFixture<KupacDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KupacDashboardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(KupacDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
