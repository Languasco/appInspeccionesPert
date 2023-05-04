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
 
export class PersonalService {

  URL = environment.URL_API;
  constructor(private http:HttpClient) { }  

  get_mostrar_informacion({  id_pais, id_grupo, id_Delegacion, estado}): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '23');
    parametros = parametros.append('filtro', String(id_pais)  + '|' +id_grupo + '|' +id_Delegacion + '|' +estado );

    return this.http.get( this.URL + 'tblPersonal' , {params: parametros});
  }

  save_anomalia(objMantenimiento:any):any{
    return this.http.post(this.URL + 'tblAnomalia', JSON.stringify(objMantenimiento), httpOptions);
  }

  set_edit_anomalia(objMantenimiento:any, id :number):any{
    return this.http.put(this.URL + 'tblAnomalia/' + id , JSON.stringify(objMantenimiento), httpOptions);
  }

  get_verificar_codigoAnomalia(codAnomalia:string){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '5');
    parametros = parametros.append('filtro', codAnomalia);

    return this.http.get( this.URL + 'tblAnomalia' , {params: parametros}).toPromise();
  }

 
  set_anular_anomalia(id : number):any{ 
    return this.http.delete(this.URL + 'tblAnomalia/' + id , httpOptions);
  }

}

