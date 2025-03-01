using GerenciadorRecebiveisAPI.Models;
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
        public int? CarrinhoId { get; set; }

        public static ResponseNotaFiscal fromEntity(NotaFiscal notaFiscal)
        {
            return new ResponseNotaFiscal()
            {
                Id = notaFiscal.Id,
                Numero = notaFiscal.Numero,
                Valor = notaFiscal.Valor,
                DataVencimento = notaFiscal.DataVencimento,
                EmpresaId = notaFiscal.EmpresaId,
                CarrinhoId = notaFiscal.CarrinhoId
            };
        }
    }
}