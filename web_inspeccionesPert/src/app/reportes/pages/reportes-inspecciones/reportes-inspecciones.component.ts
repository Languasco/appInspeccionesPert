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
import { PersonalService } from '../../../services/mantenimientos/personal.service';
import { ReporteInspeccionesService } from 'src/app/services/reportes/reporte-inspecciones.service';
import { BsDatepickerConfig } from 'ngx-bootstrap';

declare const $:any;

@Component({
  selector: 'app-reportes-inspecciones',
  templateUrl: './reportes-inspecciones.component.html',
  styleUrls: ['./reportes-inspecciones.component.css']
})
 

export class ReportesInspeccionesComponent implements OnInit {

  formParamsFiltro : FormGroup;
 
  idUserGlobal = 0;
  filtrarMantenimiento= '';
  reportesInspecciones:any[] =[];
  datepiekerConfig:Partial<BsDatepickerConfig>

  paises:any[]=[]; 
  sociedades:any[] =[];
  delegaciones:any[] =[];
 
  flag_modoEdicion :boolean = false; 

  constructor( private spinner: NgxSpinnerService, private alertasService : AlertasService, private loginService: LoginService, private funcionGlobalServices : FuncionesglobalesService,
    private anomaliasService : AnomaliasService, private reporteInspeccionesService :  ReporteInspeccionesService ) { 
    this.idUserGlobal = this.loginService.get_idUsuario();
    this.datepiekerConfig = Object.assign({}, { containerClass : 'theme-red',  dateInputFormat: 'DD/MM/YYYY' })
  }

  ngOnInit(): void {
    this.getCargarCombos();
    this.inicializarFormularioFiltro();    
  }
  
  inicializarFormularioFiltro(){ 
    this.formParamsFiltro = new FormGroup({ 
      id_pais: new FormControl('0'),
      id_grupo:new FormControl('0'),
      id_Delegacion: new FormControl('0'),
      fecha_ini: new FormControl(new Date()),
      fecha_fin: new FormControl(new Date()),
     }) 
  }

 

  getCargarCombos(){ 
    this.spinner.show(); 
    combineLatest([ this.anomaliasService.get_paises(this.idUserGlobal) ])
       .subscribe(([ _paises, _tiposFormatos] :any )=>{  
        this.spinner.hide(); 
          this.paises = _paises;     
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
    if (this.formParamsFiltro.value.id_Delegacion == 0 || this.formParamsFiltro.value.id_Delegacion == '0')  {
      this.alertasService.Swal_alert('error', 'Por favor seleccione una Delegacion.');
      return
    }
    if (this.formParamsFiltro.value.fecha_ini == '') {
      this.alertasService.Swal_alert('error', 'Por favor ingrese o seleccione la Fecha Inicial.');
      return;
    }
     if (this.formParamsFiltro.value.fecha_fin == '') {
        this.alertasService.Swal_alert('error', 'Por favor ingrese o seleccione la Fecha Final.');
        return;
    }  

    const fechaIni = this.funcionGlobalServices.formatoFecha(this.formParamsFiltro.value.fecha_ini);
    const fechaFin = this.funcionGlobalServices.formatoFecha(this.formParamsFiltro.value.fecha_fin);

    this.spinner.show();
    this.reporteInspeccionesService.get_mostrar_informacion({...this.formParamsFiltro.value, fecha_ini :fechaIni, fecha_fin : fechaFin})
    .subscribe((res:any)=>{ 
      console.log('mostrarInformacion') 
      console.log(res)
      this.spinner.hide();  
      this.reportesInspecciones= res;
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
      this.formParamsFiltro.patchValue({"id_Delegacion": '0'});
      return;
    }     
    this.mostrarDelegacionesSociedades(opcion.target.value );
  }

  mostrarDelegacionesSociedades(idGrupo:number){ 
    this.spinner.show();
    this.anomaliasService.get_delegacionesSociedades(idGrupo, this.idUserGlobal)
      .subscribe((data:any)=>{  
        this.spinner.hide();  
        this.delegaciones= data;
      }, error => {
        this.spinner.hide();
        alert(JSON.stringify(error));
      },)
  }

  keyPress(event: any) {
    this.funcionGlobalServices.verificar_soloNumeros(event)  ;
  }

  
 descargarReporte(){  

  if (this.formParamsFiltro.value.id_pais == 0 || this.formParamsFiltro.value.id_pais == '0')  {
    this.alertasService.Swal_alert('error', 'Por favor seleccione el Pais.');
    return
  }
  if (this.formParamsFiltro.value.id_grupo == 0 || this.formParamsFiltro.value.id_grupo == '0')  {
    this.alertasService.Swal_alert('error', 'Por favor seleccione la Sociedad.');
    return
  }
  if (this.formParamsFiltro.value.id_Delegacion == 0 || this.formParamsFiltro.value.id_Delegacion == '0')  {
    this.alertasService.Swal_alert('error', 'Por favor seleccione una Delegacion.');
    return
  }
  if (this.formParamsFiltro.value.fecha_ini == '') {
    this.alertasService.Swal_alert('error', 'Por favor ingrese o seleccione la Fecha Inicial.');
    return;
  }
   if (this.formParamsFiltro.value.fecha_fin == '') {
      this.alertasService.Swal_alert('error', 'Por favor ingrese o seleccione la Fecha Final.');
      return;
  }  

  const fechaIni = this.funcionGlobalServices.formatoFecha(this.formParamsFiltro.value.fecha_ini);
  const fechaFin = this.funcionGlobalServices.formatoFecha(this.formParamsFiltro.value.fecha_fin);
 
  this.spinner.show();   
  this.reporteInspeccionesService.set_DescargarInspecciones({...this.formParamsFiltro.value , fecha_ini : fechaIni , fecha_fin : fechaFin })
    .subscribe((data:any)=>{
      this.spinner.hide(); 
      const res = data.split('|');

      if (res[0] == 0 || res[0] == "0") {
        alert(JSON.stringify(res[1]));
      }else {
        window.open(String(res[1]),'_blank');
      } 
  }) 
} 

 
descargarReporte_new(){  

  if (this.formParamsFiltro.value.id_pais == 0 || this.formParamsFiltro.value.id_pais == '0')  {
    this.alertasService.Swal_alert('error', 'Por favor seleccione el Pais.');
    return
  }
  if (this.formParamsFiltro.value.id_grupo == 0 || this.formParamsFiltro.value.id_grupo == '0')  {
    this.alertasService.Swal_alert('error', 'Por favor seleccione la Sociedad.');
    return
  }
  if (this.formParamsFiltro.value.id_Delegacion == 0 || this.formParamsFiltro.value.id_Delegacion == '0')  {
    this.alertasService.Swal_alert('error', 'Por favor seleccione una Delegacion.');
    return
  }
  if (this.formParamsFiltro.value.fecha_ini == '') {
    this.alertasService.Swal_alert('error', 'Por favor ingrese o seleccione la Fecha Inicial.');
    return;
  }
   if (this.formParamsFiltro.value.fecha_fin == '') {
      this.alertasService.Swal_alert('error', 'Por favor ingrese o seleccione la Fecha Final.');
      return;
  }  

  const fechaIni = this.funcionGlobalServices.formatoFecha(this.formParamsFiltro.value.fecha_ini);
  const fechaFin = this.funcionGlobalServices.formatoFecha(this.formParamsFiltro.value.fecha_fin);
 
  this.spinner.show();   
  this.reporteInspeccionesService.set_DescargarInspeccionesNew({...this.formParamsFiltro.value , fecha_ini : fechaIni , fecha_fin : fechaFin }, this.idUserGlobal)
    .subscribe((data:any)=>{
      this.spinner.hide(); 
      const res = data.split('|');

      if (res[0] == 0 || res[0] == "0") {
        alert(JSON.stringify(res[1]));
      }else {
        window.open(String(res[1]),'_blank');
      } 
  }) 
} 


 


}

