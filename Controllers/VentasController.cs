using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaBoletos.Models;

namespace SistemaBoletos.Controllers
{
    public class VentasController : Controller
    {
        private readonly AppDbContext _context;

        public VentasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            ViewData["PersonaId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Personas, "Id", "Nombre");
            return View();
        }

        // POST: Ventas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonaId,CantidadBoletos")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                // Obtener la cooperativa Imbaburapac (Id = 1)
                var imbaburapac = await _context.Cooperativas.FindAsync(1);
                var lagos = await _context.Cooperativas.FindAsync(2);

                if (imbaburapac == null || lagos == null)
                {
                    ModelState.AddModelError("", "Cooperativas no configuradas correctamente.");
                    return View(venta);
                }

                // Verificar capacidad de Imbaburapac
                var boletosVendidosImbaburapac = _context.Ventas
                    .Where(v => v.CooperativaId == 1)
                    .Sum(v => v.CantidadBoletos);

                if (boletosVendidosImbaburapac + venta.CantidadBoletos <= imbaburapac.CapacidadMaxima)
                {
                    // Asignar a Imbaburapac sin comisión
                    venta.CooperativaId = 1;
                    venta.AplicaComision = false;
                    imbaburapac.BoletosVendidos += venta.CantidadBoletos;
                }
                else
                {
                    // Asignar a Lagos con comisión para Imbaburapac
                    venta.CooperativaId = 2;
                    venta.AplicaComision = true;
                    lagos.BoletosVendidos += venta.CantidadBoletos;
                }

                venta.PrecioUnitario = 3.50m; // Precio fijo
                _context.Add(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PersonaId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Personas, "Id", "Nombre", venta.PersonaId);
            return View(venta);
        }

        // GET: Ventas/Resumen
        public async Task<IActionResult> Resumen()
        {
            var cooperativas = await _context.Cooperativas
                .Select(c => new
                {
                    c.Nombre,
                    c.BoletosVendidos,
                    BoletosFaltantes = c.CapacidadMaxima - c.BoletosVendidos,
                    TotalVentas = _context.Ventas
                        .Where(v => v.CooperativaId == c.Id)
                        .Sum(v => v.Total),
                    ComisionImbaburapac = c.Id == 2 ? _context.Ventas
                        .Where(v => v.CooperativaId == c.Id)
                        .Sum(v => v.ComisionImbaburapac) : 0m
                })
                .ToListAsync();

            return View(cooperativas);
        }
    }
}