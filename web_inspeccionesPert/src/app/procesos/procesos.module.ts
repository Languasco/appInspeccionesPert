import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { ComponentsModule } from '../components/components.module';

//--- tabs
import { TabsModule } from 'ngx-bootstrap/tabs'; 

//----- fechas datetimePicker ---
import { BsDatepickerModule, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { esLocale } from 'ngx-bootstrap/locale';
defineLocale('es', esLocale);

import { ProcesosRoutingModule } from './procesos-routing.module';
import { PagesComponent } from './pages/pages.component';
import { BandejaAtencionInspeccionesComponent } from './pages/bandeja-atencion-inspecciones/bandeja-atencion-inspecciones.component';
import { ListadoInspeccionesComponent } from './pages/listado-inspecciones/listado-inspecciones.component';


@NgModule({
  declarations: [
    PagesComponent,
    BandejaAtencionInspeccionesComponent,
    ListadoInspeccionesComponent
  ],
  imports: [
    CommonModule,
    ProcesosRoutingModule,
    ComponentsModule,
    FormsModule,
    ReactiveFormsModule,
    Ng2SearchPipeModule,
    BsDatepickerModule.forRoot(),
    TabsModule.forRoot(),
  ]
})
export class ProcesosModule { 

  constructor( private bsLocaleService: BsLocaleService){
    this.bsLocaleService.use('es');//fecha en español, datepicker
  }

}
