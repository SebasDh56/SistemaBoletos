using System.ComponentModel.DataAnnotations;

namespace SistemaBoletos.Models
{
    public class Venta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La persona es requerida")]
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }

        [Required(ErrorMessage = "La cooperativa es requerida")]
        public int CooperativaId { get; set; }
        public Cooperativa Cooperativa { get; set; }

        [Required(ErrorMessage = "La cantidad de boletos es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int CantidadBoletos { get; set; }

        [Required(ErrorMessage = "El precio unitario es requerido")]
        public decimal PrecioUnitario { get; set; } = 3.50m; // Precio fijo de $3.50

        public bool AplicaComision { get; set; }

        public decimal ComisionImbaburapac => AplicaComision ? CantidadBoletos * PrecioUnitario * 0.1m : 0m; // 10% para Imbaburapac

        public decimal Total => CantidadBoletos * PrecioUnitario; // Total sin incluir la comisión
    }
}