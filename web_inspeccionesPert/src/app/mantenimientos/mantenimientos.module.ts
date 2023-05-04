import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Ng2SearchPipeModule } from 'ng2-search-filter';


import { MantenimientosRoutingModule } from './mantenimientos-routing.module';
import { PaisComponent } from './pages/pais/pais.component';
import { EmpresaComponent } from './pages/empresa/empresa.component';
import { PagesComponent } from './pages/pages.component';
import { ComponentsModule } from '../components/components.module';
import { AnomaliasComponent } from './pages/anomalias/anomalias.component';
import { TipoSancionComponent } from './pages/tipo-sancion/tipo-sancion.component';
import { PersonalComponent } from './pages/personal/personal.component';
import { CargosComponent } from './pages/cargos/cargos.component';
import { AreasComponent } from './pages/areas/areas.component';
import { TipoInspeccionComponent } from './pages/tipo-inspeccion/tipo-inspeccion.component';
import { TipoFormatoComponent } from './pages/tipo-formato/tipo-formato.component';
import { UsuarioRegistradoComponent } from './pages/usuario-registrado/usuario-registrado.component';
import { RegistroOTComponent } from './pages/registro-ot/registro-ot.component';
import { RegistroMasivoOTComponent } from './pages/registro-masivo-ot/registro-masivo-ot.component';
import { NumerosPersonalComponent } from './pages/numeros-personal/numeros-personal.component';


@NgModule({
  declarations: [
    PaisComponent,
    EmpresaComponent,
    PagesComponent,
    AnomaliasComponent,
    TipoSancionComponent,
    PersonalComponent,
    CargosComponent,
    AreasComponent,
    TipoInspeccionComponent,
    TipoFormatoComponent,
    UsuarioRegistradoComponent,
    RegistroOTComponent,
    RegistroMasivoOTComponent,
    NumerosPersonalComponent
  ],
  imports: [
    CommonModule,
    MantenimientosRoutingModule,
    ComponentsModule,
    FormsModule,
    ReactiveFormsModule,
    Ng2SearchPipeModule
  ]
})
export class MantenimientosModule { }
