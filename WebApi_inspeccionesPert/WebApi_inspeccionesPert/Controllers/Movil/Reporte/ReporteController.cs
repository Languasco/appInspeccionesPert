using Entidades;
using Entidades.Movil.Reporte;
using Negocios.Movil.Reporte;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
namespace WebApi_inspeccionesPert.Controllers.Movil.Reporte
{
    public class ReporteController : ApiController
    {

        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        public HttpResponseMessage GetInspectionAnomaly(int proyectoId, int delegacionId, int empresaId, string fecha, int perfil, int tipoReport, int inspectorId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            HttpResponseMessage response;
            ReporteBL reporteBL = new ReporteBL();
            response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(reporteBL.getInspectionAnomaly(proyectoId, delegacionId, empresaId, fecha, perfil, tipoReport, inspectorId).Select(x => new
                {
                    codigo = x.Codigo,
                    dato = x.Dato,
                    total = x.total,
                    normales = x.normales,
                    anomalias = x.anomalias,
                    levantadas = x.levantadas,
                    pendientes = x.pendientes,
                    fecha = x.fecha,
                    nivel = x.nivel,
                    ipal = x.ipal
                }
            )), Encoding.UTF8, "application/json");

            return response;
        }

    }
}
