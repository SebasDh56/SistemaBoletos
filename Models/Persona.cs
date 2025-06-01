using System.ComponentModel.DataAnnotations;

namespace SistemaBoletos.Models
{
    public class Persona
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "La edad es requerida")]
        [Range(1, 120, ErrorMessage = "La edad debe estar entre 1 y 120 años")]
        public int Edad { get; set; }
    }
}