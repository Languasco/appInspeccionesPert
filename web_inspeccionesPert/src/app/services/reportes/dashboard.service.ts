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
 
export class DashboardService {

  URL = environment.URL_API;
  constructor(private http:HttpClient) { }  

  get_mostrar_dashboard(obj:any,  id_personal:number, id_detallado:number , tipoReporte:number ): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '1');
    parametros = parametros.append('filtro', obj.id_pais + '|' + obj.id_grupo + '|' + obj.id_Delegacion + '|' + obj.fecha_ini + '|' + obj.fecha_fin + '|' + id_personal + '|' + id_detallado + '|' + tipoReporte);

    return this.http.get( this.URL + 'Reporte_Dashboard' , {params: parametros});
  }

 

 

}
