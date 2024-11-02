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
    public class MetodoPagoClientesController : Controller
    {
        private readonly AppDbContext _context;

        public MetodoPagoClientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MetodoPagoClientes
        // Controlador de vista (Controller)
        public async Task<IActionResult> Index(string? proveedor, string? cuenta, DateTime? fechaExpira)
        {
            var query = _context.MetodoPagoCliente.AsQueryable();

            if (!string.IsNullOrEmpty(proveedor))
            {
                query = query.Where(mp => EF.Functions.Like(mp.NombreProveedor, $"%{proveedor}%"));
            }

            if (!string.IsNullOrEmpty(cuenta))
            {
                query = query.Where(mp => EF.Functions.Like(mp.Cuenta, $"%{cuenta}%"));
            }

            if (fechaExpira.HasValue)
            {
                query = query.Where(mp => mp.FechaExpira.Date == fechaExpira.Value.Date);
            }

            var metodosPago = await query.ToListAsync();
            return View(metodosPago);
        }


        // GET: MetodoPagoClientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodoPagoCliente = await _context.MetodoPagoCliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (metodoPagoCliente == null)
            {
                return NotFound();
            }

            return View(metodoPagoCliente);
        }

        // GET: MetodoPagoClientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MetodoPagoClientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,TipoPagoId,NombreProveedor,Cuenta,FechaExpira,PorDefecto,FechaCreacion,FechaModificacion")] MetodoPagoCliente metodoPagoCliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metodoPagoCliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(metodoPagoCliente);
        }

        // GET: MetodoPagoClientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodoPagoCliente = await _context.MetodoPagoCliente.FindAsync(id);
            if (metodoPagoCliente == null)
            {
                return NotFound();
            }
            return View(metodoPagoCliente);
        }

        // POST: MetodoPagoClientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,TipoPagoId,NombreProveedor,Cuenta,FechaExpira,PorDefecto,FechaCreacion,FechaModificacion")] MetodoPagoCliente metodoPagoCliente)
        {
            if (id != metodoPagoCliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metodoPagoCliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetodoPagoClienteExists(metodoPagoCliente.Id))
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
            return View(metodoPagoCliente);
        }

        // GET: MetodoPagoClientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodoPagoCliente = await _context.MetodoPagoCliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (metodoPagoCliente == null)
            {
                return NotFound();
            }

            return View(metodoPagoCliente);
        }

        // POST: MetodoPagoClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metodoPagoCliente = await _context.MetodoPagoCliente.FindAsync(id);
            if (metodoPagoCliente != null)
            {
                _context.MetodoPagoCliente.Remove(metodoPagoCliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MetodoPagoClienteExists(int id)
        {
            return _context.MetodoPagoCliente.Any(e => e.Id == id);
        }
    }
}
