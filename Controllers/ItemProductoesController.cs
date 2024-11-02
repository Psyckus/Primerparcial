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
    public class ItemProductoesController : Controller
    {
        private readonly AppDbContext _context;

        public ItemProductoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ItemProductoes
        // Controlador de vista (Controller)
        public async Task<IActionResult> Index(string? codigoBarras, int? cantidadDisponible, string? urlImagen, decimal? precio)
        {
            var query = _context.ItemsProducto.AsQueryable();

            if (!string.IsNullOrEmpty(codigoBarras))
                query = query.Where(ip => EF.Functions.Like(ip.CodigoBarras, $"%{codigoBarras}%"));

            if (cantidadDisponible.HasValue)
                query = query.Where(ip => ip.CantidadDisponible == cantidadDisponible.Value);

            if (!string.IsNullOrEmpty(urlImagen))
                query = query.Where(ip => EF.Functions.Like(ip.UrlImagen, $"%{urlImagen}%"));

            if (precio.HasValue)
                query = query.Where(ip => ip.Precio == precio.Value);

            var itemsProducto = await query.ToListAsync();
            return View(itemsProducto);
        }


        // GET: ItemProductoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemProducto = await _context.ItemsProducto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemProducto == null)
            {
                return NotFound();
            }

            return View(itemProducto);
        }

        // GET: ItemProductoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItemProductoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductoId,CodigoBarras,CantidadDisponible,UrlImagen,Precio,FechaCreacion,FechaModificacion")] ItemProducto itemProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemProducto);
        }

        // GET: ItemProductoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemProducto = await _context.ItemsProducto.FindAsync(id);
            if (itemProducto == null)
            {
                return NotFound();
            }
            return View(itemProducto);
        }

        // POST: ItemProductoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductoId,CodigoBarras,CantidadDisponible,UrlImagen,Precio,FechaCreacion,FechaModificacion")] ItemProducto itemProducto)
        {
            if (id != itemProducto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemProductoExists(itemProducto.Id))
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
            return View(itemProducto);
        }

        // GET: ItemProductoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemProducto = await _context.ItemsProducto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemProducto == null)
            {
                return NotFound();
            }

            return View(itemProducto);
        }

        // POST: ItemProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemProducto = await _context.ItemsProducto.FindAsync(id);
            if (itemProducto != null)
            {
                _context.ItemsProducto.Remove(itemProducto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemProductoExists(int id)
        {
            return _context.ItemsProducto.Any(e => e.Id == id);
        }
    }
}
