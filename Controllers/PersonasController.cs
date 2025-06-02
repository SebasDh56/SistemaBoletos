using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaBoletos.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBoletos.Controllers
{
    public class PersonasController : Controller
    {
        private readonly AppDbContext _context;

        public PersonasController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Persona persona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(persona);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Persona {persona.NombreCompleto} registrada exitosamente!";
                return RedirectToAction("Index", "Ventas");
            }

            TempData["Error"] = "Error al registrar persona. Revise los datos.";
            return View(persona);
        }
    }
}