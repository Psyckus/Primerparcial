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
    public class ItemOrdenComprasController : Controller
    {
        private readonly AppDbContext _context;

        public ItemOrdenComprasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ItemOrdenCompras
        // Controlador de vista (Controller)
        public async Task<IActionResult> Index(int? cantidad, decimal? precio)
        {
            var query = _context.ItemsOrdenCompra.AsQueryable();

            if (cantidad.HasValue)
            {
                query = query.Where(ioc => ioc.Cantidad == cantidad);
            }

            if (precio.HasValue)
            {
                query = query.Where(ioc => ioc.Precio == precio);
            }

            var itemsOrdenCompra = await query.ToListAsync();
            return View(itemsOrdenCompra);
        }


        // GET: ItemOrdenCompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemOrdenCompra = await _context.ItemsOrdenCompra
                .FirstOrDefaultAsync(m => m.IdItemOrdenCompra == id);
            if (itemOrdenCompra == null)
            {
                return NotFound();
            }

            return View(itemOrdenCompra);
        }

        // GET: ItemOrdenCompras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItemOrdenCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdItemOrdenCompra,IdItemProducto,IdOrdenCompra,Cantidad,Precio,FechaCreacion,FechaModificacion")] ItemOrdenCompra itemOrdenCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemOrdenCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemOrdenCompra);
        }

        // GET: ItemOrdenCompras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemOrdenCompra = await _context.ItemsOrdenCompra.FindAsync(id);
            if (itemOrdenCompra == null)
            {
                return NotFound();
            }
            return View(itemOrdenCompra);
        }

        // POST: ItemOrdenCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdItemOrdenCompra,IdItemProducto,IdOrdenCompra,Cantidad,Precio,FechaCreacion,FechaModificacion")] ItemOrdenCompra itemOrdenCompra)
        {
            if (id != itemOrdenCompra.IdItemOrdenCompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemOrdenCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemOrdenCompraExists(itemOrdenCompra.IdItemOrdenCompra))
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
            return View(itemOrdenCompra);
        }

        // GET: ItemOrdenCompras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemOrdenCompra = await _context.ItemsOrdenCompra
                .FirstOrDefaultAsync(m => m.IdItemOrdenCompra == id);
            if (itemOrdenCompra == null)
            {
                return NotFound();
            }

            return View(itemOrdenCompra);
        }

        // POST: ItemOrdenCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemOrdenCompra = await _context.ItemsOrdenCompra.FindAsync(id);
            if (itemOrdenCompra != null)
            {
                _context.ItemsOrdenCompra.Remove(itemOrdenCompra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemOrdenCompraExists(int id)
        {
            return _context.ItemsOrdenCompra.Any(e => e.IdItemOrdenCompra == id);
        }
    }
}
