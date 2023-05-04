import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';

import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const HttpUploadOptions = {
  headers: new HttpHeaders({ "Content-Type": "multipart/form-data" })
}

@Injectable({
  providedIn: 'root'
})
 
export class ListadoInspeccionesService {

  URL = environment.URL_API;
  paises:any[]=[]; 
  tiposFormatos:any[]=[]; 

  constructor(private http:HttpClient) { }  
 
  get_mostrar_informacion(id_personal : number, obj:any , idDelegacion :string, idInspector :string, idRespCorreccion:string , opcion: number, fechaIni :string, fechaFin:string): any{
    const params = {
      id_personal: id_personal,
      id_pais: obj.id_pais,
      id_grupo: obj.id_grupo,
      idDelegacion: String(idDelegacion),
      idInspector: String(idInspector),
      idRespCorreccion: String(idRespCorreccion),
      opcion: opcion,
      fecha_Ini: fechaIni ,
      fecha_fin: fechaFin
  }

    return this.http.post(this.URL + 'BandejaAtencion/get_relacionInspecciones', JSON.stringify(params), httpOptions);
  }

  save_anomalia(objMantenimiento:any):any{
    return this.http.post(this.URL + 'tblAnomalia', JSON.stringify(objMantenimiento), httpOptions);
  }

  set_edit_anomalia(objMantenimiento:any, id :number):any{
    return this.http.put(this.URL + 'tblAnomalia/' + id , JSON.stringify(objMantenimiento), httpOptions);
  }

  get_Inspector_responsableCorreccion(id_pais:number, id_grupo:number, id_delegacion:string, idUser:number): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '5');
    parametros = parametros.append('filtro', String(id_pais) + '|' + id_grupo+ '|' + id_delegacion + '|' + idUser);

    return this.http.get( this.URL + 'tblPersonal' , {params: parametros});
  }

 

}

