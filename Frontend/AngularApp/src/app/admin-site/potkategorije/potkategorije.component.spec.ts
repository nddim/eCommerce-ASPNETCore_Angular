import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PotkategorijeComponent } from './potkategorije.component';

describe('PotkategorijeComponent', () => {
  let component: PotkategorijeComponent;
  let fixture: ComponentFixture<PotkategorijeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PotkategorijeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PotkategorijeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
