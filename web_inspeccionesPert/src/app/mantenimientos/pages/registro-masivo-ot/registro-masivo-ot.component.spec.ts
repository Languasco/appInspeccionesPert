import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistroMasivoOTComponent } from './registro-masivo-ot.component';

describe('RegistroMasivoOTComponent', () => {
  let component: RegistroMasivoOTComponent;
  let fixture: ComponentFixture<RegistroMasivoOTComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegistroMasivoOTComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistroMasivoOTComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
