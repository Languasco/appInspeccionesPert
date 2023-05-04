using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi_inspeccionesPert.Controllers.Mantenimientos
{
    [EnableCors("*", "*", "*")]
    public class AuditarController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();


        public class PersonaA
        {
            public int id_personal { get; set; }
            public string nombre_personal { get; set; }
            public string apellido_personal { get; set; }

        }



        public object GetAuditoria(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            
            if (id.Equals("null"))
            {
                List<PersonaA> listPer = new List<PersonaA>();
                PersonaA account = new PersonaA
                {
                    id_personal = 0,
                    nombre_personal = "No Existe",
                    apellido_personal = ""
                };
                listPer.Add(account);
                return listPer;
            }
            else
            {
                int valor = Convert.ToInt32(id);
                var list = (from a in db.tbl_usuarios  
                            where a.id_usuario  == valor
                            select new
                            {
                                id_personal = a.id_usuario  ,
                                nombre_personal = a.datos_personales ,
                                apellido_personal = "" ,

                            }).ToList();

                return list;
            }

        }

    }
}
