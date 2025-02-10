using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Context;
using GerenciadorRecebiveisAPI.Models;

namespace GerenciadorRecebiveisAPI.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly RecebiveisDbContext _context;

        public EmpresaRepository(RecebiveisDbContext context)
        {
            _context = context;
        }

        public async Task<Empresa> CreateAsync(Empresa empresa)
        {
            if (empresa == null)            
                throw new ArgumentNullException(nameof(empresa));

            await _context.Empresas.AddAsync(empresa);
            await _context.SaveChangesAsync();

            return empresa;
        }

        public async Task<Empresa> GetEmpresaAsync(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);

            return empresa;
        }
    }
}