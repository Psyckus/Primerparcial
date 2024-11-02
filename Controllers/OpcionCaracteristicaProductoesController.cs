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
    public class OpcionCaracteristicaProductoesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly OpcionCaracteristicaService _service;
        public OpcionCaracteristicaProductoesController(AppDbContext context, OpcionCaracteristicaService service)
        {
            _context = context;
            _service = service;
        }

        // GET: OpcionCaracteristicaProductoes
        // Controlador de vista (Controller)
        public async Task<IActionResult> Index(string? valor)
        {
            IEnumerable<OpcionCaracteristicaProducto> opcion;

            // Verifica si al menos uno de los campos tiene un valor
            if (!string.IsNullOrEmpty(valor))
            {
                // Realiza la búsqueda mediante el servicio
                opcion = await _service.BuscarOpcionCaracteristica(valor);
            }
            else
            {
                // Carga todos los datos si no se ha especificado ningún valor
                opcion = await _context.OpcionesCaracteristicaProducto.ToListAsync();
            }

            return View(opcion);
           
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
