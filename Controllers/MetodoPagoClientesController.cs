using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Parcial_1.Data;
using Parcial_1.Models;
using Parcial_1.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Parcial_1.Controllers
{
    public class MetodoPagoClientesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly MetodoPagoService _metodoPagoService;

        public MetodoPagoClientesController(AppDbContext context, MetodoPagoService metodoPagoService)
        {
            _context = context;
            _metodoPagoService = metodoPagoService;
        }

        // GET: MetodoPagoClientes
        // Controlador de vista (Controller)
        public async Task<IActionResult> Index(string? proveedor, string? cuenta, DateTime? fechaExpira)
        {
            IEnumerable<MetodoPagoCliente> metodoPagoCliente;

            // Verifica si al menos uno de los campos tiene un valor
            if (!string.IsNullOrEmpty(proveedor) || !string.IsNullOrEmpty(cuenta) || fechaExpira.HasValue)
            {
                // Realiza la búsqueda mediante el servicio
                metodoPagoCliente = await _metodoPagoService.BuscarMetodoPago(proveedor, cuenta, fechaExpira);
            }
            else
            {
                // Carga todos los datos si no se ha especificado ningún valor
                metodoPagoCliente = await _context.MetodoPagoCliente.ToListAsync();
            }

            return View(metodoPagoCliente);
           
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
