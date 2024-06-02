import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GlavneKategorijeComponent } from './glavne-kategorije.component';

describe('GlavneKategorijeComponent', () => {
  let component: GlavneKategorijeComponent;
  let fixture: ComponentFixture<GlavneKategorijeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GlavneKategorijeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GlavneKategorijeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
