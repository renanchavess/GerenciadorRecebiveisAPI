using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Context;
using GerenciadorRecebiveisAPI.Models;

namespace GerenciadorRecebiveisAPI.Repositories
{
    public class NotaFiscalRepository : INotaFiscalRepository
    {
        public readonly RecebiveisDbContext _context;

        public NotaFiscalRepository(RecebiveisDbContext context)
        {
            _context = context;
        }
        
        public async Task<NotaFiscal> CreateAsync(NotaFiscal notafiscal)
        {
            if (notafiscal == null)            
                throw new ArgumentNullException(nameof(notafiscal));

            await _context.NotasFiscais.AddAsync(notafiscal);
            await _context.SaveChangesAsync();
            return notafiscal;
        }

        public async Task<NotaFiscal> GetNotaFiscalAsync(int id)
        {
            var notafiscal = await _context.NotasFiscais.FindAsync(id);

            if (notafiscal == null)
                throw new ArgumentNullException(nameof(notafiscal));

            return notafiscal;
        }
    }
}