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
 

export class AnomaliasService {

  URL = environment.URL_API;
  paises:any[]=[]; 
  tiposFormatos:any[]=[]; 
  delegaciones:any[]=[]; 


  constructor(private http:HttpClient) { }  

  get_paises(id_usuario:number):any{
    if (this.paises.length > 0) {
      return of( this.paises )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '1');
      parametros = parametros.append('filtro', String(id_usuario));

      return this.http.get( this.URL + 'tblpais',  {params: parametros})
                 .pipe(map((res:any)=>{ 
                       this.paises = res;
                       return res;
                  }))
    }
  }

  get_tiposFormatos():any{
    if (this.tiposFormatos.length > 0) {
      return of( this.tiposFormatos )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '10');
      parametros = parametros.append('filtro', '');

      return this.http.get( this.URL + 'BandejaAtencion',  {params: parametros})
                 .pipe(map((res:any)=>{ 
                       this.tiposFormatos = res;
                       return res;
                  }))
    }
  }

  get_sociedadesPais(idPais:number, idUsuario:number): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '3');
    parametros = parametros.append('filtro', String(idPais) + '|' + idUsuario);

    return this.http.get( this.URL + 'tblgrupo' , {params: parametros});
  }

  get_delegacionesSociedades(idGrupo:number, idUsuario:number): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '3');
    parametros = parametros.append('filtro', String(idGrupo) + '|' + idUsuario);

    return this.http.get( this.URL + 'tblDelegacion' , {params: parametros});
  }


  get_mostrar_informacion({  id_pais, id_grupo, id_formato}): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '2');
    parametros = parametros.append('filtro', String(id_pais)  + '|' +id_grupo + '|' +id_formato  );

    return this.http.get( this.URL + 'tblAnomalia' , {params: parametros});
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

