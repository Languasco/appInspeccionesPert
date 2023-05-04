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


import { ReportesRoutingModule } from './reportes-routing.module';
import { PagesComponent } from './pages/pages.component';
import { ReportesInspeccionesComponent } from './pages/reportes-inspecciones/reportes-inspecciones.component';
import { ReportesIpalComponent } from './pages/reportes-ipal/reportes-ipal.component';
import { ReportesDashboardComponent } from './pages/reportes-dashboard/reportes-dashboard.component';


@NgModule({
  declarations: [
    PagesComponent,
    ReportesInspeccionesComponent,
    ReportesIpalComponent,
    ReportesDashboardComponent
  ],
  imports: [
    ReportesRoutingModule,
    CommonModule,
    ComponentsModule,
    FormsModule,
    ReactiveFormsModule,
    Ng2SearchPipeModule,
    BsDatepickerModule.forRoot(),
    TabsModule.forRoot(),

  ]
})
export class ReportesModule {
  constructor( private bsLocaleService: BsLocaleService){
    this.bsLocaleService.use('es');//fecha en español, datepicker
  }
 }
