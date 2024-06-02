import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VijestPageComponent } from './vijest-page.component';

describe('VijestPageComponent', () => {
  let component: VijestPageComponent;
  let fixture: ComponentFixture<VijestPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VijestPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VijestPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
