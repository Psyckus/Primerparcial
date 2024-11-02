using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial_1.Data;
using Parcial_1.Models;

namespace Parcial_1.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ClienteController : Controller
	{
		private readonly AppDbContext _context;

		public ClienteController(AppDbContext context)
		{
			_context = context;
		}
		
		// Método para buscar clientes por nombre, apellidos o correo
		[HttpGet("BuscarClientes")]
		public async Task<ActionResult<IEnumerable<Cliente>>> BuscarClientes(string nombre = null, string apellidos = null, string correo = null)
		{
			// Validar el modelo
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var query = _context.Clientes.AsQueryable();

			if (!string.IsNullOrWhiteSpace(nombre))
			{
				query = query.Where(c => c.NombreCompleto.Contains(nombre));
			}

			if (!string.IsNullOrWhiteSpace(apellidos))
			{
				query = query.Where(c => c.NombreCompleto.Contains(apellidos));
			}

			if (!string.IsNullOrWhiteSpace(correo))
			{
				query = query.Where(c => c.Correo.Contains(correo));
			}

			var clientes = await query.ToListAsync();

			if (!clientes.Any())
			{
				return NotFound("No se encontraron clientes.");
			}

			return Ok(clientes);
		}
	}
}
