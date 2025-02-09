using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Enum;

namespace GerenciadorRecebiveisAPI.DTOs
{
    public class RequestPostEmpresa
    {
        public required string Nome { get; set; }
        public required string Cnpj { get; set; }
        public required decimal Faturamento { get; set; }
        public required Ramo Ramo { get; set; }
    }
}