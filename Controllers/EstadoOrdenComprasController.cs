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
    public class EstadoOrdenComprasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly EstadoOrdenService _estadoService;

        public EstadoOrdenComprasController(AppDbContext context, EstadoOrdenService estadoService)
        {
            _context = context;
            _estadoService = estadoService;
        }

        // GET: EstadoOrdenCompras
   
        public async Task<IActionResult> Index(string estado)
        {

            IEnumerable<EstadoOrdenCompra> estadoOrden;

            // Verifica si al menos uno de los campos tiene un valor
            if (!string.IsNullOrEmpty(estado) )
            {
                // Realiza la búsqueda mediante el servicio
                estadoOrden = await _estadoService.BuscarEstadoOrden(estado);
            }
            else
            {
                // Carga todos los datos si no se ha especificado ningún valor
                estadoOrden = await _context.EstadosOrdenCompra.ToListAsync();
            }

            return View(estadoOrden);
      

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
