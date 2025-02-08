using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GerenciadorRecebiveisAPI.Models
{
    public class NotaFiscal
    {
        public int Id { get; set; }

        [StringLength(30)]
        public required string Numero { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Valor { get; set; }

        public DateTime DataVencimento { get; set; }

        public int EmpresaId { get; set; }

        public int? CarrinhoId { get; set; }

        [JsonIgnore]
        Carrinho? Carrinho { get; set; }
    }
}