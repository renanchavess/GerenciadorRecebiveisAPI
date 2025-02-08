using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Models;

namespace GerenciadorRecebiveisAPI.Repositories
{
    public interface IEmpresaRepository
    {
        Task<Empresa> GetEmpresaAsync(int id);
        Task<Empresa> CreateAsync(Empresa empresa);
    }
}