import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { combineLatest } from 'rxjs'; 
import { NgxSpinnerService } from 'ngx-spinner';
  
import Swal from 'sweetalert2';
import { AlertasService } from 'src/app/services/alertas/alertas.service';
import { LoginService } from 'src/app/services/login/login.service';
import { FuncionesglobalesService } from 'src/app/services/funciones/funcionesglobales.service';
import { AnomaliasService } from 'src/app/services/mantenimientos/anomalias.service';
import { RespuestaServer } from 'src/app/models/respuestaServer.models';
 

declare const $:any;

@Component({
  selector: 'app-anomalias',
  templateUrl: './anomalias.component.html',
  styleUrls: ['./anomalias.component.css']
})
 
export class AnomaliasComponent implements OnInit {


  formParamsFiltro : FormGroup;
  formParams : FormGroup;

  idUserGlobal = 0;
  filtrarMantenimiento= '';
  anomalias:any[] =[];

  paises:any[]=[]; 
  sociedades:any[] =[];
  tiposFormatos:any[] =[];
 
  flag_modoEdicion :boolean = false;
 

  constructor( private spinner: NgxSpinnerService, private alertasService : AlertasService, private loginService: LoginService, private funcionGlobalServices : FuncionesglobalesService,
    private anomaliasService : AnomaliasService ) { 
    this.idUserGlobal = this.loginService.get_idUsuario();
  }

  ngOnInit(): void {
    this.getCargarCombos();
    this.inicializarFormularioFiltro();    
    this.inicializarFormulario(); 
  }
  
  inicializarFormularioFiltro(){ 
    this.formParamsFiltro = new FormGroup({ 
      id_pais: new FormControl('0'),
      id_grupo:new FormControl('0'),
      id_formato: new FormControl('0'),
     }) 
  }

  inicializarFormulario(){ 
    this.formParams = new FormGroup({    
      id_pais:  new FormControl('0'),
      id_grupo:  new FormControl('0'),
      id_Empresa:  new FormControl('0'),

      isConforme:  new FormControl('2'),
      isPersonalNoRegistrado  : new FormControl('2'),

      id_Anomalia:  new FormControl('0'),
      id_formato:  new FormControl('0'),
      codigo_Anomalia:  new FormControl(''),
      descripcion_Anomalia:  new FormControl(''),
      anomalia_Critica:  new FormControl('0'),
      anomalia_Critica_General:  new FormControl('0'),
      anomalia_titulo:  new FormControl(''),
      anomalia_Grupo:  new FormControl(''),
      Descripcion_Anomalia_Espana:  new FormControl(''),
      anomalia_orden:  new FormControl(''),
      anomalia_Valor:  new FormControl(''),
      ver_Validacion:  new FormControl('0'),
      estado: new FormControl('1'),
      usuario_creacion:  new FormControl(this.idUserGlobal),
      flag_personal_nuevo: new FormControl('0'),
     }) 
  }

  getCargarCombos(){ 
    this.spinner.show(); 
    combineLatest([ this.anomaliasService.get_paises(this.idUserGlobal),  this.anomaliasService.get_tiposFormatos()])
       .subscribe(([ _paises, _tiposFormatos] :any )=>{  
        this.spinner.hide(); 
          this.paises = _paises;     
          this.tiposFormatos = _tiposFormatos; 
      }, error => {
        this.spinner.hide(); 
        alert(JSON.stringify(error));
      },)


  } 
  cerrarModal_registro(){
    setTimeout(()=>{ // 
      $('#modal_registro').modal('hide');  
    },0); 
  }
 
   mostrarInformacion(){ 

    if (this.formParamsFiltro.value.id_pais == 0 || this.formParamsFiltro.value.id_pais == '0')  {
      this.alertasService.Swal_alert('error', 'Por favor seleccione el Pais.');
      return
    }
    if (this.formParamsFiltro.value.id_grupo == 0 || this.formParamsFiltro.value.id_grupo == '0')  {
      this.alertasService.Swal_alert('error', 'Por favor seleccione la Sociedad.');
      return
    }
    if (this.formParamsFiltro.value.id_formato == 0 || this.formParamsFiltro.value.id_formato == '0')  {
      this.alertasService.Swal_alert('error', 'Por favor seleccione el Formato.');
      return
    }

    this.spinner.show();
    this.anomaliasService.get_mostrar_informacion(this.formParamsFiltro.value)
    .subscribe((data:any)=>{  
      this.spinner.hide();  
      this.anomalias= data;
    }, error => {
      this.spinner.hide();
      alert(JSON.stringify(error));
    },)
  }
 
  descargarArchivoSeleccionado(idProductoArchivo:number){    
    // Swal.fire({
    //   icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'
    // })
    // Swal.showLoading();
    // this.anomaliasService.get_descargarFileProducto(idProductoArchivo, this.idUserGlobal).subscribe((res:RespuestaServer)=>{
    //   Swal.close();
    //   console.log(res);
    //   if (res.ok) { 
    //     window.open(String(res.data),'_blank');
    //   }else{
    //     this.alertasService.Swal_alert('error', JSON.stringify(res.data));
    //     alert(JSON.stringify(res.data));
    //   }
    // })
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
  if (this.formParams.value.id_pais == '0' || this.formParams.value.id_pais == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione el Pais');
    return 
  } 
  
  if (this.formParams.value.id_grupo == '0' || this.formParams.value.id_grupo == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione el Grupo');
    return 
  } 
  
  if (this.formParams.value.id_formato == '0' || this.formParams.value.id_formato == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione el Formato');
    return 
  } 

  // REGISTRO DE CONFORMIDAD 2
  if (this.formParams.value.isConforme == '2' || this.formParams.value.id_formato == 2) {      
    if (this.formParams.value.codigo_Anomalia == '' || this.formParams.value.codigo_Anomalia == null) {
      this.alertasService.Swal_alert('error','Por favor ingrese el código de la Anomalia');
      return 
    } 
    if (this.formParams.value.descripcion_Anomalia == '' || this.formParams.value.descripcion_Anomalia == null) {
      this.alertasService.Swal_alert('error','Por favor ingrese la Descripción de la Anomalia.');
      return 
    } 
    if (this.formParams.value.anomalia_Critica == '' || this.formParams.value.anomalia_Critica == null) {
      this.alertasService.Swal_alert('error','Por favor indique si posee Anomalia Critica.');
      return 
    } 
    if (this.formParams.value.anomalia_Critica_General == '' || this.formParams.value.anomalia_Critica_General == null) {
      this.alertasService.Swal_alert('error','Por favor indique si posee  Anomalia Critica General');
      return 
    } 
  } 

  if (this.formParams.value.flag_personal_nuevo == '2' || this.formParams.value.flag_personal_nuevo == 2 ){
    this.formParams.patchValue({"id_formato": 3, "codigo_Anomalia" :  '0' , "descripcion_Anomalia" :  '' , "anomalia_titulo" :  '0' , "anomalia_orden" :  '40' 
                                , "anomalia_Valor" :  '0' , "anomalia_Grupo" :  '0' , "Descripcion_Anomalia_Espana" :  '' });
  }

  this.formParams.patchValue({ "usuario_creacion" : this.idUserGlobal });
 
  if ( this.flag_modoEdicion==false) { //// nuevo  

     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
     Swal.showLoading();

     const codigoAnom = await this.anomaliasService.get_verificar_codigoAnomalia(this.formParams.value.codigo_Anomalia);
     if (codigoAnom) {
      Swal.close();
      this.alertasService.Swal_alert('error','El codigo de anomalia ya existe, verifique..');
      return;
     }   
    this.anomaliasService.save_anomalia(this.formParams.value)
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
    this.anomaliasService.set_edit_anomalia(this.formParams.value , this.formParams.value.id_Anomalia)
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


 anular(objBD:any){

   if (objBD.estado ===0 || objBD.estado =='0') {      
     return;      
   }

   this.alertasService.Swal_Question('Sistemas', 'Esta seguro de anular ?')
   .then((result)=>{
     if(result.value){

       Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
       Swal.showLoading();
       this.anomaliasService.set_anular_anomalia(objBD.id_Anomalia).subscribe((res:any)=>{
         Swal.close();        
         if (res = 'ok') {            
           for (const item of this.anomalias) {
             if (item.id_Anomalia == objBD.id_Anomalia ) {
                item.estado = 0;
                 break;
             }
           }
           this.alertasService.Swal_Success('Se anulo correctamente..')  

         }else{
           this.alertasService.Swal_alert('error', JSON.stringify(res.data));
           alert(JSON.stringify(res.data));
         }
       })
        
     }
   }) 

 } 
 
 changePais(opcion:any){
    if ( opcion.target.value == 0 || opcion.target.value == '0' ) {
      this.sociedades = [];
      this.formParamsFiltro.patchValue({"id_grupo": '0'});
      return;
    }     
    this.mostrarSociedadesPais(opcion.target.value );
  }

  mostrarSociedadesPais(idPais:number){ 
    this.spinner.show();
    this.anomaliasService.get_sociedadesPais(idPais, this.idUserGlobal)
      .subscribe((data:any)=>{  
        this.spinner.hide();  
        this.sociedades= data;
      }, error => {
        this.spinner.hide();
        alert(JSON.stringify(error));
      },)
  }

  changeSociedad(opcion:any){
    if ( opcion.target.value == 0 || opcion.target.value == '0' ) {
      this.tiposFormatos = [];
      this.formParamsFiltro.patchValue({"id_formato": '0'});
      return;
    }     
  }

  keyPress(event: any) {
    this.funcionGlobalServices.verificar_soloNumeros(event)  ;
  }

  registroConformidad(valor:any){
    if (valor === 2 || valor === '2') {
      this.formParams.patchValue({"flag_personal_nuevo": '2'});
    } else {
        this.formParams.patchValue({"flag_personal_nuevo": '0'});
    }
  }

  registroPersonal(valor:any){
    console.log(valor);
    this.formParams.patchValue({"flag_personal_nuevo": valor});
  }

  editar(obj:any){ 
 
    this.flag_modoEdicion=true;
 
    this.spinner.show();
    this.anomaliasService.get_sociedadesPais(obj.id_pais , this.idUserGlobal)
      .subscribe((data:any)=>{  
        this.spinner.hide();  
        this.sociedades= data;

        this.formParams.patchValue({ 
          "id_pais": obj.id_pais ,"id_grupo": obj.id_grupo,"id_Empresa": obj.id_Empresa,"id_Anomalia":obj.id_Anomalia,"id_formato": obj.id_formato,"nombre_formato":obj.nombre_formato,
          "codigo_Anomalia":obj.codigo_Anomalia,"descripcion_Anomalia":obj.descripcion_Anomalia, "anomalia_Critica":obj.anomalia_Critica,"anomalia_Critica_General": obj.anomalia_Critica_General,
          "anomalia_titulo": obj.anomalia_titulo, "anomalia_orden":obj.anomalia_orden,"id_ValorInspeccion":obj.id_ValorInspeccion,"estado":obj.estado,"Descripcion_Anomalia_Espana":obj.Descripcion_Anomalia_Espana,
          "anomalia_Valor": obj.anomalia_Valor,"anomalia_Grupo": obj.anomalia_Grupo,"ver_Validacion": obj.ver_Validacion, "flag_personal_nuevo": obj.flag_personal_nuevo 
         });

         if (obj.flag_personal_nuevo === 1 || obj.flag_personal_nuevo === '1') {
          this.formParams.patchValue({ "isPersonalNoRegistrado": '1'  });
        } else if (obj.flag_personal_nuevo === 2 || obj.flag_personal_nuevo === '2') {
          this.formParams.patchValue({ "isPersonalNoRegistrado": '2'  }); 
        } else {
            this.formParams.patchValue({ "isConforme": '2'  });
        }

        setTimeout(()=>{ // 
          this.formParams.patchValue({ "id_grupo": obj.id_grupo });
         $('#modal_mantenimiento').modal('show');  
       },0); 

      }, error => {
        this.spinner.hide();
        alert(JSON.stringify(error));
      },)
  } 


}

