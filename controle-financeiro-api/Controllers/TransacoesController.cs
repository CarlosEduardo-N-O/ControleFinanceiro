using Microsoft.AspNetCore.Mvc;
using ControleFinanceiroAPI.Data;
using ControleFinanceiroAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiroAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransacoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransacoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET com filtros opcionais por id e categoria
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? id, [FromQuery] string? categoria)
        {
            var query = _context.Transacoes
                .Include(t => t.Conta)
                .Include(t => t.Categoria)
                .AsQueryable();

            if (id.HasValue)
                query = query.Where(t => t.Id == id.Value);

            if (!string.IsNullOrEmpty(categoria))
                query = query.Where(t => t.Categoria.Nome.Contains(categoria));

            var transacoes = await query.ToListAsync();
            return Ok(transacoes);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Post(Transacao transacao)
        {
            var conta = await _context.Contas.FindAsync(transacao.ContaId);
            if (conta == null)
                return NotFound("Conta não encontrada.");

            var categoria = await _context.Categorias.FindAsync(transacao.CategoriaId);
            if (categoria == null)
                return NotFound("Categoria não encontrada.");

            // Atualiza saldo
            if (transacao.Tipo == TipoTransacao.Receita)
                conta.Saldo += transacao.Valor;
            else if (transacao.Tipo == TipoTransacao.Despesa)
                conta.Saldo -= transacao.Valor;

            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = transacao.Id }, transacao);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Transacao transacao)
        {
            if (id != transacao.Id)
                return BadRequest("ID da URL não corresponde ao objeto.");

            var transacaoExistente = await _context.Transacoes.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
            if (transacaoExistente == null)
                return NotFound("Transação não encontrada.");

            _context.Entry(transacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var transacao = await _context.Transacoes.FindAsync(id);
            if (transacao == null)
                return NotFound();

            _context.Transacoes.Remove(transacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
