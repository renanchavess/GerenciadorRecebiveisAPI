using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Context;
using GerenciadorRecebiveisAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorRecebiveisAPI.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly RecebiveisDbContext _context;

        public EmpresaRepository(RecebiveisDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empresa>> GetEmpresasAsync()
        {
            var empresas = await _context.Empresas.ToListAsync();
            return empresas;
        }

        public async Task<Empresa> CreateAsync(Empresa empresa)
        {
            if (empresa == null)            
                throw new ArgumentNullException(nameof(empresa));

            await _context.Empresas.AddAsync(empresa);
            await _context.SaveChangesAsync();

            return empresa;
        }

        public async Task<Empresa> GetEmpresaByIdAsync(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);

            return empresa;
        }

        public async Task<Empresa> GetEmpresaByCnpjAsync(string cnpj)
        {
            var empresa = await _context.Empresas.FirstOrDefaultAsync(e => e.CNPJ == cnpj);
            return empresa;
        }
    }
}