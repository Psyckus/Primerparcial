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
    public class CategoriaProductoesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly CategoriaProductoService _CategoriaProductoService;

        public CategoriaProductoesController(AppDbContext context, CategoriaProductoService categoriaProductoService)
        {
            _context = context;
            _CategoriaProductoService = categoriaProductoService;
        }

        // GET: CategoriaProductoes
        // Controlador de vista (Controller)
        public async Task<IActionResult> Index(string nombreCategoria)
        {
            IEnumerable<CategoriaProducto> categoriasproducto;

            if (!string.IsNullOrEmpty(nombreCategoria))
            {
                // Realiza la búsqueda mediante el servicio
                categoriasproducto = await _CategoriaProductoService.BuscarCategoria(nombreCategoria);
            }
            else
            {
                // Carga todos los datos si no se ha especificado un nombre
                categoriasproducto = await _context.CategoriasProducto.ToListAsync();

               
            }
            return View(categoriasproducto);
        }


        // GET: CategoriaProductoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaProducto = await _context.CategoriasProducto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaProducto == null)
            {
                return NotFound();
            }

            return View(categoriaProducto);
        }

        // GET: CategoriaProductoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriaProductoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoriaPadreId,NombreCategoria,FechaCreacion,FechaModificacion")] CategoriaProducto categoriaProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriaProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaProducto);
        }

        // GET: CategoriaProductoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaProducto = await _context.CategoriasProducto.FindAsync(id);
            if (categoriaProducto == null)
            {
                return NotFound();
            }
            return View(categoriaProducto);
        }

        // POST: CategoriaProductoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoriaPadreId,NombreCategoria,FechaCreacion,FechaModificacion")] CategoriaProducto categoriaProducto)
        {
            if (id != categoriaProducto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaProductoExists(categoriaProducto.Id))
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
            return View(categoriaProducto);
        }

        // GET: CategoriaProductoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaProducto = await _context.CategoriasProducto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaProducto == null)
            {
                return NotFound();
            }

            return View(categoriaProducto);
        }

        // POST: CategoriaProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriaProducto = await _context.CategoriasProducto.FindAsync(id);
            if (categoriaProducto != null)
            {
                _context.CategoriasProducto.Remove(categoriaProducto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaProductoExists(int id)
        {
            return _context.CategoriasProducto.Any(e => e.Id == id);
        }
    }
}
