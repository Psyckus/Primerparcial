using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial_1.Data;
using Parcial_1.Models;
using Parcial_1.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Parcial_1.Controllers
{
    public class PromocionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly PromocionService _promocionService;

        public PromocionsController(AppDbContext context, PromocionService promocionService)
        {
            _context = context;
            _promocionService = promocionService;
        }

        // GET: Promocions
        // Controlador de vista (Controller)
        public async Task<IActionResult> Index(string? descripcion, int? porcentajeDescuento, DateTime? fechaInicia, DateTime? fechaFinaliza)
        {
            IEnumerable<Promocion> promocion;

            // Verifica si al menos uno de los campos tiene un valor
            if (!string.IsNullOrEmpty(descripcion) || porcentajeDescuento.HasValue || fechaInicia.HasValue || fechaFinaliza.HasValue)
            {
                // Realiza la búsqueda mediante el servicio
                promocion = await _promocionService.BuscarPromocion(descripcion, porcentajeDescuento, fechaInicia, fechaFinaliza);
            }
            else
            {
                // Carga todos los datos si no se ha especificado ningún valor
                promocion = await _context.Promociones.ToListAsync();
            }

            return View(promocion);
          
        }


        // GET: Promocions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocion = await _context.Promociones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (promocion == null)
            {
                return NotFound();
            }

            return View(promocion);
        }

        // GET: Promocions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Promocions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreCorto,Descripcion,PorcentajeDescuento,FechaInicia,FechaFinaliza,FechaCreacion,FechaModificacion")] Promocion promocion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promocion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(promocion);
        }

        // GET: Promocions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocion = await _context.Promociones.FindAsync(id);
            if (promocion == null)
            {
                return NotFound();
            }
            return View(promocion);
        }

        // POST: Promocions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreCorto,Descripcion,PorcentajeDescuento,FechaInicia,FechaFinaliza,FechaCreacion,FechaModificacion")] Promocion promocion)
        {
            if (id != promocion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promocion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromocionExists(promocion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(promocion);
        }

        // GET: Promocions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocion = await _context.Promociones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (promocion == null)
            {
                return NotFound();
            }

            return View(promocion);
        }

        // POST: Promocions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var promocion = await _context.Promociones.FindAsync(id);
            if (promocion != null)
            {
                _context.Promociones.Remove(promocion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromocionExists(int id)
        {
            return _context.Promociones.Any(e => e.Id == id);
        }
    }
}
