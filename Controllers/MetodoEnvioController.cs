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
	public class MetodoEnvioController : ControllerBase
	{
		private readonly AppDbContext _context;

		public MetodoEnvioController(AppDbContext context)
		{
			_context = context;
		}

		// Búsqueda de métodos de envío por nombre o precio
		[HttpGet("buscarMetodoEnvio")]
		public async Task<IActionResult> buscarMetodoEnvio(string nombre = null, decimal? precio = null)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var query = _context.MetodosEnvio.AsQueryable();

			if (!string.IsNullOrEmpty(nombre))
			{
				query = query.Where(me => me.Nombre.Contains(nombre));
			}

			if (precio != null)
			{
				query = query.Where(me => me.Precio == precio);
			}

			var resultados = await query.ToListAsync();

			return Ok(resultados);
		}


	}
}
