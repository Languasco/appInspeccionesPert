import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NumerosPersonalComponent } from './numeros-personal.component';

describe('NumerosPersonalComponent', () => {
  let component: NumerosPersonalComponent;
  let fixture: ComponentFixture<NumerosPersonalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NumerosPersonalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NumerosPersonalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
