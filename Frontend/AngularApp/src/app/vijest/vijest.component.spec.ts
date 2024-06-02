import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VijestComponent } from './vijest.component';

describe('VijestComponent', () => {
  let component: VijestComponent;
  let fixture: ComponentFixture<VijestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VijestComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VijestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
