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
    public class EstadoOrdenComprasController : Controller
    {
        private readonly AppDbContext _context;

        public EstadoOrdenComprasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EstadoOrdenCompras
   
        public async Task<IActionResult> Index(string estado)
        {
            var query = _context.EstadosOrdenCompra.AsQueryable();

            if (!string.IsNullOrWhiteSpace(estado))
            {
                query = query.Where(e => e.Estado.Contains(estado));
            }

            var estadosOrdenCompra = await query.ToListAsync();
            return View(estadosOrdenCompra);
        }

        // GET: EstadoOrdenCompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoOrdenCompra = await _context.EstadosOrdenCompra
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estadoOrdenCompra == null)
            {
                return NotFound();
            }

            return View(estadoOrdenCompra);
        }

        // GET: EstadoOrdenCompras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstadoOrdenCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Estado,FechaCreacion,FechaModificacion")] EstadoOrdenCompra estadoOrdenCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadoOrdenCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadoOrdenCompra);
        }

        // GET: EstadoOrdenCompras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoOrdenCompra = await _context.EstadosOrdenCompra.FindAsync(id);
            if (estadoOrdenCompra == null)
            {
                return NotFound();
            }
            return View(estadoOrdenCompra);
        }

        // POST: EstadoOrdenCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Estado,FechaCreacion,FechaModificacion")] EstadoOrdenCompra estadoOrdenCompra)
        {
            if (id != estadoOrdenCompra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadoOrdenCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoOrdenCompraExists(estadoOrdenCompra.Id))
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
            return View(estadoOrdenCompra);
        }

        // GET: EstadoOrdenCompras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoOrdenCompra = await _context.EstadosOrdenCompra
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estadoOrdenCompra == null)
            {
                return NotFound();
            }

            return View(estadoOrdenCompra);
        }

        // POST: EstadoOrdenCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadoOrdenCompra = await _context.EstadosOrdenCompra.FindAsync(id);
            if (estadoOrdenCompra != null)
            {
                _context.EstadosOrdenCompra.Remove(estadoOrdenCompra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoOrdenCompraExists(int id)
        {
            return _context.EstadosOrdenCompra.Any(e => e.Id == id);
        }
    }
}
