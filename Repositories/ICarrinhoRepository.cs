using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Models;

namespace GerenciadorRecebiveisAPI.Repositories
{
    public interface ICarrinhoRepository
    {
        Carrinho GetCarrinho(int id);
        Carrinho Create(Carrinho carrinho);
        Carrinho AdicionarNotaFiscal(int id, NotaFiscal notafiscal);
        Carrinho RemoverNotaFiscal(int id, NotaFiscal notafiscal);
    }
}