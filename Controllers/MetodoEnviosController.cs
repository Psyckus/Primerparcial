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
    public class MetodoEnviosController : Controller
    {
        private readonly AppDbContext _context;

        public MetodoEnviosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MetodoEnvios
        // Controlador de vista (Controller)
        public async Task<IActionResult> Index(string? nombre, decimal? precio)
        {
            var query = _context.MetodosEnvio.AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(me => EF.Functions.Like(me.Nombre, $"%{nombre}%"));
            }

            if (precio.HasValue)
            {
                query = query.Where(me => me.Precio == precio.Value);
            }

            var metodosEnvio = await query.ToListAsync();
            return View(metodosEnvio);
        }


        // GET: MetodoEnvios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodoEnvio = await _context.MetodosEnvio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (metodoEnvio == null)
            {
                return NotFound();
            }

            return View(metodoEnvio);
        }

        // GET: MetodoEnvios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MetodoEnvios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Precio,FechaCreacion,FechaModificacion")] MetodoEnvio metodoEnvio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metodoEnvio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(metodoEnvio);
        }

        // GET: MetodoEnvios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodoEnvio = await _context.MetodosEnvio.FindAsync(id);
            if (metodoEnvio == null)
            {
                return NotFound();
            }
            return View(metodoEnvio);
        }

        // POST: MetodoEnvios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Precio,FechaCreacion,FechaModificacion")] MetodoEnvio metodoEnvio)
        {
            if (id != metodoEnvio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metodoEnvio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetodoEnvioExists(metodoEnvio.Id))
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
            return View(metodoEnvio);
        }

        // GET: MetodoEnvios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodoEnvio = await _context.MetodosEnvio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (metodoEnvio == null)
            {
                return NotFound();
            }

            return View(metodoEnvio);
        }

        // POST: MetodoEnvios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metodoEnvio = await _context.MetodosEnvio.FindAsync(id);
            if (metodoEnvio != null)
            {
                _context.MetodosEnvio.Remove(metodoEnvio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MetodoEnvioExists(int id)
        {
            return _context.MetodosEnvio.Any(e => e.Id == id);
        }
    }
}
