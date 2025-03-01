using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Models;

namespace GerenciadorRecebiveisAPI.Repositories
{
    public interface INotaFiscalRepository
    {
        Task<NotaFiscal> GetNotaFiscalAsync(int id);
        Task<NotaFiscal> CreateAsync(NotaFiscal notafiscal);
        Task<List<NotaFiscal>> GetNotaFiscalByEmpresaId(int empresaId);
    }
}