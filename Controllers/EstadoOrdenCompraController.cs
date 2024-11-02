using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial_1.Data;
using Parcial_1.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial_1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EstadoOrdenCompraController : ControllerBase
	{
		private readonly AppDbContext _context;

		public EstadoOrdenCompraController(AppDbContext context)
		{
			_context = context;
		}

		// Búsqueda de estado de orden de compra por estado
		[HttpGet("buscarEstadoOrden")]
		public async Task<IActionResult> buscarEstadoOrden(string estado)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var query = _context.EstadosOrdenCompra.AsQueryable();

			if (!string.IsNullOrEmpty(estado))
			{
				query = query.Where(e => e.Estado.Contains(estado));
			}

			var resultados = await query.ToListAsync();

			return Ok(resultados);
		}

	
		
	}
}
