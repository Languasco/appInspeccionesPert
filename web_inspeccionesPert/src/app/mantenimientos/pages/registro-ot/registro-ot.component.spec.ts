import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistroOTComponent } from './registro-ot.component';

describe('RegistroOTComponent', () => {
  let component: RegistroOTComponent;
  let fixture: ComponentFixture<RegistroOTComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegistroOTComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistroOTComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
