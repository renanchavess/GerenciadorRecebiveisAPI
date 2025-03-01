using GerenciadorRecebiveisAPI.DTOs;

namespace GerenciadorRecebiveisAPI.Services
{
    public interface IEmpresaService
    {
        public Task<List<ResponseEmpresa>> GetEmpresasAsync();
        public Task<ResponseEmpresa> GetEmpresaByIdAsync(int id);
        public Task<ResponseEmpresa> CreateEmpresaAsync(RequestPostEmpresa empresaPost);
    }
}
