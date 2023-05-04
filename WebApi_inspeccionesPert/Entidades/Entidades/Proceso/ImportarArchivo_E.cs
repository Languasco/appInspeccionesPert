using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Procesos
{
   public class ImportarArchivo_E
    {

        public string Codigo_ot { set; get; }
        public string Nombre_ot { set; get; }
        public string Codigo_delegacion { set; get; }

        public int id_Cliente { set; get; }
        public string Ruc_cliente { set; get; }
        public string Nombre_proyecto { set; get; }

        public int id_jefe { set; get; }
        public string Dni_jefeObra { set; get; }
        public int id_cordinador { set; get; }

        public string Dni_coordinado { set; get; }
        public int id_Actividad { set; get; }
        public string Codigo_actividad { set; get; }
        public string mensaje { set; get; }

        public int id_pais { set; get; }
        public string Nombre_Pais { set; get; }
        public int id_grupo { set; get; }
        public string Nombre_Grupo { set; get; }
        public string RUC_Empresa { set; get; }
        public string Nombre_Empresa { set; get; }
        public string Cargo_Espania { set; get; }

        public int  id_Empresas  { set; get; }
        public string Empresa { set; get; }
        public int id_Delegacion { set; get; }
        public string Delegacion { set; get; }
        public int id_Proyecto { set; get; }
        public string Proyecto { set; get; }
        public string Tipo_documento { set; get; }
        public string Nro_Documento { set; get; }
        public string mensaje_reniec { set; get; }
        public string Apellidos { set; get; }
        public string Nombres { set; get; }
        public int id_Cargo { set; get; }
        public string Cargo { set; get; }
        public string Estado { set; get; }
        public string Email { set; get; }
        public string Codigo { set; get; }
        public string Usuario { set; get; }
        public string Contrasenia { set; get; }
        public int id_perfil { set; get; }
        public string Perfil { set; get; }

        public int id_EmpresaColaboradora { set; get; }
        public string RUC_Empresa_Colaboradora { set; get; }
        public string Nombre_Empresa_Colaboradora { set; get; }

    }
}
