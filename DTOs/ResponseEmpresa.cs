using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Enum;

namespace GerenciadorRecebiveisAPI.DTOs
{
    public class ResponseEmpresa
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Cnpj { get; set; }
        public decimal Faturamento { get; set; }
        public Ramo Ramo { get; set; }
    }
}