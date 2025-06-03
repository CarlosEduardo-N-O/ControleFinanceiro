using ControleFinanceiroAPI.Data;
using ControleFinanceiroAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/Categorias
    [HttpPost]
    public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCategorias), new { id = categoria.Id }, categoria);
    }


    // ✅ GET: api/Categorias?nome=Alimentação
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias([FromQuery] int? id, [FromQuery] string? nome)
    {
        var query = _context.Categorias.AsQueryable();

        if (id.HasValue)
            query = query.Where(c => c.Id == id.Value);

        if (!string.IsNullOrEmpty(nome))
            query = query.Where(c => c.Nome.Contains(nome));

        return await query.ToListAsync();
    }

    // ✅ PUT: api/Categorias/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
    {
        if (id != categoria.Id)
        {
            return BadRequest("ID da URL não corresponde ao ID do objeto enviado.");
        }

        _context.Entry(categoria).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Categorias.Any(c => c.Id == id))
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

    // ✅ DELETE: api/Categorias/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoria(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null)
        {
            return NotFound();
        }

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
