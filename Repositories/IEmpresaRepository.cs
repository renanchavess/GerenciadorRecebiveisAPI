using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Models;

namespace GerenciadorRecebiveisAPI.Repositories
{
    public interface IEmpresaRepository
    {
        Task<Empresa> GetEmpresaByIdAsync(int id);
        Task<IEnumerable<Empresa>> GetEmpresasAsync();
        Task<Empresa> CreateAsync(Empresa empresa);
        Task<Empresa> GetEmpresaByCnpjAsync(string cnpj);
    }
}