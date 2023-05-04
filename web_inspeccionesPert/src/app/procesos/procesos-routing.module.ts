import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../guards/auth.guard';
import { BandejaAtencionInspeccionesComponent } from './pages/bandeja-atencion-inspecciones/bandeja-atencion-inspecciones.component';
import { ListadoInspeccionesComponent } from './pages/listado-inspecciones/listado-inspecciones.component';
import { PagesComponent } from './pages/pages.component';

const routes: Routes = [
  {
    path : '', component : PagesComponent,
    children : [
      { path: 'BandejaAtencionInspecciones', component: BandejaAtencionInspeccionesComponent, canActivate: [ AuthGuard] }, 
      { path: 'ListaInspecciones', component: ListadoInspeccionesComponent, canActivate: [ AuthGuard] }, 
      { path: '**', redirectTo: 'home' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProcesosRoutingModule { }
