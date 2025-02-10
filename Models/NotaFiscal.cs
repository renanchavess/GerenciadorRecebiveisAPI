using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorRecebiveisAPI.Models
{
    public class NotaFiscal
    {
        public int Id { get; set; }

        [StringLength(30)]
        public int Numero { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Valor { get; set; }

        public DateOnly DataVencimento { get; set; }

        public int EmpresaId { get; set; }

        public int? CarrinhoId { get; set; }

        Carrinho? Carrinho { get; set; }

        public bool Vencida()
        {
            return DataVencimento.DayNumber < DateOnly.FromDateTime(DateTime.Now).DayNumber;
        }

        public int Prazo()
        {
            return DataVencimento.DayNumber - DateOnly.FromDateTime(DateTime.Now).DayNumber;
        }
    }
}