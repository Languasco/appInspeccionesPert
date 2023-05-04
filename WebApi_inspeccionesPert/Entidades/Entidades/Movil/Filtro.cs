using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Movil
{
    public class Filtro
    {
        public int paisId { get; set; } // buscar personal // save personal
        //public int proyectoId { get; set; }                // save personal
        public int delegacionId { get; set; }              // save personal
        public int grupoId { get; set; }                  // save personal
        public int otId { get; set; }
        public int inspeccionId { get; set; }              // save personal usuario creacion
        public string version { get; set; }

        // buscar personal

        public string nombre { get; set; }         //save personal
        public int tipo { get; set; }



        // para guardar personal

        public string tipoDoc { get; set; }        //save personal
        public string nroDoc { get; set; }         //save personal
        public string apellidos { get; set; }      //save personal 
        public int cargoId { get; set; }           //save personal
        public string email { get; set; }          //save personal
        public string fecha { get; set; }          //save personal
    }
}
