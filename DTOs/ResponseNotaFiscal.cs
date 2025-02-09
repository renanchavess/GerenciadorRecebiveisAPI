using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorRecebiveisAPI.DTOs
{
    public class ResponseNotaFiscal
    {
        public int Id { get; set; }        
        public int Numero { get; set; }        
        public decimal Valor { get; set; }
        public DateOnly DataVencimento { get; set; }
        public int EmpresaId { get; set; }
    }
}