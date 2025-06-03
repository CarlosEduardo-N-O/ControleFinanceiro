namespace ControleFinanceiroAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        // Uma categoria pode ter várias transações
        public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    }
}
