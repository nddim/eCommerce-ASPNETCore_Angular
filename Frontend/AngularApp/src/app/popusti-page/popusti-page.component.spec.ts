import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopustiPageComponent } from './popusti-page.component';

describe('PopustiPageComponent', () => {
  let component: PopustiPageComponent;
  let fixture: ComponentFixture<PopustiPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PopustiPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PopustiPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
