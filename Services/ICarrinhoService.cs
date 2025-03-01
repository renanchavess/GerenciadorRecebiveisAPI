using GerenciadorRecebiveisAPI.DTOs;
using GerenciadorRecebiveisAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorRecebiveisAPI.Services
{
    public interface ICarrinhoService
    {
        public Task<bool> AdicionarNotaFiscalAsync(int carrinhoId, int notaFiscalId);
        public Task<bool> RemoverNotaFiscalAsync(int carrinhoId, int notaFiscalId);
        public Task<ResponseCheckout> RealizarCheckoutAsync(int carrinhoId);
        public Task<ResponseCarrinho> GetCarrinhoAsync(int id);
        public Task<ActionResult<List<ResponseCarrinho>>> GetCarrinhosByEmpresaIdAsync(int empresaId);
        public Task<ResponseCarrinho> CreateAsync(int empresaId);
    }
}
