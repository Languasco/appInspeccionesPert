import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TipoFormatoComponent } from './tipo-formato.component';

describe('TipoFormatoComponent', () => {
  let component: TipoFormatoComponent;
  let fixture: ComponentFixture<TipoFormatoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TipoFormatoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TipoFormatoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
