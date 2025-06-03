using Microsoft.AspNetCore.Mvc;
using ControleFinanceiroAPI.Data;
using ControleFinanceiroAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiroAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContasController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conta>>> GetContas([FromQuery] int? usuario)
        {
            var query = _context.Contas
                .Include(c => c.Transacoes)
                .AsQueryable();

            if (usuario.HasValue)
                query = query.Where(c => c.UsuarioId == usuario.Value);

            return await query.ToListAsync();
        }

        // ✅ POST
        [HttpPost]
        public async Task<IActionResult> Post(Conta conta)
        {
            _context.Contas.Add(conta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetContas), new { id = conta.Id }, conta);
        }

        // ✅ PUT por ID
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConta(int id, Conta conta)
        {
            if (id != conta.Id)
            {
                return BadRequest("ID da URL não corresponde ao ID do objeto enviado.");
            }

            _context.Entry(conta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Contas.Any(c => c.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // ✅ DELETE por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConta(int id)
        {
            var conta = await _context.Contas.FindAsync(id);
            if (conta == null)
            {
                return NotFound();
            }

            _context.Contas.Remove(conta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
