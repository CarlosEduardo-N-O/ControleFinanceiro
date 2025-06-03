using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; // <- Importante para usar [JsonIgnore]

namespace ControleFinanceiroAPI.Models
{
    public class Transacao
    {
        public int Id { get; set; }

        // Chave estrangeira para Conta
        public int ContaId { get; set; }

        [ForeignKey(nameof(ContaId))]
        [JsonIgnore]
        public Conta? Conta { get; set; }

        // Chave estrangeira para Categoria
        public int CategoriaId { get; set; }

        [ForeignKey(nameof(CategoriaId))]
        [JsonIgnore]
        public Categoria? Categoria { get; set; }

        public TipoTransacao Tipo { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime Data { get; set; }
    }
}
