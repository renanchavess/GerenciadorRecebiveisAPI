using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Models;

namespace GerenciadorRecebiveisAPI.Repositories
{
    public interface ICarrinhoRepository
    {
        Task<Carrinho> GetCarrinhoAsync(int id);
        Task<Carrinho> CreateAsync(Carrinho carrinho);
        Task<bool> AdicionarNotaFiscalAsync(int id, NotaFiscal notafiscal);
        Task<bool> RemoverNotaFiscalAsync(int id, NotaFiscal notafiscal);
    }
}