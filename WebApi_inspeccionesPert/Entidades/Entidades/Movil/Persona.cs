using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Movil
{
    public class Persona
    {
        public int PersonaId { get; set; }
        public int EmpresaId { get; set; }
        public int DelegacionId { get; set; }
        public int ProyectoId { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public int Perfil { get; set; }
        public string Email { get; set; }
        public string EnvioOnline { get; set; }
        public string Pass { get; set; }
        public string Users { get; set; }
    }
}
