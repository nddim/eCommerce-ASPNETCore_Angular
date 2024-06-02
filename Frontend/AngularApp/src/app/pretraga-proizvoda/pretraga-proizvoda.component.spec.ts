import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PretragaProizvodaComponent } from './pretraga-proizvoda.component';

describe('PretragaProizvodaComponent', () => {
  let component: PretragaProizvodaComponent;
  let fixture: ComponentFixture<PretragaProizvodaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PretragaProizvodaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PretragaProizvodaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
