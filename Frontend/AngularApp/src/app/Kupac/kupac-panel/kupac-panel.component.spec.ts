import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KupacPanelComponent } from './kupac-panel.component';

describe('KupacPanelComponent', () => {
  let component: KupacPanelComponent;
  let fixture: ComponentFixture<KupacPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KupacPanelComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(KupacPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
