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
    public class OrdenComprasController : Controller
    {
        private readonly AppDbContext _context;

        public OrdenComprasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrdenCompras
        // Controlador de vista (Controller)
        public async Task<IActionResult> Index(decimal? montoOrden, int? idEstadoOrden)
        {
            var query = _context.OrdenesCompra.AsQueryable();

            if (montoOrden != null)
            {
                query = query.Where(oc => oc.MontoOrden == montoOrden);
            }

            if (idEstadoOrden != null)
            {
                query = query.Where(oc => oc.IdEstadoOrden == idEstadoOrden);
            }

            var ordenes = await query.ToListAsync();
            return View(ordenes);
        }


        // GET: OrdenCompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenCompra = await _context.OrdenesCompra
                .FirstOrDefaultAsync(m => m.IdOrdenCompra == id);
            if (ordenCompra == null)
            {
                return NotFound();
            }

            return View(ordenCompra);
        }

        // GET: OrdenCompras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrdenCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOrdenCompra,IdCliente,IdMetodoPago,IdDireccionEnvio,IdMetodoEnvio,MontoOrden,IdEstadoOrden,FechaCreacion,FechaModificacion")] OrdenCompra ordenCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordenCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ordenCompra);
        }

        // GET: OrdenCompras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenCompra = await _context.OrdenesCompra.FindAsync(id);
            if (ordenCompra == null)
            {
                return NotFound();
            }
            return View(ordenCompra);
        }

        // POST: OrdenCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOrdenCompra,IdCliente,IdMetodoPago,IdDireccionEnvio,IdMetodoEnvio,MontoOrden,IdEstadoOrden,FechaCreacion,FechaModificacion")] OrdenCompra ordenCompra)
        {
            if (id != ordenCompra.IdOrdenCompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordenCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenCompraExists(ordenCompra.IdOrdenCompra))
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
            return View(ordenCompra);
        }

        // GET: OrdenCompras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenCompra = await _context.OrdenesCompra
                .FirstOrDefaultAsync(m => m.IdOrdenCompra == id);
            if (ordenCompra == null)
            {
                return NotFound();
            }

            return View(ordenCompra);
        }

        // POST: OrdenCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordenCompra = await _context.OrdenesCompra.FindAsync(id);
            if (ordenCompra != null)
            {
                _context.OrdenesCompra.Remove(ordenCompra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenCompraExists(int id)
        {
            return _context.OrdenesCompra.Any(e => e.IdOrdenCompra == id);
        }
    }
}
