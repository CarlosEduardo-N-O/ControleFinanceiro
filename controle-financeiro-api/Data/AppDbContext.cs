using Microsoft.EntityFrameworkCore;
using ControleFinanceiroAPI.Models;

namespace ControleFinanceiroAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Conta> Contas { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
