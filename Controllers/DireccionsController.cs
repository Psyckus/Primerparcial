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
    public class DireccionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly DireccionService _direccionService;
        public DireccionsController(AppDbContext context, DireccionService direccionService)
        {
            _context = context;
            _direccionService = direccionService;
        }

        // GET: Direccions
        // Controlador de vista (Controller)
        public async Task<IActionResult> Index(string direccionExacta, string codigoPostal)
        {
            IEnumerable<Direccion> direccion;

            // Verifica si al menos uno de los campos tiene un valor
            if (!string.IsNullOrEmpty(direccionExacta) || !string.IsNullOrEmpty(codigoPostal))
            {
                // Realiza la búsqueda mediante el servicio
                direccion = await _direccionService.BuscarDirecciones(direccionExacta, codigoPostal);
            }
            else
            {
                // Carga todos los datos si no se ha especificado ningún valor
                direccion = await _context.Direcciones.ToListAsync();
            }

            return View(direccion);
          
        }

        // GET: Direccions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direcciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (direccion == null)
            {
                return NotFound();
            }

            return View(direccion);
        }

        // GET: Direccions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Direccions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProvinciaId,CantonId,DistritoId,DireccionExacta,CodigoPostal,PaisId,FechaCreacion,FechaModificacion")] Direccion direccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(direccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(direccion);
        }

        // GET: Direccions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direcciones.FindAsync(id);
            if (direccion == null)
            {
                return NotFound();
            }
            return View(direccion);
        }

        // POST: Direccions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProvinciaId,CantonId,DistritoId,DireccionExacta,CodigoPostal,PaisId,FechaCreacion,FechaModificacion")] Direccion direccion)
        {
            if (id != direccion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(direccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DireccionExists(direccion.Id))
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
            return View(direccion);
        }

        // GET: Direccions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direcciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (direccion == null)
            {
                return NotFound();
            }

            return View(direccion);
        }

        // POST: Direccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var direccion = await _context.Direcciones.FindAsync(id);
            if (direccion != null)
            {
                _context.Direcciones.Remove(direccion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DireccionExists(int id)
        {
            return _context.Direcciones.Any(e => e.Id == id);
        }
    }
}
