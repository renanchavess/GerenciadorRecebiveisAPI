using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Models;

namespace GerenciadorRecebiveisAPI.Repositories
{
    public interface INotaFiscalRepository
    {
        NotaFiscal GetNotaFiscal(int id);
        NotaFiscal Create(NotaFiscal notafiscal);
    }
}