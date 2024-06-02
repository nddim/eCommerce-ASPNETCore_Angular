import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KontaktPageComponent } from './kontakt-page.component';

describe('KontaktPageComponent', () => {
  let component: KontaktPageComponent;
  let fixture: ComponentFixture<KontaktPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KontaktPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(KontaktPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
