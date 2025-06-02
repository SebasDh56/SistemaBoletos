using System.Collections.Generic;

namespace SistemaBoletos.Models
{
    public class Cooperativa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CapacidadMaxima { get; set; }
        public List<Venta> Ventas { get; set; } = new List<Venta>();
    }
}