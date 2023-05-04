using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Mantenimiento
{
    public class Personal_E
    {
        public int   id_pais { set; get; }
        public int id_Empresa { set; get; }
        public int id_Proyecto { set; get; }
        public string  tipoDoc_Personal { set; get; }
        public string  nroDoc_Personal { set; get; }
        public string  fechaIngreso_Personal { set; get; }
        public string  nombre_cargo { set; get; }
        public string  tipoPersonal { set; get; }
        public string  fechaCese_Personal { set; get; }
        public string  email_personal { set; get; }
        public string  login_Sistema { set; get; }
        public string  Contrasenia_sistema { set; get; }
        public int id_Perfil { set; get; }
        public string  envio_Online { set; get; }
        public int estado { set; get; }
        public string  usuario_creacion { set; get; }
        public string  fecha_creacion { set; get; }
        public string  usuario_edicion { set; get; }
        public string  fecha_edicion { set; get; }
        public int id_EmpresaColaboradora { set; get; }
        public string codigo_personal { set; get; }        

        public bool checkeado { set; get; }
        public int id_personal_Delegacion { set; get; }
        public int id_Personal { set; get; }
        public int id_Delegacion { set; get; }
        public string nombre_delegacion { set; get; }
        public string identificador { set; get; }
        public string personal { set; get; } 
        
        public string apellidos_Personal { set; get; }
        public string nombres_Personal { set; get; }
        public int id_Cargo { set; get; }
        public string id_grupo { set; get; }

        public string datos { set; get; }
        public int cantidades { set; get; }


    }

    public class Personal_Repre
    {
        public int id_Personal { get; set; }
        public string nroDoc_Personal { get; set; }
        public string apellidos_Personal { get; set; }
        public string nombres_Personal { get; set; }
    }

    public class Personal_list
    {
        public List<Personal_E> list_data { get; set; }
        public int totalcount { get; set; }
    }


}
