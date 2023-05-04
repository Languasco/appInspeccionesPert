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
import { DashboardService } from 'src/app/services/reportes/dashboard.service';

declare const $:any;

@Component({
  selector: 'app-reportes-dashboard',
  templateUrl: './reportes-dashboard.component.html',
  styleUrls: ['./reportes-dashboard.component.css']
})
export class ReportesDashboardComponent implements OnInit {

  formParamsFiltro : FormGroup;
 
  idUserGlobal = 0;
  filtrarMantenimiento= '';
  reportesDashboard:any[] =[];
  reportesDashboard_det :any[] =[];
  datepiekerConfig:Partial<BsDatepickerConfig>

  paises:any[]=[]; 
  sociedades:any[] =[];
  delegaciones:any[] =[];
 
  flag_modoEdicion :boolean = false; 
  idPerfilReporte8 :boolean = false;
  idPerfilReporteVarios :boolean = false;

  constructor( private spinner: NgxSpinnerService, private alertasService : AlertasService, private loginService: LoginService, private funcionGlobalServices : FuncionesglobalesService,
    private anomaliasService : AnomaliasService, private dashboardService :  DashboardService ) { 
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
    this.dashboardService.get_mostrar_dashboard({...this.formParamsFiltro.value, fecha_ini :fechaIni, fecha_fin : fechaFin}, this.idUserGlobal, 0,1  )
    .subscribe((res:any)=>{ 
      console.log('mostrarInformacion') 
      console.log(res)
      this.spinner.hide();  

      this.idPerfilReporte8 = false;
      this.idPerfilReporteVarios = false;

      for (var i = 0; i < res.length; i++) {            
         if(Number(res[i].perfil) == 8){
          this.idPerfilReporte8  = true;
          break;
         }   
         if(Number(res[i].perfil) != 8){
          this.idPerfilReporteVarios  = true;
          break;
         }
      }

      this.reportesDashboard= res;
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

  MostrarDetalle(obj_data:any){
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
    const id_detallado = obj_data.Codigo;

    this.spinner.show();
    this.dashboardService.get_mostrar_dashboard({...this.formParamsFiltro.value, fecha_ini :fechaIni, fecha_fin : fechaFin}, this.idUserGlobal, id_detallado,2  )
    .subscribe((res:any)=>{ 

      this.spinner.hide();
          this.reportesDashboard_det = res;

          setTimeout(()=>{ // 
            $('#modal_proceso').modal('show');  
          },0); 
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

}


