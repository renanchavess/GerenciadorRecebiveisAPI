using GerenciadorRecebiveisAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorRecebiveisAPI.DTOs
{
    public class ResponseCarrinho
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public List<ResponseNotaFiscal>? NotasFiscais { get; set; }
        public bool Checkout { get; set; }
        public static ResponseCarrinho fromEntity(Carrinho carrinho)
        {
            ResponseCarrinho responseCarrinho = new ResponseCarrinho()
            {
                Id = carrinho.Id,
                EmpresaId = carrinho.EmpresaId,
                NotasFiscais = carrinho.NotasFiscais.Select(nf => new ResponseNotaFiscal()
                {
                    Id = nf.Id,
                    Numero = nf.Numero,
                    Valor = nf.Valor,
                    DataVencimento = nf.DataVencimento,
                    EmpresaId = nf.EmpresaId
                }).ToList(),
                Checkout = carrinho.Checkout is not null
            };

            return responseCarrinho;
        }
    }
}