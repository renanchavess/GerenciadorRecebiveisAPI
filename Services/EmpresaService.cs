using GerenciadorRecebiveisAPI.DTOs;
using GerenciadorRecebiveisAPI.Models;
using GerenciadorRecebiveisAPI.Repositories;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorRecebiveisAPI.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<List<ResponseEmpresa>> GetEmpresasAsync()
        {
            var empresas = await _empresaRepository.GetEmpresasAsync();
            List<ResponseEmpresa> responseEmpresas = new List<ResponseEmpresa>();
            foreach (var empresa in empresas)
            {
                responseEmpresas.Add(ResponseEmpresa.fromEntity(empresa));
            }
            return responseEmpresas;
        }

        public async Task<ResponseEmpresa> GetEmpresaByIdAsync(int id)
        {
            var empresa = await _empresaRepository.GetEmpresaByIdAsync(id);
            return ResponseEmpresa.fromEntity(empresa);
        }

        public async Task<ResponseEmpresa> CreateEmpresaAsync(RequestPostEmpresa empresaPost)
        {
            Empresa empresa = await _empresaRepository.GetEmpresaByCnpjAsync(empresaPost.Cnpj);

            if (empresa is not null)
            {
               throw new ValidationException("Empresa já cadastrada");
            }

            empresa = new Empresa()
            {
                Nome = empresaPost.Nome,
                CNPJ = empresaPost.Cnpj,
                Faturamento = empresaPost.Faturamento,
                Ramo = empresaPost.Ramo
            };

            await _empresaRepository.CreateAsync(empresa);
            return ResponseEmpresa.fromEntity(empresa);
        }
    }
}
