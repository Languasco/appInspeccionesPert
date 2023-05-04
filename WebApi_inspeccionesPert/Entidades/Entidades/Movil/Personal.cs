
namespace Entidades.Movil
{

    public class Personal
    {
        public int PersonalId { get; set; }
        public string NroDoc { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public int CargoId { get; set; }
        public string NombreCargo { get; set; }
        public string Email { get; set; }
        public int EmpresaColaboradoraId { get; set; }
    }
}
