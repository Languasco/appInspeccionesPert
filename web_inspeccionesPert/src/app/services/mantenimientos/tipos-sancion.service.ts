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
export class TiposSancionService {

  URL = environment.URL_API;
  constructor(private http:HttpClient) { }  

  get_mostrar_informacion(): any{
    return this.http.get( this.URL + 'tblTipo_Sancion');
  }
 
  get_verificar_nombreTurno(descTurno:string){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '1');
    parametros = parametros.append('filtro', descTurno);

    return this.http.get( this.URL + 'Mantenimientos' , {params: parametros}).toPromise();
  }

  set_save_tipoSancion(objMantenimiento:any):any{
    return this.http.post(this.URL + 'tblTipo_Sancion', JSON.stringify(objMantenimiento), httpOptions);
  }

  set_edit_tipoSancion(objMantenimiento:any, id :number):any{
    return this.http.put(this.URL + 'tblTipo_Sancion/' + id , JSON.stringify(objMantenimiento), httpOptions);
  }

  set_anular_tipoSancion(id : number):any{ 
    return this.http.delete(this.URL + 'tblTipo_Sancion/' + id, httpOptions);
  }

}

