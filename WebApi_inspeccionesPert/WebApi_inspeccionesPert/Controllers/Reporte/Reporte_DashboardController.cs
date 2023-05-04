using Entidades;
using Negocios.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi_inspeccionesPert.Controllers.Reporte
{
     [EnableCors("*", "*", "*")]
    public class Reporte_DashboardController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();
        public object get_Reporte_Dashboard(int opcion, string filtro)
        {
            db.Configuration.ProxyCreationEnabled = false;

            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');


                    int id_pais = Convert.ToInt32(parametros[0].ToString());
                    int id_grupo = Convert.ToInt32(parametros[1].ToString());
                    int id_delegacion = Convert.ToInt32(parametros[2].ToString());
                    string fecha_ini = parametros[3].ToString();
                    string fecha_fin = parametros[4].ToString(); 
                    int  id_personal =Convert.ToInt32(parametros[5].ToString());
                    int idDetalle = Convert.ToInt32(parametros[6].ToString());
                    int tiporeporte = Convert.ToInt32(parametros[7].ToString());

                    Reporte_Dashboard_BL obj_negocio = new Reporte_Dashboard_BL();
                    resul = obj_negocio.Listando_Reporte_DashBoard( id_pais, id_grupo, id_delegacion,   fecha_ini, fecha_fin, id_personal,idDetalle, tiporeporte);
                }
                else
                {
                    resul = "Opcion selecciona invalida";
                }
            }
            catch (Exception ex)
            {
                resul = ex.Message;
            }
            return resul;
        }

    }
}
