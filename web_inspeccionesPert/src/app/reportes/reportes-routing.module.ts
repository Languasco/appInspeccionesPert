import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../guards/auth.guard';
import { PagesComponent } from './pages/pages.component';
import { ReportesDashboardComponent } from './pages/reportes-dashboard/reportes-dashboard.component';
import { ReportesInspeccionesComponent } from './pages/reportes-inspecciones/reportes-inspecciones.component';
import { ReportesIpalComponent } from './pages/reportes-ipal/reportes-ipal.component';

const routes: Routes = [
  {
    path : '', component : PagesComponent,
    children : [
      { path: 'ReporteInspecciones', component: ReportesInspeccionesComponent, canActivate: [ AuthGuard] }, 
      { path: 'ReporteIpal', component: ReportesIpalComponent, canActivate: [ AuthGuard] }, 
      { path: 'ReporteDashboard', component: ReportesDashboardComponent, canActivate: [ AuthGuard] }, 
      { path: '**', redirectTo: 'home' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportesRoutingModule { }
