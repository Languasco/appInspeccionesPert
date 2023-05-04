import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AlertasService } from '../../../services/alertas/alertas.service';
import { FuncionesglobalesService } from '../../../services/funciones/funcionesglobales.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoginService } from '../../../services/login/login.service';
import Swal from 'sweetalert2'; 
import { BandejaAtencionInspeccionService } from '../../../services/procesos/bandeja-atencion-inspeccion.service';
import { combineLatest } from 'rxjs';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap';
import { InputFileI } from 'src/app/models/inputFile.models';
 
declare var $:any;

@Component({
  selector: 'app-bandeja-atencion-inspecciones',
  templateUrl: './bandeja-atencion-inspecciones.component.html',
  styleUrls: ['./bandeja-atencion-inspecciones.component.css']
})
export class BandejaAtencionInspeccionesComponent implements OnInit {

  formParamsFiltro : FormGroup;
  formParams: FormGroup;
  formParamsAnomalia: FormGroup;
  formParamsFile : FormGroup;

  idUserGlobal :number = 0;
  flag_modoEdicion :boolean =false;
  flag_modoEdicion_anomalias:boolean =false;
  datepiekerConfig:Partial<BsDatepickerConfig>

  proyectos :any[]=[]; 
  estadosInspeccion :any[]=[]; 
  niveles :any[]=[]; 
  inspectores :any[]=[]; 

  coordinadores:any[]=[]; 
  jefesObras:any[]=[]; 
  responsablesCorrecciones:any[]=[]; 
  clientes :any[]=[]; 
  empresasContratistas :any[]=[]; 
  cargos :any[]=[]; 
  tecnicos :any[]=[]; 
  areas :any[]=[]; 
  tiposInspecciones :any[]=[]; 
  tiposSanciones :any[]=[]; 

  bandejaAtencionCab :any[]=[]; 
  filtrarMantenimiento = "";  
  TabActivo_Global=0;
  id_inspeccion_Global = 0;
  id_inspeccion_detalle_Global= 0;
 
  tiposFormatos :any[]=[]; 
  personalAnomalias :any[]=[]; 
  tiposAnomalias :any[]=[]; 
 
  anomaliasInspeccionesDet :any[]=[]; 
  imgProducto= './assets/img/sinImagen.jpg';
  filePhoto:InputFileI[] = []; 
  fotoSeleccionado :any[]=[]; 


  @ViewChild('staticTabsPrincipal', { static: false }) staticTabsPrincipal: TabsetComponent;

  constructor(private alertasService : AlertasService, private spinner: NgxSpinnerService, private loginService: LoginService,
              private funcionGlobalServices : FuncionesglobalesService, private bandejaAtencionInspeccionService :BandejaAtencionInspeccionService ) {         
    this.idUserGlobal = this.loginService.get_idUsuario();
    this.datepiekerConfig = Object.assign({}, { containerClass : 'theme-red',  dateInputFormat: 'DD/MM/YYYY' })
  }
 
 ngOnInit(): void {
  //  this.mostrarInformacion();
   this.inicializarFormularioFiltro(); 
   this.inicializarFormulario(); 
   this.inicializarFormulario_anomalias(); 
   this.inicializarFile();
   this.getCargarCombos();
 }

 inicializarFormularioFiltro(){ 
  this.formParamsFiltro = new FormGroup({ 
    id_Proyecto: new FormControl('0'),
    estado_inspeccion:new FormControl('0'),
    nivelInspeccion: new FormControl('0'),
    inspector: new FormControl('0'),
    fecha_ini: new FormControl(new Date()),
    fecha_fin: new FormControl(new Date()),
   }) 
}

 inicializarFormulario(){ 
    this.formParams= new FormGroup({
        id_Inspeccion: new FormControl('0'), 
        id_EmpresaColaboradora: new FormControl('0'), 
        id_Cliente: new FormControl('0'), 
        lugar_Inspeccion  :new FormControl(''), 
        actividadOT_Inspeccion :new FormControl(''), 
        trabajoArealizar_Inspeccion :new FormControl(''), 
        id_Cargo :new FormControl('0'), 
        id_Personal_Inspeccionado :new FormControl('0'), 
        id_Area :new FormControl('0'), 
        id_Personal_Coordinador :new FormControl('0'), 
        id_Personal_JefeObra :new FormControl('0'), 
        placa_Inspeccion :new FormControl(''), 
        id_NivelInspeccion :new FormControl('0'), 
        id_TipoInspeccion :new FormControl('0'), 
        Resultado_Inspeccion :new FormControl('0'), 
        iniciofin_Trabajo :new FormControl('0'), 
        accionPropuesta_Correctiva :new FormControl(''), 
        id_Personal_Responsable :new FormControl('0'), 
        fechaPropuesta_Correctiva :new FormControl(''), 
        observacion_Correctiva :new FormControl(''), 
        paralizacion_Correctiva :new FormControl('0'), 
        sancion_Correctiva :new FormControl('0'), 
        id_TipoSancion :new FormControl('0'), 
        nroTrabajadores_Correctiva: new FormControl(''), 
        Obs_Levantada :new FormControl(''), 
        nro_inspeccionRelacionada: new FormControl(''), 
        estado:new FormControl('0')
    }) 
 }

 inicializarFormulario_anomalias(){ 
  this.formParamsAnomalia = new FormGroup({ 
    id_Proyecto: new FormControl('0'), 

    id_inspeccion_detalle: new FormControl('0'),
    id_inspeccion: new FormControl('0'),
    id_personal: new FormControl('0'),
    id_anomalia: new FormControl('0'),
    descripcion: new FormControl(''),

    levantamiento: new FormControl('VACIO'),
    foto_levantamiento: new FormControl('VACIO'),
    descripcion_levantamiento: new FormControl('VACIO'),
    id_formato: new FormControl('0'),
    id_ValorInspeccion: new FormControl('NO'),
    estado:  new FormControl('1'),
    usuario_creacion:  new FormControl('0'),
    Resultado_Inspeccion : new FormControl('0'),
    accionPropuesta_Correctiva : new FormControl(''), 
    fechaPropuesta_Correctiva : new FormControl(null), 
   }) 
}

inicializarFile(){ 
  this.formParamsFile = new FormGroup({
    file : new FormControl(''),
    observacion: new FormControl(''),
    
    destinatario: new FormControl(''),
    copia: new FormControl(''),
    asunto: new FormControl(''),
    mensaje: new FormControl(''),
    tipoFormato: new FormControl('0'),

   })
}


 getCargarCombos(){ 
  this.spinner.show(); 
  combineLatest([ this.bandejaAtencionInspeccionService.get_proyectos() , this.bandejaAtencionInspeccionService.get_estadosInspecciones() , this.bandejaAtencionInspeccionService.get_nivelesInspecciones(), this.bandejaAtencionInspeccionService.get_personalesBandejaAtencion(),
    this.bandejaAtencionInspeccionService.get_clientes()  , this.bandejaAtencionInspeccionService.get_empresasContratistas(),  this.bandejaAtencionInspeccionService.get_cargos() , this.bandejaAtencionInspeccionService.get_areas() ,
    this.bandejaAtencionInspeccionService.get_tiposInspecciones() , this.bandejaAtencionInspeccionService.get_tiposSanciones(), this.bandejaAtencionInspeccionService.get_tiposFormatos(),
    this.bandejaAtencionInspeccionService.get_personalAnomalias()
  ])
     .subscribe(([ _proyectos, _estadosInspeccion, _niveles,_inspectores, _clientes, _empresasContratistas, _cargos, _areas, _tiposInspecciones, _tiposSanciones, _tiposFormatos,_personalAnomalias, _tiposAnomalias] :any )=>{  
      this.spinner.hide(); 
        this.proyectos = _proyectos;    
        this.estadosInspeccion = _estadosInspeccion.filter(e=> e.tipoproceso_estado == 'INSPECCION');
        this.niveles = _niveles;
  
        for (const item of _inspectores) {
          if (item.grupo == 'grupo1'  ){
            this.jefesObras.push(item);
            this.responsablesCorrecciones.push(item);
          }
          if (item.grupo == 'grupo2'  ){
            this.inspectores.push(item);
            this.coordinadores.push(item);
          }
        }

        this.clientes  = _clientes; 
        this.empresasContratistas = _empresasContratistas;
        this.cargos = _cargos;
        this.areas = _areas;

        this.tiposInspecciones = _tiposInspecciones;
        this.tiposSanciones = _tiposSanciones;
        this.tiposFormatos = _tiposFormatos;
        this.personalAnomalias = _personalAnomalias;

    }, error => {
      this.spinner.hide(); 
      alert(JSON.stringify(error));
    },)
} 

 mostrarInformacion(){ 

    if (this.formParamsFiltro.value.id_Proyecto == '' || this.formParamsFiltro.value.id_Proyecto == '0' || this.formParamsFiltro.value.id_Proyecto == 0) {
        this.alertasService.Swal_alert('error', 'Por favor seleccione el Proyecto');
        return;
    }  
    else if (this.formParamsFiltro.value.estado_inspeccion == '' || this.formParamsFiltro.value.estado_inspeccion == '0' || this.formParamsFiltro.value.estado_inspeccion == 0) {
        this.alertasService.Swal_alert('error', 'Por favor seleccione el Estado');
        return;
    }
    else if (this.formParamsFiltro.value.fecha_ini == '') {
        this.alertasService.Swal_alert('error', 'Por favor ingrese o seleccione la Fecha Inicial.');
        return;
    }
    else if (this.formParamsFiltro.value.fecha_fin == '') {
        this.alertasService.Swal_alert('error', 'Por favor ingrese o seleccione la Fecha Final.');
        return;
    }  

    const fechaIni = this.funcionGlobalServices.formatoFecha(this.formParamsFiltro.value.fecha_ini);
    const fechaFin = this.funcionGlobalServices.formatoFecha(this.formParamsFiltro.value.fecha_fin);

    this.spinner.show();
    this.bandejaAtencionInspeccionService.get_mostrar_informacion({...this.formParamsFiltro.value, fecha_ini :fechaIni, fecha_fin : fechaFin})
        .subscribe((data:any)=>{  
            this.spinner.hide();      
            this.bandejaAtencionCab = data;        
          }, error => {
            this.spinner.hide();
            alert(JSON.stringify(error));
          },)
 }   
  
 cerrarModal(){
    setTimeout(()=>{ // 
      $('#modal_proceso').modal('hide');  
    },0); 
 }

 nuevo(){
    this.flag_modoEdicion = false;
    this.flag_modoEdicion_anomalias = false;
    this.id_inspeccion_Global = 0;
    this.inicializarFormulario(); 
 
    this.blank_Detalle();
    this.anomaliasInspeccionesDet = [];
    
    setTimeout(()=>{ // 
      this.staticTabsPrincipal.tabs[0].active = true; 
      $('#modal_proceso').modal('show');  
    },0); 
 } 

 async saveUpdate(){ 
 
  if (this.formParams.value.id_Cliente == undefined || this.formParams.value.id_Cliente == null || this.formParams.value.id_Cliente == '0' || this.formParams.value.id_Cliente == 0) {
      this.alertasService.Swal_alert('error','Por favor ingrese o seleccione el Cliente');
      return ;
  }
  else if (this.formParams.value.id_EmpresaColaboradora == '0') {
      this.alertasService.Swal_alert('error','Es necesario seleccionar la Empresa Contratista.');
      return ;
  }
  else if (this.formParams.value.id_Cargo == '0') {
      this.alertasService.Swal_alert('error','Es necesario seleccionar el Cargo.');
      return ;
  }
  else if (this.formParams.value.id_Personal_Inspeccionado == '0') {
      this.alertasService.Swal_alert('error','Seleccione a quien se Inspeccionara');
      return ;;
  }
  else if (this.formParams.value.id_Area == '0') {
      this.alertasService.Swal_alert('error','Es necesario seleccionar el Area');
      return ;
  }  
  else if (this.formParams.value.id_Personal_Coordinador == '0') {
      this.alertasService.Swal_alert('error','Es necesario seleccionar el Coordinador');
      return ;
  }  
  else if (this.formParams.value.id_Personal_JefeObra == '0') {
      this.alertasService.Swal_alert('error','Es necesario seleccionar el Jefe de Obras');
      return ;
  }
  else if (this.formParams.value.id_NivelInspeccion == '0') {
    this.alertasService.Swal_alert('error','Es necesario seleccionar el Nivel de Inspeccion');
    return ;
  }
  else if (this.formParams.value.id_TipoInspeccion == '0') {
    this.alertasService.Swal_alert('error','Es necesario seleccionar el tipo de Inspeccion ');
    return ;
  }
  else if (this.formParams.value.id_Personal_Responsable == '0') {
    this.alertasService.Swal_alert('error','Es necesario seleccionar el responsable de Correcion');
    return ;
  }
  else if (this.formParams.value.id_TipoSancion == '0') {
    this.alertasService.Swal_alert('error','Es necesario seleccionar el tipo de Sancion');
    return ;
  }
  
   this.formParams.patchValue({ "nroTrabajadores_Correctiva" : (this.formParams.value.nroTrabajadores_Correctiva=='') ? '0' : this.formParams.value.nroTrabajadores_Correctiva ,  "Resultado_Inspeccion" : '', "accionPropuesta_Correctiva" : '', "fechaPropuesta_Correctiva" : new Date(),  "usuario_creacion" : this.idUserGlobal });
 
  if ( this.flag_modoEdicion==false) { //// nuevo  
 
     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
     Swal.showLoading(); 
    this.bandejaAtencionInspeccionService.set_save_bandejaAtencion(this.formParams.value)
      .subscribe((data:any)=>{  
        Swal.close();    
        this.flag_modoEdicion = true;
        this.id_inspeccion_Global = data.id_Inspeccion;
        this.formParams.patchValue({ "id_Inspeccion" : this.id_inspeccion_Global });

        this.alertasService.Swal_Success('Se agrego la inspeccion correctamente..'); 
        this.mostrarInformacion();

      }, error => {
        Swal.close();
        alert(JSON.stringify(error));
      },)
     
   }else{ /// editar

     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Actualizando, espere por favor'  })
     Swal.showLoading();    
    this.bandejaAtencionInspeccionService.set_editar_bandejaAtencion(this.formParams.value , this.formParams.value.id_Inspeccion)
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
  const listado = this.bandejaAtencionCab.find(u=> u.descripcion_TipoSancion.toUpperCase() === descripcion.toUpperCase());
  return listado;
}

 editar(obj_data:any){

    this.flag_modoEdicion=true;
    this.flag_modoEdicion_anomalias = true;
    this.id_inspeccion_Global = 0;
    this.anomaliasInspeccionesDet = [];
    
    this.id_inspeccion_Global = 0;
    this.id_inspeccion_Global = obj_data.id_Inspeccion;

    this.spinner.show();
    this.bandejaAtencionInspeccionService.get_tecnicos(obj_data.id_Cargo, this.idUserGlobal)
      .subscribe((res:any)=>{  
        this.spinner.hide();   

            this.formParams= new FormGroup({
              id_Inspeccion : new FormControl(obj_data.id_Inspeccion),
             id_EmpresaColaboradora : new FormControl( obj_data.id_EmpresaColaboradora),
             id_Cliente : new FormControl( obj_data.id_Cliente),
             lugar_Inspeccion : new FormControl( obj_data.lugar_Inspeccion),
             actividadOT_Inspeccion : new FormControl(obj_data.actividadOT_Inspeccion),
             trabajoArealizar_Inspeccion : new FormControl( obj_data.trabajoArealizar_Inspeccion),
             id_Cargo: new FormControl( obj_data.id_Cargo),
             id_Personal_Inspeccionado: new FormControl(obj_data.id_Personal_Inspeccionado),
         
             id_Area : new FormControl( obj_data.id_Area),
             id_Personal_Coordinador : new FormControl( obj_data.id_Personal_Coordinador),
             id_Personal_JefeObra : new FormControl( obj_data.id_Personal_JefeObra),
             placa_Inspeccion : new FormControl( obj_data.placa_Inspeccion),
             id_NivelInspeccion : new FormControl( obj_data.id_NivelInspeccion),
         
             id_TipoInspeccion : new FormControl( obj_data.id_TipoInspeccion),
             Resultado_Inspeccion : new FormControl( ''),
             iniciofin_Trabajo : new FormControl( obj_data.iniciofin_Trabajo),
          
             accionPropuesta_Correctiva : new FormControl( ''),
             id_Personal_Responsable : new FormControl( obj_data.id_Personal_Responsable),
         
             fechaPropuesta_Correctiva : new FormControl( new Date()),
             observacion_Correctiva : new FormControl( obj_data.observacion_Correctiva),
             paralizacion_Correctiva : new FormControl( obj_data.paralizacion_Correctiva),
             sancion_Correctiva : new FormControl( obj_data.sancion_Correctiva),
             id_TipoSancion : new FormControl( obj_data.id_TipoSancion),
             nroTrabajadores_Correctiva : new FormControl( obj_data.nroTrabajadores_Correctiva),
             Obs_Levantada : new FormControl( obj_data.Obs_Levantada),
             nro_inspeccionRelacionada : new FormControl( obj_data.nro_inspeccionRelacionada),
             estado : new FormControl( obj_data.estado),
           }) 
           
         setTimeout(()=>{ // 
           this.staticTabsPrincipal.tabs[0].active = true; 
           $('#modal_proceso').modal('show');  
         },0);

         this.get_anomaliasDet();

      }, error => {
        this.spinner.hide();
        alert(JSON.stringify(error));
      },)

      
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
     this.bandejaAtencionInspeccionService.set_anular_tipoSancion(objBD.id_TipoSancion)
       .subscribe((res:any)=>{  
         Swal.close();    
           for (const item of this.bandejaAtencionCab) {
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

 selectTab(data: TabDirective){   

  const nameTab = data.heading;  

     switch (nameTab) {
       case 'Datos Generales':
        setTimeout(()=>{ // 
          this.TabActivo_Global = 0;
          // alert('tab 0')
        },0);
       break;
       case 'Datos de Inspeccion':  
         setTimeout(()=>{ // 
          this.TabActivo_Global = 1;
          // alert('tab 1')
        },0);
       break;
       case 'Anomalias':  
         setTimeout(()=>{ // 
          this.TabActivo_Global = 2;
          // alert('tab 2')
        },0);
       break;           
       default:
         break;
     }
}

changeCargo(opcion:any){
  if ( opcion.target.value == 0 || opcion.target.value == '0' ) {
    this.tecnicos = [];
    this.formParams.patchValue({"id_Personal_Inspeccionado": '0'});
    return;
  }     
  this.mostrarTecnicos(opcion.target.value );
}

  mostrarTecnicos(idCargo:number){ 
    this.spinner.show();
    this.bandejaAtencionInspeccionService.get_tecnicos(idCargo, this.idUserGlobal)
      .subscribe((res:any)=>{  
        this.spinner.hide();  
  
        if (res.ok==true) {
          this.tecnicos = res.data;
        }else{
          this.spinner.hide();
          this.alertasService.Swal_alert('error', JSON.stringify(res.data));
        }
  
      }, error => {
        this.spinner.hide();
        alert(JSON.stringify(error));
      },)
  }




   changeTipoFormato(opcion:any){
    if ( opcion.target.value == 0 || opcion.target.value == '0' ) {
      this.tiposAnomalias = [];
      this.formParamsAnomalia.patchValue({"id_anomalia": '0'});
      return;
    }    
    this.formParamsAnomalia.patchValue({"id_anomalia": '0'}); 
    this.mostrarTipoAnomalias(opcion.target.value );
  }
  

   mostrarTipoAnomalias(idFormato:number){ 
    this.spinner.show();
    this.bandejaAtencionInspeccionService.get_tiposAnomalias_tipoFormato(idFormato, this.idUserGlobal)
      .subscribe((data:any)=>{  
        this.spinner.hide();  
        this.tiposAnomalias = data; 
      }, error => {
        this.spinner.hide();
        alert(JSON.stringify(error));
      },)
  }

  async saveUpdate_anomalias(){ 

    if ( this.id_inspeccion_Global == 0) {
      this.alertasService.Swal_alert('error','Es necesario primero Guardar los Datos de la Inspeccion.');
      return;
    }
 
    if (this.formParamsAnomalia.value.id_formato == '' || this.formParamsAnomalia.value.id_formato == '0' || this.formParamsAnomalia.value.id_formato == 0) {
        this.alertasService.Swal_alert('error','Por favor seleccione un Tipo de Formato');
        return;
    }
    else if (this.formParamsAnomalia.value.Resultado_Inspeccion == '0') {
        this.alertasService.Swal_alert('error','Es necesario seleccionar un Resultado de Inspección .');
        return;
    }
    else if (this.formParamsAnomalia.value.id_personal == '' || this.formParamsAnomalia.value.id_personal == '0' || this.formParamsAnomalia.value.id_personal == 0 || this.formParamsAnomalia.value.id_personal == null) {
        this.alertasService.Swal_alert('error','Por favor seleccione un Personal.');
        return ;
    }
    else if (this.formParamsAnomalia.value.Resultado_Inspeccion == 'Anomala') {
      if (this.formParamsAnomalia.value.id_anomalia == '' || this.formParamsAnomalia.value.id_anomalia == '0' || this.formParamsAnomalia.value.id_anomalia == 0) {
          this.alertasService.Swal_alert('error','Por favor seleccione una Anomalia');
          return ;
      }
    }

    if (this.formParamsAnomalia.value.Resultado_Inspeccion == 'Correcto') {
      this.formParamsAnomalia.patchValue({ "id_anomalia" : '0', "accionPropuesta_Correctiva" : '', "fechaPropuesta_Correctiva" : null ,"descripcion" : '' });
    }   

    this.formParamsAnomalia.patchValue({ "id_inspeccion" :  this.id_inspeccion_Global , "usuario_creacion" :  this.idUserGlobal });

    const fechaPropuesta = (this.formParamsAnomalia.value.fechaPropuesta_Correctiva == null ||  this.formParamsAnomalia.value.fechaPropuesta_Correctiva == '') ? null :  new Date(this.formParamsAnomalia.value.fechaPropuesta_Correctiva);
   
    if ( this.flag_modoEdicion_anomalias==false) { //// nuevo  

      const valor = this.validarPersonal(this.formParamsAnomalia.value.id_personal , this.formParamsAnomalia.value.id_anomalia )
      if (valor == true) { 
          this.alertasService.Swal_alert('error','Lo sentimos ya se agrego este Personal y la Anomalia..');
          return;
      }
   
       Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
       Swal.showLoading(); 
        this.bandejaAtencionInspeccionService.set_save_anomaliasDet({...this.formParamsAnomalia.value, fechaPropuesta_Correctiva :fechaPropuesta })
        .subscribe((data:any)=>{  
          Swal.close();    
 
          this.spinner.show(); 
           this.bandejaAtencionInspeccionService.set_NroSancionados(this.id_inspeccion_Global)
           .subscribe((resultado:any)=>{  
            this.spinner.hide(); 
            if (resultado == "OK") {

                this.alertasService.Swal_Success('Se agregó la Anomalia correctamente..');  

                // //+++ Guardando la Foto
                 this.upload_image(data.id_inspeccion_detalle );
                // //+++ Guardando la Foto
   
            } else {
                alert('Lo sentimos se produjo un error al Actualizar el nro Sancionados, vuelva  a intentarlo');
            }    
 
           }, error => {
            this.spinner.hide(); 
             alert(JSON.stringify(error));
           },)


        }, error => {
          Swal.close();
          alert(JSON.stringify(error));
        },)
       
     }else{ /// editar
  
      Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Actualizando, espere por favor'  })
      Swal.showLoading();    
      this.bandejaAtencionInspeccionService.set_editar_anomaliasDet(this.formParamsAnomalia.value , this.formParamsAnomalia.value.id_inspeccion_detalle)
        .subscribe((resultado:any)=>{  
          Swal.close();    
          if (resultado == "OK") {

            this.spinner.show(); 
            this.bandejaAtencionInspeccionService.set_NroSancionados(this.id_inspeccion_Global)
            .subscribe((resultado:any)=>{  
             this.spinner.hide(); 
             if (resultado == "OK") {
     
                this.alertasService.Swal_Success('Se actualizo la Anomalia correctamente..');  

              // //+++ Guardando la Foto
              this.upload_image(this.formParamsAnomalia.value.id_inspeccion_detalle );
              // //+++ Guardando la Foto
    
             } else {
                 alert('Lo sentimos se produjo un error al Actualizar el nro Sancionados, vuelva  a intentarlo');
             }     
 
            }, error => {
             this.spinner.hide(); 
              alert(JSON.stringify(error));
            },)  

          } else {
              alert('Lo sentimos se produjo un error al Actualizar el nro Sancionados, vuelva  a intentarlo');
          }  

        }, error => {
          Swal.close();
          alert(JSON.stringify(error));
        },)
  
     }
  
   } 
    
  validarPersonal(idPersonal: number, idAnomalia : number) {
    var resultado = false; 
    this.anomaliasInspeccionesDet.forEach(function (item, index) {
        if (item.id_personal == idPersonal && item.id_anomalia == idAnomalia) {
            resultado = true;
        }
    })
    return resultado;
  }

  blank_Detalle() {
    this.flag_modoEdicion_anomalias = false;
    this.inicializarFormulario_anomalias();
    this.inicializarFile();
  }

  get_anomaliasDet() {
    this.spinner.show(); 
    this.bandejaAtencionInspeccionService.get_anomaliasDet(this.id_inspeccion_Global)
    .subscribe((data:any)=>{  
      this.spinner.hide(); 
      this.anomaliasInspeccionesDet = data;   
      this.blank_Detalle();
    }, error => {
      this.spinner.hide(); 
      alert(JSON.stringify(error));
    },)
  }

  editar_anomaliasDet(obj:any){

    console.log(obj)
 
    this.flag_modoEdicion_anomalias = true; 
    const fechaPropuesta = (obj.fechaPropuesta_Correctiva ==null)? null :  obj.fechaPropuesta_Correctiva;

    if(obj.id_formato == 0){

      this.tiposAnomalias = []; 
      this.formParamsAnomalia = new FormGroup({ 
        id_Proyecto: new FormControl('0'), 
    
        id_inspeccion_detalle: new FormControl(obj.id_inspeccion_detalle),
        id_inspeccion: new FormControl(obj.id_inspeccion),
        id_personal: new FormControl(obj.id_personal),
        id_anomalia: new FormControl(obj.id_anomalia),
        descripcion: new FormControl(obj.descripcionAnomalia),
    
        levantamiento: new FormControl('VACIO'),
        foto_levantamiento: new FormControl('VACIO'),
        descripcion_levantamiento: new FormControl('VACIO'),
  
        id_formato: new FormControl(obj.id_formato),
        id_ValorInspeccion: new FormControl('NO'),
        estado:  new FormControl('1'),
        usuario_creacion:  new FormControl('0'),
  
        Resultado_Inspeccion : new FormControl(obj.Resultado_Inspeccion),
        accionPropuesta_Correctiva : new FormControl(obj.accionPropuesta_Correctiva), 
        fechaPropuesta_Correctiva : new FormControl(fechaPropuesta), 
     }) 
       return
    } 

    this.spinner.show();
    this.bandejaAtencionInspeccionService.get_tiposAnomalias_tipoFormato(obj.id_formato, this.idUserGlobal)
      .subscribe((data:any)=>{  
        this.spinner.hide();  
        this.tiposAnomalias = data; 

          this.formParamsAnomalia = new FormGroup({ 
            id_Proyecto: new FormControl('0'), 
        
            id_inspeccion_detalle: new FormControl(obj.id_inspeccion_detalle),
            id_inspeccion: new FormControl(obj.id_inspeccion),
            id_personal: new FormControl(obj.id_personal),
            id_anomalia: new FormControl(obj.id_anomalia),
            descripcion: new FormControl(obj.descripcionAnomalia),
        
            levantamiento: new FormControl('VACIO'),
            foto_levantamiento: new FormControl('VACIO'),
            descripcion_levantamiento: new FormControl('VACIO'),
      
            id_formato: new FormControl(obj.id_formato),
            id_ValorInspeccion: new FormControl('NO'),
            estado:  new FormControl('1'),
            usuario_creacion:  new FormControl('0'),
      
            Resultado_Inspeccion : new FormControl(obj.Resultado_Inspeccion),
            accionPropuesta_Correctiva : new FormControl(obj.accionPropuesta_Correctiva), 
            fechaPropuesta_Correctiva : new FormControl(fechaPropuesta), 
         }) 

      }, error => {
        this.spinner.hide();
        alert(JSON.stringify(error));
      },)

  }

 
  onFileChange(event:any) {   
    var filesTemporal = event.target.files; //FileList object       
      var fileE:InputFileI [] = []; 
      for (var i = 0; i < event.target.files.length; i++) { //for multiple files          
        fileE.push({
            'file': filesTemporal[i],
            'namefile': filesTemporal[i].name,
            'status': '',
            'message': ''
        })  
      }
       this.filePhoto = fileE;
   }

   upload_image(id_inspeccion_detalle:number) {
    if ( this.filePhoto.length ==0){
      //---Refrescando la informacion detalle--
      this.get_anomaliasDet();
      return;
    }
    Swal.fire({
      icon: 'info', allowOutsideClick: false,allowEscapeKey: false, text: 'Espere por favor'
    })
    Swal.showLoading();
   this.bandejaAtencionInspeccionService.upload_imagen_anomalia( this.filePhoto[0].file , id_inspeccion_detalle, this.idUserGlobal ).subscribe(
     (result:any) =>{
      Swal.close();

        const Obj_parametro_Foto = {
            id_inspeccion_foto : 0,
            id_inspeccion_detalle: id_inspeccion_detalle,
            nombre_foto: result.nombreArchivo,
            estado :1,
        }

        this.spinner.show();
        this.bandejaAtencionInspeccionService.save_inspeccciones_detalle_Foto(Obj_parametro_Foto)
          .subscribe((data:any)=>{  
            this.spinner.hide();  


          //---Refrescando la informacion detalle--
          this.get_anomaliasDet();


          }, error => {
            this.spinner.hide();
            alert(JSON.stringify(error));
          },)
 
       },(err) => {
        Swal.close();
        this.alertasService.Swal_alert('error',JSON.stringify(err)); 
       }
   )

 }

   abrir_modalFotos(id_inspeccion_detalle:any){
    this.spinner.show(); 
    this.bandejaAtencionInspeccionService.get_fotosAnomalias(id_inspeccion_detalle)
    .subscribe((data:any)=>{  
      this.spinner.hide(); 
          this.fotoSeleccionado = data;
         setTimeout(() => {
          $('#modalfotos').modal('show');
        }, 100);  

    }, error => {
      this.spinner.hide(); 
      alert(JSON.stringify(error));
    },)
  }

  cerrarModal_fotos(){
    setTimeout(()=>{ // 
      $('#modalfotos').modal('hide');  
    },0); 
 }
   
 
 cerrarModal_levantamiento(){
    setTimeout(()=>{ // 
      $('#modalLevantamiento').modal('hide');  
    },0); 
  }
 
  abrir_modalLevantamiento(obj:any){   
    this.id_inspeccion_detalle_Global = obj.id_inspeccion_detalle;  
    setTimeout(()=>{ // 
      $('#modalLevantamiento').modal('show');  
    },0); 
  } 
  
  Guardando_LevantamientoAnomalias( ) {
      if ( this.filePhoto.length ==0){
        this.alertasService.Swal_alert('error','Es necesario que incluya una imagen para poder Generar el Levantamiento de la Anomalia.');
        return;
      }

      if ( this.formParamsFile.value.observacion == '' || this.formParamsFile.value.observacion == null){
        this.alertasService.Swal_alert('error','Por favor ingrese la Observacion del Levantamiento de la Anomalia.');
        return;
      }

      Swal.fire({
        icon: 'info', allowOutsideClick: false,allowEscapeKey: false, text: 'Espere por favor'
      })
      Swal.showLoading();
      this.bandejaAtencionInspeccionService.upload_imagen_anomalia( this.filePhoto[0].file , this.id_inspeccion_detalle_Global, this.idUserGlobal ).subscribe(
       (result:any) =>{
        Swal.close();
  
          const Obj_parametro_Foto = { 
            id_inspeccion_detalle: this.id_inspeccion_detalle_Global,
            levantamiento: 'VACIO',
            foto_levantamiento: result.nombreArchivo,
            descripcion_levantamiento : this.formParamsFile.value.observacion,
          }
  
          this.spinner.show();
          this.bandejaAtencionInspeccionService.save_inspeccciones_detalle_AnomaliaFoto(Obj_parametro_Foto)
            .subscribe((data:any)=>{  
              this.spinner.hide();  
  
              this.alertasService.Swal_alert('success','Proceso realizado correctamente');

              //---Refrescando la informacion detalle--
              this.cerrarModal_levantamiento();
              this.get_anomaliasDet();
  
            }, error => {
              this.spinner.hide();
              alert(JSON.stringify(error));
            },)
   
         },(err) => {
          Swal.close();
          this.alertasService.Swal_alert('error',JSON.stringify(err)); 
         }
     )
  
  }

  abrir_modalFotosLevantamiento(obj:any){

    this.fotoSeleccionado = [];
    this.fotoSeleccionado.push({
      ruta_foto : obj.ruta_foto,
      descripcion_levantamiento : obj.descripcion_levantamiento
    })  

    setTimeout(() => {
     $('#modalfotos').modal('show');
   }, 0);  
  }

  cerrarModal_correo(){
    setTimeout(()=>{ // 
      $('#modalCorreo').modal('hide');  
    },0); 
  }
  
  abrir_modalCorreo(obj:any){

    this.inicializarFile();
     
    let email_jefe = '';
    let email_resp = '';
    let destinatario = '';
    let asuntos = '';

    this.spinner.show(); 
    this.bandejaAtencionInspeccionService.get_personal_email(this.idUserGlobal, this.formParams.value.id_Personal_JefeObra, this.formParams.value.id_Personal_Responsable  )
    .subscribe((data:any)=>{  
      this.spinner.hide(); 

          if (data.length ==1 ){
            destinatario = '';
          }else{
            email_jefe = data[1].correo;
            email_resp = data[2].correo;

            if (email_jefe == '' || email_jefe == null) {
              destinatario = data[2].correo;
            } else {
                if (email_resp == '' || email_resp == null) {
                  destinatario = data[1].correo;
                } else {
                  destinatario = data[1].correo + ';' + data[2].correo;
                }
            }    
          }  
          
          if (this.formParams.value.id_NivelInspeccion == 1 || this.formParams.value.id_NivelInspeccion == "1") {
            asuntos = "INSPECCION SIN INCUMPLIMIENTO";
          } else {
            asuntos = "INSPECCION CON INCUMPLIMIENTO"
          } 
 
         setTimeout(() => {
            this.formParamsFile = new FormGroup({
              file : new FormControl(''),
              observacion: new FormControl(''),            
              destinatario: new FormControl(destinatario),
              copia: new FormControl(data[0].correo),
              asunto: new FormControl(asuntos),
              mensaje: new FormControl(''),
              tipoFormato: new FormControl('0'),        
             })
            $('#modalCorreo').modal('show');
        }, 0);  

    }, error => {
      this.spinner.hide(); 
      alert(JSON.stringify(error));
    },)



 
  }

  enviar_correo(){

    if ( this.formParamsFile.value.destinatario == '' || this.formParamsFile.value.destinatario == null){
      this.alertasService.Swal_alert('error','Por favor ingrese el correo destinatario.');
      return;
    }
    if ( this.formParamsFile.value.asunto == '' || this.formParamsFile.value.asunto == null){
      this.alertasService.Swal_alert('error','Por favor ingrese un asunto.');
      return;
    }
    if ( this.formParamsFile.value.tipoFormato == '0' || this.formParamsFile.value.tipoFormato == 0){
      this.alertasService.Swal_alert('error','Por favor seleccione el formato.');
      return;
    }

    let codigoAle = Math.floor(Math.random() * 1000000) + '_' + new Date().getTime(); 
    let nombrePdf= codigoAle + '.pdf'

    this.spinner.show(); 
    this.bandejaAtencionInspeccionService.get_enviandoCorreo(this.id_inspeccion_Global, nombrePdf,this.formParamsFile.value.destinatario ,this.formParamsFile.value.copia, this.formParamsFile.value.asunto, this.formParamsFile.value.mensaje , this.formParamsFile.value.tipoFormato   )
    .subscribe((data:any)=>{  
       this.spinner.hide(); 
       console.log(data);
    }, error => {
      this.spinner.hide(); 
      alert(JSON.stringify(error));
    },)
  }


}

