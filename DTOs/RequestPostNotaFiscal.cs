using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorRecebiveisAPI.DTOs
{
    public class RequestPostNotaFiscal
    {
        public required int Numero { get; set; }
        public required decimal Valor { get; set; }
        public required DateOnly DataVencimento { get; set; }
        public required int EmpresaId { get; set; }

    }
}