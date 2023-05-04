import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AlertasService } from '../../../services/alertas/alertas.service';
import { FuncionesglobalesService } from '../../../services/funciones/funcionesglobales.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoginService } from '../../../services/login/login.service';
import Swal from 'sweetalert2'; 
import { TiposSancionService } from 'src/app/services/mantenimientos/tipos-sancion.service';
declare var $:any;

@Component({
  selector: 'app-tipo-sancion',
  templateUrl: './tipo-sancion.component.html',
  styleUrls: ['./tipo-sancion.component.css']
})
export class TipoSancionComponent implements OnInit {

  formParamsFiltro : FormGroup;
  formParams: FormGroup;

  idUserGlobal :number = 0;
  flag_modoEdicion :boolean =false;

  tiposSancion :any[]=[]; 
  filtrarMantenimiento = "";
 
  constructor(private alertasService : AlertasService, private spinner: NgxSpinnerService, private loginService: LoginService,
              private funcionGlobalServices : FuncionesglobalesService, private tiposSancionService : TiposSancionService  ) {         
    this.idUserGlobal = this.loginService.get_idUsuario();
  }
 
 ngOnInit(): void {
   this.mostrarInformacion();
   this.inicializarFormulario(); 
 }


 inicializarFormulario(){ 
    this.formParams= new FormGroup({
      id_TipoSancion: new FormControl('0'), 
      descripcion_TipoSancion: new FormControl(''), 
      estado : new FormControl('1'),   
      usuario_creacion : new FormControl('')
    }) 
 }

 mostrarInformacion(){ 
      this.spinner.show();
      this.tiposSancionService.get_mostrar_informacion()
          .subscribe((res:any)=>{  
              this.spinner.hide();      
              this.tiposSancion = res;        
            }, error => {
              this.spinner.hide();
              alert(JSON.stringify(error));
            },)
 }   
  
 cerrarModal(){
    setTimeout(()=>{ // 
      $('#modal_mantenimiento').modal('hide');  
    },0); 
 }

 nuevo(){
    this.flag_modoEdicion = false;
    this.inicializarFormulario();  
    setTimeout(()=>{ // 
      $('#modal_mantenimiento').modal('show');  
    },0); 
 } 

 async saveUpdate(){ 

  if (this.formParams.value.descripcion_TipoSancion == '' || this.formParams.value.descripcion_TipoSancion == null) {
    this.alertasService.Swal_alert('error','Por favor ingrese la descripcion');
    return 
  } 
   this.formParams.patchValue({ "usuario_creacion" : this.idUserGlobal });
 
  if ( this.flag_modoEdicion==false) { //// nuevo  

    const listado = this.get_verificarCodigo(this.formParams.value.descripcion_TipoSancion.trim());
    if (listado) {
      this.alertasService.Swal_alert('error','EL Tipo de Sancion ya se encuentra Registrada, verifique..');
      return;
    }

     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
     Swal.showLoading(); 
    this.tiposSancionService.set_save_tipoSancion(this.formParams.value)
      .subscribe((res:any)=>{  
        Swal.close();    
         this.alertasService.Swal_Success('Se agrego correctamente..'); 
         this.flag_modoEdicion = true;
         this.mostrarInformacion();
         this.cerrarModal();      
      }, error => {
        Swal.close();
        alert(JSON.stringify(error));
      },)
     
   }else{ /// editar

     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Actualizando, espere por favor'  })
     Swal.showLoading();    
    this.tiposSancionService.set_edit_tipoSancion(this.formParams.value , this.formParams.value.id_TipoSancion)
      .subscribe((res:any)=>{  
        Swal.close();    
         this.mostrarInformacion();
         this.alertasService.Swal_Success('Se actualizó correctamente..');  
         this.cerrarModal();      
      }, error => {
        Swal.close();
        alert(JSON.stringify(error));
      },)

   }

 } 

 get_verificarCodigo(descripcion:string){ 
  const listado = this.tiposSancion.find(u=> u.descripcion_TipoSancion.toUpperCase() === descripcion.toUpperCase());
  return listado;
}

 editar({id_TipoSancion, descripcion_TipoSancion , estado  }){
   this.flag_modoEdicion=true;
   this.formParams.patchValue({ "id_TipoSancion" : id_TipoSancion,"descripcion_TipoSancion" : descripcion_TipoSancion, "estado" : estado 
  });
   setTimeout(()=>{ // 
    $('#modal_mantenimiento').modal('show');  
  },0);  
 } 

 anular(objBD:any){

   if (objBD.estado ===0 || objBD.estado =='0') {      
     return;      
   }

   this.alertasService.Swal_Question('Sistemas', 'Esta seguro de anular ?')
   .then((result)=>{
     if(result.value){


      Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Anulando, espere por favor'  })
      Swal.showLoading();    
     this.tiposSancionService.set_anular_tipoSancion(objBD.id_TipoSancion)
       .subscribe((res:any)=>{  
         Swal.close();    
           for (const item of this.tiposSancion) {
             if (item.id_TipoSancion == objBD.id_TipoSancion ) {
                 item.estado = 0;
                 break;
             }
           }
           this.alertasService.Swal_Success('Se anulo correctamente..')     
       }, error => {
         Swal.close();
         alert(JSON.stringify(error));
       },)
        
     }
   }) 

 }


}

