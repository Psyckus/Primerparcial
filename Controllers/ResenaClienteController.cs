using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial_1.Data;
using Parcial_1.Models;

namespace Parcial_1.Controllers
{
	[ApiController]
[Route("api/[controller]")]
	public class ResenaClienteController : Controller
	{
		private readonly AppDbContext _context;

		public ResenaClienteController(AppDbContext context)
		{
			_context = context;
		}

		// Método para buscar reseñas por valor de clasificación
		[HttpGet("BuscarResenas")]
		public async Task<ActionResult<IEnumerable<ResenaCliente>>> BuscarResenas(int? valorClasificacion)
		{
			// Validar el modelo
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var query = _context.ResenaCliente.AsQueryable();

			// Verificar si el valor de clasificación es diferente de nulo
			if (valorClasificacion != null)
			{
				query = query.Where(r => r.ValorClasificacion == valorClasificacion);
			}

			var reseñas = await query.ToListAsync();

			if (!reseñas.Any())
			{
				return NotFound("No se encontraron reseñas.");
			}

			return Ok(reseñas);
		}
	
	}
}
