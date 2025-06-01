using System.ComponentModel.DataAnnotations;

namespace SistemaBoletos.Models
{
    public class Cooperativa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la cooperativa es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La capacidad máxima es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "La capacidad debe ser mayor a 0")]
        public int CapacidadMaxima { get; set; }

        public int BoletosVendidos { get; set; }
    }
}