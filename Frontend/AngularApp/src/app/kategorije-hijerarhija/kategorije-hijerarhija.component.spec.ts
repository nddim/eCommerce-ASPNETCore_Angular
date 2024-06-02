import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KategorijeHijerarhijaComponent } from './kategorije-hijerarhija.component';

describe('KategorijeHijerarhijaComponent', () => {
  let component: KategorijeHijerarhijaComponent;
  let fixture: ComponentFixture<KategorijeHijerarhijaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KategorijeHijerarhijaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(KategorijeHijerarhijaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
