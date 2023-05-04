import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { combineLatest } from 'rxjs'; 
import { NgxSpinnerService } from 'ngx-spinner';
  
import Swal from 'sweetalert2';
import { AlertasService } from 'src/app/services/alertas/alertas.service';
import { LoginService } from 'src/app/services/login/login.service';
import { FuncionesglobalesService } from 'src/app/services/funciones/funcionesglobales.service';
 
import { ListadoInspeccionesService } from 'src/app/services/procesos/listado-inspecciones.service';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { AnomaliasService } from 'src/app/services/mantenimientos/anomalias.service';
 
import { TabDirective, TabsetComponent } from 'ngx-bootstrap';
import { InputFileI } from 'src/app/models/inputFile.models';
import { BandejaAtencionInspeccionService } from 'src/app/services/procesos/bandeja-atencion-inspeccion.service';


declare const $:any;
@Component({
  selector: 'app-listado-inspecciones',
  templateUrl: './listado-inspecciones.component.html',
  styleUrls: ['./listado-inspecciones.component.css']
})
 
export class ListadoInspeccionesComponent implements OnInit {

  formParamsFiltro : FormGroup;
  formParams: FormGroup;
  formParamsAnomalia: FormGroup;
  formParamsFile : FormGroup;

  idUserGlobal = 0;
  filtrarMantenimiento= '';
  listadoInspecciones:any[] =[];

  paises:any[]=[]; 
  sociedades:any[] =[];
  delegaciones:any[] =[]; 

  flag_modoEdicion :boolean = false; 
  datepiekerConfig:Partial<BsDatepickerConfig>

  inspectores :any[] =[];
  responsablesCorreccion :any[] =[];
 
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

  proyectos :any[]=[]; 
  estadosInspeccion :any[]=[]; 
  niveles :any[]=[]; 
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
  flag_modoEdicion_anomalias:boolean =false;

  @ViewChild('staticTabsPrincipal', { static: false }) staticTabsPrincipal: TabsetComponent;
  constructor( private spinner: NgxSpinnerService, private alertasService : AlertasService, private loginService: LoginService, private funcionGlobalServices : FuncionesglobalesService,
    private anomaliasService : AnomaliasService , private listadoInspeccionesService : ListadoInspeccionesService , private bandejaAtencionInspeccionService :BandejaAtencionInspeccionService ) { 
    this.idUserGlobal = this.loginService.get_idUsuario();
    this.datepiekerConfig = Object.assign({}, { containerClass : 'theme-red',  dateInputFormat: 'DD/MM/YYYY' })
  }

  ngOnInit(): void {
    this.inicializarFormularioFiltro(); 
    this.inicializarFormulario(); 
    this.inicializarFormulario_anomalias(); 
    this.inicializarFile();
    this.getCargarCombos();
    this.getCargarCombos2();    

    setTimeout(()=>{ //
      //------- utilizando el combo buscador --
        $('.select2Filtro').select2({
           multiple: true
       });
    },0);
 

    $('#cbo_delegacion_filtro').on("change", (e)=> {
      this.mostrarInspector_responsableCorreccion();
    });

  }
  
  inicializarFormularioFiltro(){ 
    this.formParamsFiltro = new FormGroup({ 
      fecha_ini: new FormControl(new Date()),
      fecha_fin: new FormControl(new Date()),
      id_pais: new FormControl('0'),
      id_grupo:new FormControl('0'),
      id_delegacion: new FormControl('0'),
      id_inspecctor: new FormControl('0'),
      id_responsableCorreccion: new FormControl('0'),
      Tipo: new FormControl('1'),
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
    combineLatest([ this.anomaliasService.get_paises(this.idUserGlobal), this.bandejaAtencionInspeccionService.get_proyectos() , this.bandejaAtencionInspeccionService.get_estadosInspecciones() , this.bandejaAtencionInspeccionService.get_nivelesInspecciones(), this.bandejaAtencionInspeccionService.get_personalesBandejaAtencion(),
      this.bandejaAtencionInspeccionService.get_clientes()  , this.bandejaAtencionInspeccionService.get_empresasContratistas()
    ])
       .subscribe(([ _paises, _proyectos, _estadosInspeccion, _niveles,_inspectores, _clientes, _empresasContratistas] :any )=>{  
        this.spinner.hide(); 

        this.paises = _paises;   
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
 
      }, error => {
        this.spinner.hide(); 
        alert(JSON.stringify(error));
      },)
  } 
  
  getCargarCombos2(){ 
    this.spinner.show(); 
    combineLatest([  this.bandejaAtencionInspeccionService.get_cargos() , this.bandejaAtencionInspeccionService.get_areas() ,
      this.bandejaAtencionInspeccionService.get_tiposInspecciones() , this.bandejaAtencionInspeccionService.get_tiposSanciones(), this.bandejaAtencionInspeccionService.get_tiposFormatos(),
      this.bandejaAtencionInspeccionService.get_personalAnomalias(), 
    ])
       .subscribe(([ _cargos, _areas, _tiposInspecciones, _tiposSanciones, _tiposFormatos,_personalAnomalias] :any )=>{  
        this.spinner.hide(); 

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

    let listMultipleDelegaciones :any =[];
    let listMultipleInspectores :any =[];
    let listMultipleRespCorreccion :any =[];

    const cboDelegaciones = $("#cbo_delegacion_filtro").val(); 
    const cboInspectores = $("#cbo_inspectores_filtro").val(); 
    const cboRespCorreccion = $("#cbo_respCorreccion_filtro").val();

    listMultipleDelegaciones =  this.funcionGlobalServices.obtenerValorComboSelect2Multiples(cboDelegaciones, this.delegaciones,'id_Delegacion');
    listMultipleInspectores =  this.funcionGlobalServices.obtenerValorComboSelect2Multiples(cboInspectores, this.inspectores,'id_Personal');
    listMultipleRespCorreccion =  this.funcionGlobalServices.obtenerValorComboSelect2Multiples(cboRespCorreccion, this.responsablesCorreccion,'id_Personal');

    const fechaIni = this.funcionGlobalServices.formatoFecha(this.formParamsFiltro.value.fecha_ini);
    const fechaFin = this.funcionGlobalServices.formatoFecha(this.formParamsFiltro.value.fecha_fin);


    this.spinner.show();
    this.listadoInspeccionesService.get_mostrar_informacion( this.idUserGlobal,  this.formParamsFiltro.value, listMultipleDelegaciones.join(), listMultipleInspectores.join(),listMultipleRespCorreccion.join(), this.formParamsFiltro.value.Tipo , fechaIni,fechaFin  )
    .subscribe((data:any)=>{  
      this.spinner.hide();  
      this.listadoInspecciones= data;
    }, error => {
      this.spinner.hide();
      alert(JSON.stringify(error));
    },)
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
      this.delegaciones = [];
      this.inspectores = [];
      this.responsablesCorreccion = []; 
      this.formParamsFiltro.patchValue({"id_delegacion": '0',"id_inspecctor": '0', "id_responsableCorreccion": '0'});
      return;
    }     
    this.mostrarSociedadesDelegacion(opcion.target.value );
  }

  mostrarSociedadesDelegacion(idGrupo:number){ 
    this.spinner.show();
    this.anomaliasService.get_delegacionesSociedades(idGrupo, this.idUserGlobal)
      .subscribe((data:any)=>{  
        this.spinner.hide();  
        this.delegaciones = data;    
        $('#cbo_delegacion_filtro').val("0").trigger('change');
       }, error => {
        this.spinner.hide();
        alert(JSON.stringify(error));
      },)
  }

  mostrarInspector_responsableCorreccion(){ 
 
    const cboDelegaciones = $("#cbo_delegacion_filtro").val(); 
    if(cboDelegaciones == null){
      this.inspectores = [];
      this.responsablesCorreccion = [];
      return;
    }

    let listDelegaciones :any =[];  
    listDelegaciones =  this.funcionGlobalServices.obtenerValorComboSelect2Multiples(cboDelegaciones, this.delegaciones,'id_Delegacion');
 
    this.spinner.show();
    this.listadoInspeccionesService.get_Inspector_responsableCorreccion(this.formParamsFiltro.value.id_pais, this.formParamsFiltro.value.id_grupo, listDelegaciones.join() , this.idUserGlobal)
      .subscribe((data:any)=>{  
        this.spinner.hide();  
        this.inspectores = data;
        this.responsablesCorreccion = data;
      }, error => {
        this.spinner.hide();
        alert(JSON.stringify(error));
      },)
  }
 
  keyPress(event: any) {
    this.funcionGlobalServices.verificar_soloNumeros(event)  ;
  }
 



  cerrarModal(){
    setTimeout(()=>{ // 
      $('#modal_proceso').modal('hide');  
    },0); 
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

    editar_anomaliasDet(obj:any){
   
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
  
    abrir_modalLevantamiento(obj:any){   
      this.id_inspeccion_detalle_Global = obj.id_inspeccion_detalle;  
      setTimeout(()=>{ // 
        $('#modalLevantamiento').modal('show');  
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

  blank_Detalle() {
    this.flag_modoEdicion_anomalias = false;
    this.inicializarFormulario_anomalias();
    this.inicializarFile();
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

  cerrarModal_correo(){
    setTimeout(()=>{ // 
      $('#modalCorreo').modal('hide');  
    },0); 
  }

}