import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KupacSidebarComponent } from './kupac-sidebar.component';

describe('KupacSidebarComponent', () => {
  let component: KupacSidebarComponent;
  let fixture: ComponentFixture<KupacSidebarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KupacSidebarComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(KupacSidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
