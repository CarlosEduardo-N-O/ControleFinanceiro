using ControleFinanceiroAPI.Data;
using ControleFinanceiroAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsuarioController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<ActionResult<Usuario>> Login(Usuario login)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Contas)
            .FirstOrDefaultAsync(u => u.Login == login.Login && u.Senha == login.Senha);

        if (usuario == null)
        {
            return Unauthorized();
        }

        return usuario;
    }

    // GET: api/Usuario
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int? id, [FromQuery] string? nome)
    {
        var query = _context.Usuarios
            .Include(u => u.Contas)
            .AsQueryable();

        if (id.HasValue)
            query = query.Where(u => u.Id == id.Value);

        if (!string.IsNullOrEmpty(nome))
            query = query.Where(u => u.Nome.Contains(nome));

        var usuarios = await query.ToListAsync();
        return Ok(usuarios);
    }

    // POST: api/Usuario
    [HttpPost]
    public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
    {
        // 💡 Dica: criptografar a senha antes de salvar
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = usuario.Id }, usuario);
    }

    // PUT: api/Usuario/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Usuario usuario)
    {
        if (id != usuario.Id)
            return BadRequest("ID da URL não corresponde ao objeto.");

        var usuarioExistente = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        if (usuarioExistente == null)
            return NotFound("Usuário não encontrado.");

        _context.Entry(usuario).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Usuario/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
            return NotFound("Usuário não encontrado.");

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
