using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Enum;

namespace GerenciadorRecebiveisAPI.Models
{
    public class Empresa
    {
        public int Id { get; set; }

        [StringLength(80)]
        public required string Nome { get; set; }

        [StringLength(18)]
        public required string CNPJ { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Faturamento { get; set; }  

        public Ramo Ramo { get; set; }
    }
}