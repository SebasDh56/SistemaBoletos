using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaBoletos.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }
        public int CooperativaId { get; set; }
        public Cooperativa Cooperativa { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public bool AplicaComision { get; set; }
        public DateTime Fecha { get; set; }

        [NotMapped]
        public decimal Total => Cantidad * PrecioUnitario;
    }
}