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
    public class ResenaClientesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ResenaService _resenaService;

        public ResenaClientesController(AppDbContext context, ResenaService resenaService)
        {
            _context = context;
            _resenaService = resenaService;
        }

        // GET: ResenaClientes
        public async Task<IActionResult> Index(int? valorClasificacion)
        {
            IEnumerable<ResenaCliente> resena;

            // Verifica si al menos uno de los campos tiene un valor
            if (valorClasificacion.HasValue)
            {
                // Realiza la búsqueda mediante el servicio
                resena = await _resenaService.BuscarResenas(valorClasificacion);
            }
            else
            {
                // Carga todos los datos si no se ha especificado ningún valor
                resena = await _context.ResenaCliente.ToListAsync();
            }

            return View(resena);
     
        }

        // GET: ResenaClientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resenaCliente = await _context.ResenaCliente
                .Include(r => r.Cliente)
                .Include(r => r.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resenaCliente == null)
            {
                return NotFound();
            }

            return View(resenaCliente);
        }

        // GET: ResenaClientes/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Clave");
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Descripcion");
            return View();
        }

        // POST: ResenaClientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResenaCliente resenaCliente)
        {
            // Establecer las fechas de creación y modificación
            resenaCliente.FechaCreacion = DateTime.Now;
            resenaCliente.FechaModificacion = DateTime.Now;

            // Agregar el nuevo objeto a la base de datos
            _context.ResenaCliente.Add(resenaCliente);

            // Intentar guardar los cambios en la base de datos
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirigir a la lista después de una inserción exitosa
            }
            catch (DbUpdateException ex)
            {
                // Manejar excepciones de actualización en la base de datos
                ModelState.AddModelError("", "No se pudo guardar los cambios. Intenta de nuevo.");
                Console.WriteLine(ex.Message); // Log para ver el error
            }

            // Si llegamos aquí, algo salió mal, volvemos a mostrar el formulario
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Clave", resenaCliente.ClienteId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Descripcion", resenaCliente.ProductoId);
            return View(resenaCliente);
        }


        // GET: ResenaClientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resenaCliente = await _context.ResenaCliente.FindAsync(id);
            if (resenaCliente == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Clave", resenaCliente.ClienteId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Descripcion", resenaCliente.ProductoId);
            return View(resenaCliente);
        }

        // POST: ResenaClientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,ProductoId,ValorClasificacion,Comentario,FechaCreacion,FechaModificacion")] ResenaCliente resenaCliente)
        {
            if (id != resenaCliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resenaCliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResenaClienteExists(resenaCliente.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Clave", resenaCliente.ClienteId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Descripcion", resenaCliente.ProductoId);
            return View(resenaCliente);
        }

        // GET: ResenaClientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resenaCliente = await _context.ResenaCliente
                .Include(r => r.Cliente)
                .Include(r => r.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resenaCliente == null)
            {
                return NotFound();
            }

            return View(resenaCliente);
        }

        // POST: ResenaClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resenaCliente = await _context.ResenaCliente.FindAsync(id);
            if (resenaCliente != null)
            {
                _context.ResenaCliente.Remove(resenaCliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResenaClienteExists(int id)
        {
            return _context.ResenaCliente.Any(e => e.Id == id);
        }
    }
}
