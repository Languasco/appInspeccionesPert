import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const HttpUploadOptions = {
  headers: new HttpHeaders({ "Content-Type": "multipart/form-data" })
}

@Injectable({
  providedIn: 'root'
})
 
export class ReporteInspeccionesService {

  URL = environment.URL_API;
  constructor(private http:HttpClient) { }  

  get_mostrar_informacion(obj:any): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '1');
    parametros = parametros.append('filtro', obj.id_pais + '|' + obj.id_grupo + '|' + obj.id_Delegacion + '|' + obj.fecha_ini + '|' + obj.fecha_fin );

    return this.http.get( this.URL + 'ReporteListado_Inspecciones' , {params: parametros});
  }

  
  set_DescargarInspecciones(obj:any){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '2');
    parametros = parametros.append('filtro', obj.id_pais + '|' + obj.id_grupo + '|' + obj.id_Delegacion + '|' + obj.fecha_ini + '|' + obj.fecha_fin );

    return this.http.get( this.URL + 'ReporteListado_Inspecciones' , {params: parametros});
  } 

  set_DescargarInspeccionesNew(obj:any, id_usuario:number){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '3');
    parametros = parametros.append('filtro', obj.id_pais + '|' + obj.id_grupo + '|' + obj.id_Delegacion + '|' + obj.fecha_ini + '|' + obj.fecha_fin + '|' + id_usuario );

    return this.http.get( this.URL + 'ReporteListado_Inspecciones' , {params: parametros});
  } 
 

}

