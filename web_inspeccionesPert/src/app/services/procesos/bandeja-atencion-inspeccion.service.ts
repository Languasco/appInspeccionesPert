import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
 
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const HttpUploadOptions = {
  headers: new HttpHeaders({ "Content-Type": "multipart/form-data" })
}

@Injectable({
  providedIn: 'root'
})
 
export class BandejaAtencionInspeccionService {

  URL = environment.URL_API;
  proyectos :any[]=[]; 
  estadosInspecciones :any[]=[]; 
  nivelesInspecciones :any[]=[]; 
  personales :any[]=[]; 
  clientes :any[]=[]; 
  empresasContratistas :any[]=[]; 

  cargos :any[]=[]; 
  tecnicos :any[]=[]; 
  areas :any[]=[]; 

  tiposInspecciones :any[]=[]; 
  tiposFormatos :any[]=[]; 
  personalAnomalias :any[]=[]; 
  tiposAnomalias :any[]=[]; 

  constructor(private http:HttpClient) { }  


  get_proyectos():any{
    if (this.proyectos.length > 0) {
      return of( this.proyectos )
    }else{
      return this.http.get( this.URL + 'tblproyecto')
                 .pipe(map((res:any)=>{ 
                       this.proyectos = res;
                       return res;
                  }))
    }
  }

  get_estadosInspecciones():any{
    if (this.estadosInspecciones.length > 0) {
      return of( this.estadosInspecciones )
    }else{
      return this.http.get( this.URL + 'tblEstados')
                 .pipe(map((res:any)=>{ 
                       this.estadosInspecciones = res;
                       return res;
                  }))
    }
  }

  get_nivelesInspecciones():any{
    if (this.nivelesInspecciones.length > 0) {
      return of( this.nivelesInspecciones )
    }else{
      return this.http.get( this.URL + 'tblNivelInspeccion')
                 .pipe(map((res:any)=>{ 
                       this.nivelesInspecciones = res;
                       return res;
                  }))
    }
  }

  get_personalesBandejaAtencion():any{
    if (this.personales.length > 0) {
      return of( this.personales )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '24');
      parametros = parametros.append('filtro', '');

      return this.http.get( this.URL + 'TblPersonal', {params: parametros})
                 .pipe(map((res:any)=>{ 
                       this.personales = res.data;
                       return res.data;
                  }))
    }
  }

  get_clientes():any{
    if (this.clientes.length > 0) {
      return of( this.clientes )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '11');
      parametros = parametros.append('filtro', '0');

      return this.http.get( this.URL + 'BandejaAtencion', {params: parametros})
                 .pipe(map((res:any)=>{ 
                       this.clientes = res;
                       return res;
                  }))
    }
  }

  get_empresasContratistas():any{
    if (this.empresasContratistas.length > 0) {
      return of( this.empresasContratistas )
    }else{
      return this.http.get( this.URL + 'tblEmpresaColaboradora')
                 .pipe(map((res:any)=>{ 
                       this.empresasContratistas = res;
                       return res;
                  }))
    }
  }


  get_cargos():any{
    if (this.cargos.length > 0) {
      return of( this.cargos )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '12');
      parametros = parametros.append('filtro', '');

      return this.http.get( this.URL + 'BandejaAtencion',  {params: parametros})
                 .pipe(map((res:any)=>{ 
                       this.cargos = res;
                       return res;
                  }))
    }
  }
 
  get_areas():any{
    if (this.areas.length > 0) {
      return of( this.areas )
    }else{
      return this.http.get( this.URL + 'tblAreas')
                 .pipe(map((res:any)=>{ 
                       this.areas = res;
                       return res;
                  }))
    }
  }

  get_tecnicos(idCargo:number, idUsuario:number): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '17');
    parametros = parametros.append('filtro', String(idCargo) + '|' + idUsuario);

    return this.http.get( this.URL + 'BandejaAtencion' , {params: parametros});
  }

  get_tiposInspecciones(): any{
    // return this.http.get( this.URL + 'tblTipo_Inspeccion');

    if (this.tiposInspecciones.length > 0) {
      return of( this.tiposInspecciones )
    }else{
      return this.http.get( this.URL + 'tblTipo_Inspeccion')
                 .pipe(map((res:any)=>{ 
                       this.tiposInspecciones = res;
                       return res;
                  }))
    }
  }

  get_tiposSanciones(): any{
    return this.http.get( this.URL + 'tblTipo_Sancion');
  }

  get_tiposFormatos(): any{
    if (this.tiposFormatos.length > 0) {
      return of( this.tiposFormatos )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '10');
      parametros = parametros.append('filtro', '');

      return this.http.get( this.URL + 'BandejaAtencion', {params: parametros})
                 .pipe(map((res:any)=>{ 
                       this.tiposFormatos = res;
                       return res;
                  }))
    }
  }
  

  get_personalAnomalias(): any{
    if (this.personalAnomalias.length > 0) {
      return of( this.personalAnomalias )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '6');
      parametros = parametros.append('filtro', '');

      return this.http.get( this.URL + 'BandejaAtencion', {params: parametros})
                 .pipe(map((res:any)=>{ 
                       this.personalAnomalias = res;
                       return res;
                  }))
    }
  }

  get_tiposAnomalias_tipoFormato(idFormato:number, idUsuario:number): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '1');
    parametros = parametros.append('filtro', String(idFormato));

    return this.http.get( this.URL + 'tblAnomalia' , {params: parametros});
  }
  

  get_mostrar_informacion(obj:any):any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '1');
    parametros = parametros.append('filtro', obj.id_Proyecto + '|' + obj.estado_inspeccion + '|' + obj.nivelInspeccion + '|' + obj.inspector + '|' + obj.fecha_ini + '|' + obj.fecha_fin);

    return this.http.get( this.URL + 'BandejaAtencion', {params: parametros});
  }
 
  get_verificar_nombreTurno(descTurno:string){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '1');
    parametros = parametros.append('filtro', descTurno);

    return this.http.get( this.URL + 'Mantenimientos' , {params: parametros}).toPromise();
  }

  set_save_bandejaAtencion(objMantenimiento:any):any{
    return this.http.post(this.URL + 'tblInspeccion_Cab', JSON.stringify(objMantenimiento), httpOptions);
  }

  set_editar_bandejaAtencion(objMantenimiento:any, id :number):any{
    return this.http.put(this.URL + 'tblInspeccion_Cab/' + id , JSON.stringify(objMantenimiento), httpOptions);
  }

  set_anular_tipoSancion(id : number):any{ 
    return this.http.delete(this.URL + 'tblTipo_Sancion/' + id, httpOptions);
  }


  set_save_anomaliasDet(objMantenimiento:any):any{
    return this.http.post(this.URL + 'tblInspeccion_Cab_Detalle', JSON.stringify(objMantenimiento), httpOptions);
  }

  set_editar_anomaliasDet(objMantenimiento:any, id :number):any{
    return this.http.put(this.URL + 'tblInspeccion_Cab_Detalle/' + id , JSON.stringify(objMantenimiento), httpOptions);
  }

  set_NroSancionados(id_inspeccion:number):any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '13');
    parametros = parametros.append('filtro', id_inspeccion);

    return this.http.get( this.URL + 'BandejaAtencion', {params: parametros});
  }

  get_anomaliasDet(id_inspeccion:number):any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '4');
    parametros = parametros.append('filtro', id_inspeccion);

    return this.http.get( this.URL + 'BandejaAtencion', {params: parametros});
  }

  upload_imagen_anomalia(file:any, idUsuario:number, idusuarioLogin : any) :any{   
    const formData = new FormData();   
    formData.append('file', file);
    return this.http.post(this.URL + 'BandejaAtencion', formData);    
  }
 
  save_inspeccciones_detalle_Foto(objProceso:any): any{
    return this.http.post(this.URL + 'tblInspeccion_Cab_Detalle_Foto', JSON.stringify(objProceso), httpOptions);
  }

  get_fotosAnomalias(id_inspeccion_detalle:number):any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '5');
    parametros = parametros.append('filtro', id_inspeccion_detalle);

    return this.http.get( this.URL + 'BandejaAtencion', {params: parametros});
  }

  save_inspeccciones_detalle_AnomaliaFoto(obj:any): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '7');
    parametros = parametros.append('filtro',  obj.id_inspeccion_detalle + '|' + obj.levantamiento + '|' + obj.foto_levantamiento + '|' + obj.descripcion_levantamiento);

    return this.http.get( this.URL + 'BandejaAtencion', {params: parametros});
  }

  get_personal_email(idUsuario:number,  id_Personal_JefeObra:number, id_Personal_Responsable : number):any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '8');
    parametros = parametros.append('filtro', idUsuario + '|' + id_Personal_JefeObra + '|' + id_Personal_Responsable );

    return this.http.get( this.URL + 'BandejaAtencion', {params: parametros});
  }

  get_enviandoCorreo(id_inspeccionCab :number, nombrePdf : string ,  destinatario: string, copia: string , asunto:string, mensaje:string, tipoFormato:number):any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '9');
    parametros = parametros.append('filtro', id_inspeccionCab + '|' + nombrePdf + '|' + destinatario + '|' + copia + '|' + asunto + '|' + mensaje + '|' + tipoFormato );

    return this.http.get( this.URL + 'BandejaAtencion', {params: parametros});
  }

}
