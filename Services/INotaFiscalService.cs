using GerenciadorRecebiveisAPI.DTOs;
using GerenciadorRecebiveisAPI.Models;

namespace GerenciadorRecebiveisAPI.Services
{
    public interface INotaFiscalService
    {
        public Task<ResponseNotaFiscal> GetNotaFiscal(int id);
        public Task<ResponseNotaFiscal> CreateNotaFiscal(RequestPostNotaFiscal notaFiscalPost);
        public Task<List<ResponseNotaFiscal>> GetNotaFiscalByEmpresaId(int empresaId);
    }
}
