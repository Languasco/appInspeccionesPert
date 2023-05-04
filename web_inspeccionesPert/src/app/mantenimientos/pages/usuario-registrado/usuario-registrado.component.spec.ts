import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsuarioRegistradoComponent } from './usuario-registrado.component';

describe('UsuarioRegistradoComponent', () => {
  let component: UsuarioRegistradoComponent;
  let fixture: ComponentFixture<UsuarioRegistradoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UsuarioRegistradoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UsuarioRegistradoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
