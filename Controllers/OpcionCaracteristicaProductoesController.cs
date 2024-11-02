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
    public class OpcionCaracteristicaProductoesController : Controller
    {
        private readonly AppDbContext _context;

        public OpcionCaracteristicaProductoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OpcionCaracteristicaProductoes
        // Controlador de vista (Controller)
        public async Task<IActionResult> Index(string? valor)
        {
            var query = _context.OpcionesCaracteristicaProducto.AsQueryable();

            if (!string.IsNullOrEmpty(valor))
            {
                query = query.Where(ocp => EF.Functions.Like(ocp.Valor, $"%{valor}%"));
            }

            var opciones = await query.ToListAsync();
            return View(opciones);
        }


        // GET: OpcionCaracteristicaProductoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opcionCaracteristicaProducto = await _context.OpcionesCaracteristicaProducto
                .FirstOrDefaultAsync(m => m.IdOpcionCaracteristicaProducto == id);
            if (opcionCaracteristicaProducto == null)
            {
                return NotFound();
            }

            return View(opcionCaracteristicaProducto);
        }

        // GET: OpcionCaracteristicaProductoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OpcionCaracteristicaProductoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOpcionCaracteristicaProducto,IdCaracteristicaProducto,Valor,FechaCreacion,FechaModificacion")] OpcionCaracteristicaProducto opcionCaracteristicaProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(opcionCaracteristicaProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(opcionCaracteristicaProducto);
        }

        // GET: OpcionCaracteristicaProductoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opcionCaracteristicaProducto = await _context.OpcionesCaracteristicaProducto.FindAsync(id);
            if (opcionCaracteristicaProducto == null)
            {
                return NotFound();
            }
            return View(opcionCaracteristicaProducto);
        }

        // POST: OpcionCaracteristicaProductoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOpcionCaracteristicaProducto,IdCaracteristicaProducto,Valor,FechaCreacion,FechaModificacion")] OpcionCaracteristicaProducto opcionCaracteristicaProducto)
        {
            if (id != opcionCaracteristicaProducto.IdOpcionCaracteristicaProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opcionCaracteristicaProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpcionCaracteristicaProductoExists(opcionCaracteristicaProducto.IdOpcionCaracteristicaProducto))
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
            return View(opcionCaracteristicaProducto);
        }

        // GET: OpcionCaracteristicaProductoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opcionCaracteristicaProducto = await _context.OpcionesCaracteristicaProducto
                .FirstOrDefaultAsync(m => m.IdOpcionCaracteristicaProducto == id);
            if (opcionCaracteristicaProducto == null)
            {
                return NotFound();
            }

            return View(opcionCaracteristicaProducto);
        }

        // POST: OpcionCaracteristicaProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var opcionCaracteristicaProducto = await _context.OpcionesCaracteristicaProducto.FindAsync(id);
            if (opcionCaracteristicaProducto != null)
            {
                _context.OpcionesCaracteristicaProducto.Remove(opcionCaracteristicaProducto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpcionCaracteristicaProductoExists(int id)
        {
            return _context.OpcionesCaracteristicaProducto.Any(e => e.IdOpcionCaracteristicaProducto == id);
        }
    }
}
