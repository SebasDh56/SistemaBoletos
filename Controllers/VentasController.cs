using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaBoletos.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBoletos.Controllers
{
    public class VentasController : Controller
    {
        private readonly AppDbContext _context;
        private const decimal PrecioBoleto = 3.50m;

        public VentasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ventas = await _context.Ventas
                .Include(v => v.Persona)
                .Include(v => v.Cooperativa)
                .OrderByDescending(v => v.Fecha)
                .Take(50)
                .ToListAsync();

            return View(ventas);
        }

        public async Task<IActionResult> Create()
        {
            var personas = await _context.Personas.ToListAsync();
            if (!personas.Any())
            {
                TempData["Error"] = "Debe registrar al menos una persona antes de crear ventas";
                return RedirectToAction("Create", "Personas");
            }

            ViewData["PersonaId"] = new SelectList(personas, "Id", "CedulaNombre");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venta venta)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verificar que la persona existe
                    var persona = await _context.Personas.FindAsync(venta.PersonaId);
                    if (persona == null)
                    {
                        ModelState.AddModelError("PersonaId", "La persona seleccionada no existe");
                        throw new Exception("Persona no encontrada");
                    }

                    var imbaburapac = await _context.Cooperativas.FindAsync(1);
                    var lagos = await _context.Cooperativas.FindAsync(2);

                    // Calcular boletos vendidos en Imbaburapac
                    var boletosVendidos = await _context.Ventas
                        .Where(v => v.CooperativaId == 1)
                        .SumAsync(v => v.Cantidad);

                    if (boletosVendidos + venta.Cantidad <= imbaburapac.CapacidadMaxima)
                    {
                        venta.CooperativaId = 1;
                        venta.AplicaComision = false;
                    }
                    else
                    {
                        venta.CooperativaId = 2;
                        venta.AplicaComision = true;
                    }

                    venta.PrecioUnitario = 3.50m;
                    venta.Fecha = DateTime.Now;

                    _context.Add(venta);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = $"Venta registrada exitosamente para {persona.NombreCompleto}. Total: ${venta.Total.ToString("0.00")}";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                // Log del error (puedes ver esto en la consola de depuración)
                Console.WriteLine($"Error al registrar venta: {ex.Message}");
            }

            // Si llegamos aquí, hubo un error
            var personas = await _context.Personas.ToListAsync();
            ViewData["PersonaId"] = new SelectList(personas, "Id", "CedulaNombre", venta.PersonaId);
            TempData["Error"] = "Error al registrar la venta. Revise los datos.";
            return View(venta);
        }
        public async Task<IActionResult> Resumen()
        {
            var cooperativas = await _context.Cooperativas
                .Include(c => c.Ventas)
                .ToListAsync();

            var resumen = cooperativas.Select(c => new
            {
                c.Nombre,
                BoletosVendidos = c.Ventas.Sum(v => v.Cantidad),
                TotalVentas = c.Ventas.Sum(v => v.Cantidad * v.PrecioUnitario),
                BoletosFaltantes = c.Id == 1 ? Math.Max(0, c.CapacidadMaxima - c.Ventas.Sum(v => v.Cantidad)) : 0
            }).ToList();

            var ventasConComision = await _context.Ventas
                .Where(v => v.CooperativaId == 2 && v.AplicaComision)
                .SumAsync(v => v.Cantidad * v.PrecioUnitario);

            ViewBag.ComisionImbaburapac = ventasConComision * 0.10m;
            ViewBag.PrecioBoleto = PrecioBoleto;

            return View(resumen);
        }
    }
}