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
    public class ItemProductoesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ItemProductoService _itemProductoService;

        public ItemProductoesController(AppDbContext context, ItemProductoService itemProductoService)
        {
            _context = context;
            _itemProductoService = itemProductoService;
        }

        // GET: ItemProductoes
        // Controlador de vista (Controller)
        public async Task<IActionResult> Index(string? codigoBarras, int? cantidadDisponible, string? urlImagen, decimal? precio)
        {
            IEnumerable<ItemProducto> itemProductos;

            // Verifica si al menos uno de los campos tiene un valor
            if (!string.IsNullOrEmpty(codigoBarras) || cantidadDisponible.HasValue || !string.IsNullOrEmpty(urlImagen) || precio.HasValue)
            {
                // Realiza la búsqueda mediante el servicio
                itemProductos = await _itemProductoService.BuscarItemProducto(codigoBarras, cantidadDisponible, urlImagen ,precio);
            }
            else
            {
                // Carga todos los datos si no se ha especificado ningún valor
                itemProductos = await _context.ItemsProducto.ToListAsync();
            }

            return View(itemProductos);
           
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
