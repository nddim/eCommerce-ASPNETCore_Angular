import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProizvodPretragaNazivComponent } from './proizvod-pretraga-naziv.component';

describe('ProizvodPretragaNazivComponent', () => {
  let component: ProizvodPretragaNazivComponent;
  let fixture: ComponentFixture<ProizvodPretragaNazivComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProizvodPretragaNazivComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProizvodPretragaNazivComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
