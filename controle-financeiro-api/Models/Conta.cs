using System.Text.Json.Serialization;

namespace ControleFinanceiroAPI.Models
{
    public class Conta
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Saldo { get; set; }

        // Um conta pertence a um usuário
        public int UsuarioId { get; set; }

        [JsonIgnore]
        public Usuario? Usuario { get; set; }  // <-- Deixamos o Usuario como 'nullable'

        [JsonIgnore]
        public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    }
}
