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
        
        public NotaFiscal Create(NotaFiscal notafiscal)
        {
            if (notafiscal == null)            
                throw new ArgumentNullException(nameof(notafiscal));

            _context.NotasFiscais.Add(notafiscal);
            _context.SaveChanges();
            return notafiscal;
        }

        public NotaFiscal GetNotaFiscal(int id)
        {
            var notafiscal = _context.NotasFiscais.Find(id);

            if (notafiscal == null)
                throw new ArgumentNullException(nameof(notafiscal));

            return notafiscal;
        }
    }
}