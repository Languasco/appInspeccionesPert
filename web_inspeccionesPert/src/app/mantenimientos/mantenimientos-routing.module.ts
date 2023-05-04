import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../guards/auth.guard';
import { AnomaliasComponent } from './pages/anomalias/anomalias.component';
import { EmpresaComponent } from './pages/empresa/empresa.component';
import { PagesComponent } from './pages/pages.component';
import { PaisComponent } from './pages/pais/pais.component';
import { PersonalComponent } from './pages/personal/personal.component';
import { TipoSancionComponent } from './pages/tipo-sancion/tipo-sancion.component';
import { TipoInspeccionComponent } from './pages/tipo-inspeccion/tipo-inspeccion.component';

const routes: Routes = [
  {
    path : '', component : PagesComponent,
    children : [
      { path: 'Pais', component: PaisComponent, canActivate: [ AuthGuard] },
      { path: 'Empresas', component: EmpresaComponent, canActivate: [ AuthGuard] },
      { path: 'Anomalia', component: AnomaliasComponent, canActivate: [ AuthGuard] },      
      { path: 'TipoSancion', component: TipoSancionComponent, canActivate: [ AuthGuard] },   
      { path: 'Personal', component: PersonalComponent, canActivate: [ AuthGuard] },   
      { path: 'TipoInspeccion', component: TipoInspeccionComponent, canActivate: [ AuthGuard] },   
      { path: '**', redirectTo: 'home' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MantenimientosRoutingModule { }
