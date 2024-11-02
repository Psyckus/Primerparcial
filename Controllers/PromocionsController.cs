using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial_1.Data;
using Parcial_1.Models;

namespace Parcial_1.Controllers
{
    public class PromocionsController : Controller
    {
        private readonly AppDbContext _context;

        public PromocionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Promocions
        // Controlador de vista (Controller)
        public async Task<IActionResult> Index(string? descripcion, int? porcentajeDescuento, DateTime? fechaInicia, DateTime? fechaFinaliza)
        {
            var query = _context.Promociones.AsQueryable();

            if (!string.IsNullOrEmpty(descripcion))
            {
                query = query.Where(p => EF.Functions.Like(p.Descripcion, $"%{descripcion}%"));
            }

            if (porcentajeDescuento.HasValue)
            {
                query = query.Where(p => p.PorcentajeDescuento == porcentajeDescuento.Value);
            }

            if (fechaInicia.HasValue)
            {
                query = query.Where(p => p.FechaInicia.Date == fechaInicia.Value.Date);
            }

            if (fechaFinaliza.HasValue)
            {
                query = query.Where(p => p.FechaFinaliza.Date == fechaFinaliza.Value.Date);
            }

            var promociones = await query.ToListAsync();
            return View(promociones);
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
