using System.ComponentModel.DataAnnotations;

namespace SistemaBoletos.Models
{
    public class Persona
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La cédula es requerida")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "La edad es requerida")]
        [Range(1, 120, ErrorMessage = "La edad debe estar entre 1 y 120")]
        public int Edad { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellido}";
        public string CedulaNombre => $"{Cedula} - {NombreCompleto}";
    }
}