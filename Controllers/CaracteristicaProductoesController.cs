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
    public class CaracteristicaProductoesController : Controller
    {
        private readonly AppDbContext _context;

        public CaracteristicaProductoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CaracteristicaProductoes
        // Controlador de vista (Controller)
        public async Task<IActionResult> Index(string nombre)
        {
            var query = _context.CaracteristicasProducto.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                query = query.Where(cp => EF.Functions.Like(cp.Nombre, $"%{nombre}%"));
            }

            var caracteristicas = await query.ToListAsync();
            return View(caracteristicas);
        }


        // GET: CaracteristicaProductoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caracteristicaProducto = await _context.CaracteristicasProducto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caracteristicaProducto == null)
            {
                return NotFound();
            }

            return View(caracteristicaProducto);
        }

        // GET: CaracteristicaProductoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CaracteristicaProductoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoriaProductoId,Nombre,FechaCreacion,FechaModificacion")] CaracteristicaProducto caracteristicaProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(caracteristicaProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(caracteristicaProducto);
        }

        // GET: CaracteristicaProductoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caracteristicaProducto = await _context.CaracteristicasProducto.FindAsync(id);
            if (caracteristicaProducto == null)
            {
                return NotFound();
            }
            return View(caracteristicaProducto);
        }

        // POST: CaracteristicaProductoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoriaProductoId,Nombre,FechaCreacion,FechaModificacion")] CaracteristicaProducto caracteristicaProducto)
        {
            if (id != caracteristicaProducto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caracteristicaProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaracteristicaProductoExists(caracteristicaProducto.Id))
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
            return View(caracteristicaProducto);
        }

        // GET: CaracteristicaProductoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caracteristicaProducto = await _context.CaracteristicasProducto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caracteristicaProducto == null)
            {
                return NotFound();
            }

            return View(caracteristicaProducto);
        }

        // POST: CaracteristicaProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caracteristicaProducto = await _context.CaracteristicasProducto.FindAsync(id);
            if (caracteristicaProducto != null)
            {
                _context.CaracteristicasProducto.Remove(caracteristicaProducto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaracteristicaProductoExists(int id)
        {
            return _context.CaracteristicasProducto.Any(e => e.Id == id);
        }
    }
}
